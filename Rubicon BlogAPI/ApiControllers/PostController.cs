using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rubicon_BlogAPI.Model.Requests.Insert;
using Rubicon_BlogAPI.Model.Requests.Search;
using Rubicon_BlogAPI.Model.Requests.Update;
using Rubicon_BlogAPI.Services;

namespace Rubicon_BlogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        private readonly IPostsService _service;
        public PostController(IPostsService service) {
            _service = service;
        }

        [HttpGet]
        public ActionResult<Model.PostsList> Get([FromQuery] PostsSearchRequest search)
        {
            var result = _service.Get(search);
            if(result == null)
            {
                return NoContent();
            }
            return result;
        }

        [HttpGet("{slug}")]
        public ActionResult<Model.Post> GetBySlug(string slug)
        {
            return _service.GetBySlug(slug);
        }

        [HttpPost]
        public ActionResult<Model.Post> Insert([FromBody] PostInsertRequest request)
        {
            return _service.Insert(request);
        }

        [HttpPut("{slug}")]
        public ActionResult<Model.Post> Update(string slug, [FromBody] PostUpdateRequest request)
        {
            var result = _service.Update(slug, request);
            if (result == null)
            {
                return BadRequest("Error finding or updating post");
            } 
            return result;
        }

        [HttpDelete]
        public ActionResult<object> Delete(string slug)
        {
            var result = _service.Delete(slug);
            if (result)
            {
                return Ok("Post removed");
            }
            return BadRequest("Could not find or delete post");
        }
    }
}