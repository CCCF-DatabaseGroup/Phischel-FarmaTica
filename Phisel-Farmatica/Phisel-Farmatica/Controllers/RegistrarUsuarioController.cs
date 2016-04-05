using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phisel_Farmatica.Models;

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


        public JsonResult RegistrarUsuario(string pNickname, string pContrasena,string pCorreoElectronico)
        {

            Usuario usuario = new Usuario(pNickname, pContrasena, pCorreoElectronico);
            if (usuario.insertar())
            {
                return Json(new { Status = true}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status =  false}, JsonRequestBehavior.AllowGet);
        }

    }
}