using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hsr.Core.Cache;
using Hsr.Data.Interface;
using Hsr.Models;

namespace Hsr.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IRepository<Ryan> _repository;
        public readonly ICacheManager _CacheManager;
        public HomeController(IRepository<Ryan> repository, ICacheManager cacheManager)
        {
            _repository = repository;
            _CacheManager = cacheManager;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }
    }
}
