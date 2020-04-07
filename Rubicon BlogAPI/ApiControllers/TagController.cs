using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rubicon_BlogAPI.Services;

namespace Rubicon_BlogAPI.Controllers
{
    public class TagController : BaseGetController<Model.Tag, object>
    {
        public TagController(IBaseGetService<Model.Tag, object> service) : base(service)
        {
        }
    }
}