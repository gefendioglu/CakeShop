using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;

namespace WebUI.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IHttpContextAccessor httpContextAccessor, ICompositeViewEngine compositeViewEngine) : base(httpContextAccessor, compositeViewEngine)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
