using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rubicon_BlogAPI.Services;

namespace Rubicon_BlogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseGetController<TModel, TSearch> : ControllerBase
    {
        private readonly IBaseGetService<TModel, TSearch> _service;
        public BaseGetController(IBaseGetService<TModel, TSearch> service)
        {
            _service = service;
        }

        [HttpGet] 
        public virtual ActionResult<List<TModel>> Get([FromQuery]TSearch search)
        {
            return _service.Get(search);
        }

        [HttpGet("{id}")]
        public virtual ActionResult<TModel> GetById(string id)
        {
            return _service.GetById(id);
        }
    }
}