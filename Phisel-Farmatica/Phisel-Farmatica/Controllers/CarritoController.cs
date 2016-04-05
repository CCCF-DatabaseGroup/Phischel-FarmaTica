using Phisel_Farmatica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phisel_Farmatica.Controllers
{
    public class CarritoController : Controller
    {

        class SucursalContainer
        {
            public string Sucursal { get; set; }
            public List<object> ListaSucursal { get; set; }

        }

        // GET: Carrito
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerCarrito()
        {
            object carrito = Session[HomeController.CARRITO];
            if (carrito == null)
            {
                Session.Add(HomeController.CARRITO, new List<Producto>());
                carrito = Session[HomeController.CARRITO];
            }

            List<object> carritoMapeado = new List<object>();
            List<SucursalContainer> sucursales = new List<SucursalContainer>();
            bool agregar;
            foreach (Producto productoConsultado in ((List<Producto>)carrito))
            {
                agregar = true;
                foreach (SucursalContainer sucursal in sucursales)
                {
                    if (sucursal.Sucursal == productoConsultado._NombreSucursal)
                    {
                        agregar = false;
                        break;
                    }
                }
                if (agregar)
                {
                    SucursalContainer sc = new SucursalContainer();
                    sc.Sucursal = productoConsultado._NombreSucursal;
                    sc.ListaSucursal = new List<object>();
                    foreach (Producto productoConsultado2 in ((List<Producto>)carrito))
                    {
                        if (productoConsultado._NombreSucursal.Equals(productoConsultado2._NombreSucursal))
                            sc.ListaSucursal.Add(productoConsultado.obtenerJson());
                    }
                    sucursales.Add(sc);
                    carritoMapeado.Add(new { Sucursal = sc.Sucursal, ListaSucursal = sc.ListaSucursal.ToList()});
                }
                
                    
            }

            return Json(carritoMapeado, JsonRequestBehavior.AllowGet);
        }




        /**
            eliminarDelCarrito: elimina del carrito un producto
        */
        [HttpPost]
        public JsonResult eliminarDelCarrito(int pProductoId,string pSucursal)
        {
            if (pSucursal == null)
            {
                pSucursal = "";
            }
            object carrito = Session[HomeController.CARRITO];
            if (carrito == null)
            {
                Session.Add(HomeController.CARRITO, new List<Producto>());
                carrito = Session[HomeController.CARRITO];
            }
            foreach (Producto producto in ((List<Producto>)carrito))
            {
                if (producto._IdProducto == pProductoId && pSucursal.Equals(producto._NombreSucursal))
                {
                    ((List<Producto>)carrito).Remove(producto);
                    break;
                }
            }
            return ObtenerCarrito();
        }


        /**
            cambiarCantidadProductoEnCarrito: Cambia la cantidad del producto en el carrito
        */
        [HttpPost]
        public JsonResult cambiarCantidadProductoEnCarrito(int pProductoId, int pCantidad)
        {
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }


        /**
         comprarCarrito los objetos del carrito, retorna la factura
        */
        [HttpPost]
        public JsonResult comprarCarrito()
        {
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }


        /**
         Pepara la factura, calcula los productos disponibles, etc
        */
        [HttpPost]
        public JsonResult prepararFactura()
        {
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}