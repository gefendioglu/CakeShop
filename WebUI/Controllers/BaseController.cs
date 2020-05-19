using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace WebUI.Controllers
{
    public class BaseController : Controller
    {

        IHttpContextAccessor _httpContextAccessor;
        ICompositeViewEngine _compositeViewEngine;
        ISession _session;

            
        public BaseController(IHttpContextAccessor httpContextAccessor, ICompositeViewEngine compositeViewEngine)
        {
            _httpContextAccessor = httpContextAccessor;
            _compositeViewEngine = compositeViewEngine;
            _session = _httpContextAccessor.HttpContext.Session;
        }

    }
}