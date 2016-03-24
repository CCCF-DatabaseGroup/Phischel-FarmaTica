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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getUserData(string userId,string userPassword)
        {
            var algo = new { Algo = "true" };
            return Json(algo,JsonRequestBehavior.AllowGet);
        }
    }
}