using SaaS.EntityMis.Info;
using SaaS.Framework.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaaS.APIMis.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            Result result = new Result();
            result.Message = "Welcome to OtherPay！";
            return Json(result,JsonRequestBehavior.AllowGet);
        }


        
    }
}
