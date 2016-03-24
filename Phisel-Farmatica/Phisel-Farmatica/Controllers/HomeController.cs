using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phisel_Farmatica.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string a)
        {
            System.Diagnostics.Debug.WriteLine("\nIndex called");
            System.Diagnostics.Debug.WriteLine(Session["UserId"]);
            if (Session["UserId"] != null)
            {
                return View("~/Views/Home/Index2.cshtml");
            }
            else return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult Index2(string data)
        {
            System.Diagnostics.Debug.WriteLine("\nIndex 2 called");
            return View("~/Views/Home/Index2.cshtml");
        }

        [HttpPost]
        public JsonResult getUserData(string userId,string userPassword)
        {
            System.Diagnostics.Debug.WriteLine("\nFUNNNNNNNNNNNNNNNNNNNNNNNNNNN");
            var algo = new { Algo = "a" };
            Session.Add("UserId", userId);
            Session.Add("Password", userPassword);
            return Json(algo,JsonRequestBehavior.AllowGet);
        }
        public JsonResult getUserName()
        {
            return Json(new { UserId = (string)Session["UserId"]}, JsonRequestBehavior.AllowGet);
        }
    }
}