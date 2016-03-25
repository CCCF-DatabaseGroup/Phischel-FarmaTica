using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phisel_Farmatica.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            if (HomeController.USUARIO_CLIENTE.Equals(Session[HomeController.TIPO_DE_USUARIO]))
            {
                return View("~/Views/Product/CompraProducto.cshtml");
            }
            else if (HomeController.USUARIO_DEPENDIENTE.Equals(Session[HomeController.TIPO_DE_USUARIO]))
            {
                return View("~/Views/Product/CompraProducto.cshtml");
            }
            else if (HomeController.USUARIO_ADMINISTRADOR.Equals(Session[HomeController.TIPO_DE_USUARIO]))
            {
                return View("~/Views/Product/AdministrarProductos.cshtml");
            }
            return View();
        }
    }
}