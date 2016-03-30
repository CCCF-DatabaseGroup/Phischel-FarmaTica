using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phisel_Farmatica.Controllers
{
    public class RegistrarUsuarioController : Controller
    {

        // GET: RegistrarUsuario
        public ActionResult Index()
        {
            if (Session[HomeController.RANGO_USUARIO] != null && 
                HomeController.USUARIO_ADMINISTRADOR_PHISHEL.Equals(Session[HomeController.RANGO_USUARIO]))
            {
                return View("~/Views/RegistrarUsuario/RegistrarPersonal.cshtml");
            }
            else if (Session[HomeController.RANGO_USUARIO] != null &&
                HomeController.USUARIO_ADMINISTRADOR_FARMATICA.Equals(Session[HomeController.RANGO_USUARIO]))
            {
                return View("~/Views/RegistrarUsuario/RegistrarPersonal.cshtml");
            }
            return View("~/Views/RegistrarUsuario/Index.cshtml");
        }
    }
}