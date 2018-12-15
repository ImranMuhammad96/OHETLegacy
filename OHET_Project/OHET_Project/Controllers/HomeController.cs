using OHET_Project.BBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OHET_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            FacebookData fbData = new FacebookData();
            fbData.GetFbPosts();

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

            return View();
        }
    }
}