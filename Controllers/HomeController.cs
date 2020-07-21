using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoCallConsultant.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string code)
        {
            Session["code"] = code;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.test = "test";
            return View();
        }
    }
}