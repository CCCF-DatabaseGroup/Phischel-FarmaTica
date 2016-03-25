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
            if (Session[HomeController.TIPO_DE_USUARIO] != null && 
                HomeController.USUARIO_ADMINISTRADOR == (int)Session[HomeController.TIPO_DE_USUARIO])
            {
                return View("~/Views/RegistrarUsuario/RegistrarPersonal.cshtml");
            }
            return View("~/Views/RegistrarUsuario/Index.cshtml");
        }
    }
}