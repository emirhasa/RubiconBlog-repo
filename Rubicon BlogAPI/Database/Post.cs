using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubicon_BlogAPI.Database
{
    public class Post
    {
        public Post()
        {
            PostTags = new HashSet<PostTag>();
        }

        //this will be the real key
        public int PostId { get; set; }

        //this will be an unique index
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //here it's post tags because the class represents one post - and this is the collection of tags tied to that post
        //it's useful if we need to retrieve for instance all tags related to a particular post as opposed to
        //all the posts for one tag

        public ICollection<PostTag> PostTags { get; set; }
    }
}
