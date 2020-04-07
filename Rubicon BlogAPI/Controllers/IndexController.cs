using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Rubicon_BlogAPI.Controllers
{
    public class IndexController : Controller
    {
        //to set up redirection to swagger for published site
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/")]
        [Route("/docs")]
        public IActionResult Home()
        {
            return new RedirectResult("~/swagger/index.html");
        }
    }
}