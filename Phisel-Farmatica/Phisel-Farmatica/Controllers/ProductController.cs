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
            if (HomeController.USUARIO_CLIENTE.Equals(Session[HomeController.RANGO_USUARIO]))
            {
                return View("~/Views/Product/CompraProducto.cshtml");
            }
            else if (HomeController.USUARIO_DEPENDIENTE_FARMATICA.Equals(Session[HomeController.RANGO_USUARIO]))
            {
                return View("~/Views/Product/CompraProducto.cshtml");
            }
            else if (HomeController.USUARIO_DEPENDIENTE_PHISHEL.Equals(Session[HomeController.RANGO_USUARIO]))
            {
                return View("~/Views/Product/CompraProducto.cshtml");
            }
            else if (HomeController.USUARIO_ADMINISTRADOR_PHISHEL.Equals(Session[HomeController.RANGO_USUARIO]))
            {
                return View("~/Views/Product/AdministrarProductos.cshtml");
            }
            else if (HomeController.USUARIO_ADMINISTRADOR_FARMATICA.Equals(Session[HomeController.RANGO_USUARIO]))
            {
                return View("~/Views/Product/AdministrarProductos.cshtml");
            }
            else if (HomeController.USUARIO_ADMINISTRADOR_TOTAL.Equals(Session[HomeController.RANGO_USUARIO]))
            {
                return View("~/Views/Product/AdministrarProductos.cshtml");
            }
            return View();
        }
    }
}