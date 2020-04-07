using Rubicon_BlogAPI.Model.Requests.Insert;
using Rubicon_BlogAPI.Model.Requests.Search;
using Rubicon_BlogAPI.Model.Requests.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubicon_BlogAPI.Services
{
    public interface IPostsService 
    {
        Model.PostsList Get(PostsSearchRequest search);
        Model.Post GetBySlug(string slug);
        Model.Post Insert(PostInsertRequest request);
        Model.Post Update(string slug, PostUpdateRequest request);
        bool Delete(string slug);
    }
}
