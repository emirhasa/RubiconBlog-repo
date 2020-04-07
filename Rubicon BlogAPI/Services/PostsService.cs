using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rubicon_BlogAPI.Database;
using Rubicon_BlogAPI.Exceptions;
using Rubicon_BlogAPI.Model;
using Rubicon_BlogAPI.Model.Requests.Insert;
using Rubicon_BlogAPI.Model.Requests.Search;
using Rubicon_BlogAPI.Model.Requests.Update;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rubicon_BlogAPI.Services
{
    public class PostsService : IPostsService
    {
        private readonly IMapper _mapper;
        private readonly BlogContext _context;

        public PostsService(IMapper mapper, BlogContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public Model.PostsList Get(PostsSearchRequest search)
        {
            var query = _context.Posts.AsQueryable();

            if (search != null && !string.IsNullOrWhiteSpace(search.Tag))
                query = query.Where(p => p.PostTags.Any(pt => pt.TagId == search.Tag));

            query = query.Include(p => p.PostTags);

            var posts = _mapper.Map<List<Model.Post>>(query.OrderByDescending(p => p.UpdatedAt).Take(5).ToList());

            if (posts.Count == 0) return null;

            return new Model.PostsList
            {
                blogPosts = posts,
                postsCount = posts.Count()
            };
        }

        public Model.Post GetBySlug(string slug)
        {
            return _mapper.Map<Model.Post>(_context.Posts.Where(p=>p.Slug == slug).Include(p=>p.PostTags).SingleOrDefault());
        }

        public Model.Post Insert(PostInsertRequest request)
        {
            var entity = _mapper.Map<Database.Post>(request);
            entity.CreatedAt = DateTime.Now.ToUniversalTime();
            entity.UpdatedAt = DateTime.Now.ToUniversalTime();
            entity.Slug = CreateSlug(entity.Title);

            //validate does a slug exist already? 
            bool valid = SlugValid(entity.Slug);

            if (valid)
            {
                _context.Posts.Add(entity);

                _context.SaveChanges();

                //let's go through the tags and add them
                var tags = request.tagList;
                if (tags != null)
                {
                    if (tags.Count > 0)
                    {
                        foreach (var tag in tags)
                        {
                            //check if tag exists if not add it
                            var check = _context.Tags.Where(t => t.Name == tag).SingleOrDefault();

                            //tag doesn't exist add it first
                            if (check == null) _context.Tags.Add(new Database.Tag
                            {
                                Name = tag
                            }); 

                            //assign the tag to the new post
                            _context.PostTags.Add(new Database.PostTag
                            {
                                PostId = entity.PostId,
                                TagId = tag
                            });
                        }
                    }
                }

                _context.SaveChanges();

                return _mapper.Map<Model.Post>(entity);
            } else
            {
                throw new UserException("Post with this title already exists, choose another one");
            }

        }

        public Model.Post Update(string slug, PostUpdateRequest request)
        {
            //we validated the things but if there's still some kind of specific error just return a null
            //we don't throw an Exception to save performance and keep simplicity of course it could be implemented in better ways
            //in that case controller will receive a null and notify user about failure to execute update
            try
            {
                var entity = _context.Posts.Where(p => p.Slug == slug).Include(p=>p.PostTags).Single();

                //check which post parameters were actually changed?
                if (!string.IsNullOrWhiteSpace(request.Title) && request.Title != entity.Title)
                {
                    entity.Title = request.Title;

                    //also need to create a new slug
                    entity.Slug = CreateSlug(request.Title);

                    bool valid = SlugValid(entity.Slug);
                    if (!valid) throw new UserException("Post with this title already exists, choose another one");
                }

                if (!string.IsNullOrWhiteSpace(request.Description) && request.Description != entity.Description)
                {
                    entity.Description = request.Description;
                }

                if (!string.IsNullOrWhiteSpace(request.Body) && request.Body != entity.Body)
                {
                    entity.Body = request.Body;
                }

                entity.UpdatedAt = DateTime.Now.ToUniversalTime();

                _context.SaveChanges();
                return _mapper.Map<Model.Post>(entity);
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(string slug)
        {
            var entity = _context.Posts.Where(p=>p.Slug == slug).SingleOrDefault();

            if (entity != null)
            {
                _context.Posts.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public string CreateSlug(string title)
        {
            //There are 3 solutions I tried - you can see below, the 3rd is the simplest and best at first glance. If it's not let's 
            //stick with it for sake of simplicity for the moment at least
            string slug = RemoveAccents3(title);
            slug = FormatTitle(slug);

            //we finally have the required format
            return slug;
        }

        public string FormatTitle(string title)
        {
            string slug = RemoveAccents(title);

            //Remove punctuation
            slug = slug.Where(c => !char.IsPunctuation(c)).ToArray().ToString();

            //Leave only alphanumeric characters and spaces
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            string format = rgx.Replace(title, "");

            format = format.ToLower();

            //Replace multiple white spaces with one 
            //by splitting based on white space into words
            var tempFormat = format.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s));

            //- and then putting it back with a dash instead of whitespace in between each word
            format = string.Join("-", tempFormat);
            return format;
        }

        public bool SlugValid(string slug)
        {
            var slugCheck = _context.Posts.Where(p => p.Slug == slug).SingleOrDefault();
            if (slugCheck == null) return true;
            return false;
        }

        public string RemoveAccents(string title)
        {
            var normalizedString = title.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public string RemoveAccents2(string title)
        {
            if (null == title) return null;
            var chars = title
                .Normalize(NormalizationForm.FormD)
                .ToCharArray()
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray();

            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public string RemoveAccents3(string text)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] tempBytes;
            tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(text);
            return Encoding.UTF8.GetString(tempBytes);
        }
    }
}
