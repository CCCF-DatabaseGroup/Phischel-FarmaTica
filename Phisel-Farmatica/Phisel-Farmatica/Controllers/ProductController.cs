using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phisel_Farmatica.Controllers
{
    public class ProductController : Controller
    {
        public const string NOMBRE_SUCURSAL = "Nombre_sucursal";
        public const string DISTRITO = "Distrito";
        public const string CANTON = "Canton";

        public const string NOMBRE_PRODUCTO = "Nombre_producto";
        public const string DESCRIPCION_PRODUCTO = "Descripcion";
        public const string PRESCRIPCION_PRODUCTO = "Prescripcion";
        public const string CANTIDAD_BODEGA = "Cantidad_bodega";
        public const string PRECIO_PRODUCTO = "Precio_sucursal";

        private SqlConnection sqlcon;
        private SqlCommand sqlcommand;
        private SqlDataReader sqlreader;
        public const string NOMBRE_CATEGORIA = "Nombre_categoria";
        // GET: Product
        public ActionResult Index()
        {
            if (Session[HomeController.FARMACIA] == null)
            {
                Session.Add(HomeController.FARMACIA, HomeController.FARMATICA);
            }
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


        /**
        Hace un query a la base de datos
            recibe el query y retorna un SqlDataReader con el resultado de la base de datos
        */
        private SqlDataReader doQuery(string pQuery)
        {
            System.Diagnostics.Debug.WriteLine(pQuery);
            try
            {
                sqlcon = new SqlConnection(HomeController.CONNECTION_STRING);
                sqlcon.Open();
                System.Diagnostics.Debug.WriteLine(pQuery);
                sqlcommand = new SqlCommand(pQuery, sqlcon);
                sqlreader = sqlcommand.ExecuteReader();
                return sqlreader;

            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine("\nRayos!");
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return null;
            }
        }

        [HttpPost]
        public JsonResult modificarProvincia(string pProvincia)
        {
            System.Diagnostics.Debug.WriteLine(pProvincia);
            Session.Add(HomeController.PROVINCIA, pProvincia);
            return Json(new { EstadoSolicitud = true }, JsonRequestBehavior.AllowGet);
        }

        /**
            Se solicita la lista completa de sucursales
        */
        public JsonResult obtenerListaSucursal()
        {

            /**
            Formato Json:
            [{Nombre:"",Canton:"",Distrito:""},...,{Nombre:"",Canton:"",Distrito:""}]
            */
            System.Diagnostics.Debug.WriteLine("Obteniendo sucursales...");
            List<object> sucursales = new List<object>();
            sqlreader = doQuery("exec obtenerSucursalesEnProvinciaPorFarmacia '" + Session[HomeController.PROVINCIA] +"', '"+ Session[HomeController.FARMACIA] +  "';");
            if (sqlreader != null)
            {
                System.Diagnostics.Debug.WriteLine("reader es diferente de null");
                while (sqlreader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("se puede leer");
                    sucursales.Add(new
                    {
                        Nombre = (string)sqlreader[NOMBRE_SUCURSAL],
                        Canton = (string)sqlreader[CANTON],
                        Distrito = (string)sqlreader[DISTRITO]
                    });
                }
            }
            sqlcon.Close();
            return Json(sucursales, JsonRequestBehavior.AllowGet);
        }


        /**
            Se solicita la lista completa de las categorias de producto
        */
        public JsonResult obtenerListaCategoria()
        {

            /**
            Formato Json:
            [{Nombre:"",Ubicacion:""},...,{Nombre:"",Ubicacion:""}]
            */
            List<object> categorias = new List<object>();
            sqlreader = doQuery("exec obtenerCategoriaProducto");
            if (sqlreader != null)
            {
                System.Diagnostics.Debug.WriteLine("reader es diferente de null");
                while (sqlreader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("se puede leer");
                    categorias.Add(new
                    {
                        Nombre = (string)sqlreader[NOMBRE_CATEGORIA],
                    });
                }
            }
            sqlcon.Close();
            return Json(categorias, JsonRequestBehavior.AllowGet);
        }


        /**
            Recibe 10 productos, solicita los primeros diez con el pProductoIntervalo = 1,
            los segundos 10 son con pProductoIntervalo=11; osea que pProductoIntervalo cumple con
            la funcion matematica pProductoIntervalo=10*k+1, con k un numero entero positivo
        */
        [HttpPost]
        public JsonResult obtenerProducto(string pSucursal,string pCategoria)
        {
            /**
            Formato Json:
                1) [{EstadoSolicitud:true},{CodigoDeProducto:"", Nombre:"", CantidadDisponible:""},...,
                    {CodigoDeProducto:"", Nombre:"", CantidadDisponible:""}
                    ]
                2) [{EstadoSolicitud:false}]
            */
            List<object> productos = new List<object>();
            sqlreader = doQuery("exec obtenerProductoDeSucursal '"+ Session[HomeController.FARMACIA] + "', '" + pSucursal + "', '" + pCategoria +"';");
            if (sqlreader != null)
            {
                System.Diagnostics.Debug.WriteLine("reader es diferente de null");
                while (sqlreader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("se puede leer");
                    productos.Add(new
                    {
                        Nombre = (string)sqlreader[NOMBRE_PRODUCTO],
                        Descripcion = (string)sqlreader[DESCRIPCION_PRODUCTO],
                        Prescripcion = ((bool)sqlreader[PRESCRIPCION_PRODUCTO])?"Si":"No",
                        Cantidad = (int)sqlreader[CANTIDAD_BODEGA],
                        Precio = sqlreader[PRECIO_PRODUCTO]
                    });
                }
            }
            sqlcon.Close();
            return Json(productos, JsonRequestBehavior.AllowGet);
        }

        /**
            actualizarProducto: Recibe la lista de las caracteristicas de un producto y las actualiza
            Retorna el siguiente formato Json
            Formato Json:
                1) {EstadoSolicitud:true}
                2) {EstadoSolicitud:false}
            La primera para actualizacion exitosa y la segunda para actualizacion fallida
        */
        [HttpPost]
        public JsonResult actualizarProducto(/*Datos del Producto*/)
        {
            SqlDataReader reader = doQuery("");
            if (reader != null)
            {

            }
            else
            {

            }
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        /**
            eliminarProducto: Recibe el codigo del producto a eliminar
            Formato Json:
                1) {EstadoSolicitud:true}
                2) {EstadoSolicitud:false}
            La primera para eliminacion exitosa y la segunda para eliminacion fallida
        */
        public JsonResult eliminarProducto(/*Producto*/)
        {
            SqlDataReader reader = doQuery("");
            if (reader != null)
            {

            }
            else
            {

            }
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }


        /**
            agregarProducto: Recibe el nombre del producto, sucursal en la que se encuentra etc..
            retorna el siguiente formato Json:
                1) {EstadoSolicitud:true}
                2) {EstadoSolicitud:false}
            La primera para Insercion exitosa y la segunda para Insercion fallida
        */
        [HttpPost]
        public JsonResult agregarProducto(/*Producto*/)
        {
            SqlDataReader reader = doQuery("");
            if (reader != null)
            {

            }
            else
            {

            }
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }

        /**
            obtenerCantidadDeProducto: Obtiene la cantidad de productos de la farmacia selecionada
                Formato Json:
                1) {EstadoSolicitud:true,CantidadProducto:0}
                2) {EstadoSolicitud:false}
        */
        public JsonResult obtenerCantidadDeProducto()
        {
            SqlDataReader reader = doQuery("");
            if (reader != null)
            {

            }
            else
            {

            }
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }


        /**
            agregarCantidadProductoSucursal: Agrega productos disponibles en la sucursal
                Formato Json:
                1) {EstadoSolicitud:true,CantidadProducto:0}
                2) {EstadoSolicitud:false}
        */
        [HttpPost]
        public JsonResult agregarCantidadProductoSucursal(int pIdProducto,int pCantidadProducto)
        {
            SqlDataReader reader = doQuery("");
            if (reader != null)
            {

            }
            else
            {

            }
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }


        /**
            agregarAlCarrito: agrega al carrito un producto
        */
        [HttpPost]
        public JsonResult agregarAlCarrito(int pProductoId)
        {
            SqlDataReader reader = doQuery("");
            if (reader != null)
            {

            }
            else
            {

            }
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }

        /**
            eliminarDelCarrito: elimina del carrito un producto
        */
        [HttpPost]
        public JsonResult eliminarDelCarrito(int pProductoId)
        {
            SqlDataReader reader = doQuery("");
            if (reader != null)
            {

            }
            else
            {

            }
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }

        /**
            cambiarCantidadProductoEnCarrito: Cambia la cantidad del producto en el carrito
        */
        [HttpPost]
        public JsonResult cambiarCantidadProductoEnCarrito(int pProductoId,int pCantidad)
        {
            SqlDataReader reader = doQuery("");
            if (reader != null)
            {

            }
            else
            {

            }
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }


        /**
         comprarCarrito los objetos del carrito, retorna la factura
        */
        [HttpPost]
        public JsonResult comprarCarrito()
        {
            SqlDataReader reader = doQuery("");
            if (reader != null)
            {

            }
            else
            {

            }
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }


        /**
         Pepara la factura, calcula los productos disponibles, etc
        */
        [HttpPost]
        public JsonResult prepararFactura()
        {
            SqlDataReader reader = doQuery("");
            if (reader != null)
            {

            }
            else
            {

            }
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }


        /**
       Si pFarmacia positivo entonces Farmatica si no Phishel
       */
        [HttpPost]
        public JsonResult modificarFarmacia(bool pFarmacia)
        {
            /**
            Si es un usuario cliente o una sesion nula puede modificar el valor de la farmacia
            */
            if ((HomeController.USUARIO_CLIENTE.Equals(Session[HomeController.RANGO_USUARIO])
                || Session[HomeController.RANGO_USUARIO] == null))
            {
                if (pFarmacia)
                {
                    Session[HomeController.FARMACIA] = HomeController.FARMATICA;
                }
                else
                {
                    Session[HomeController.FARMACIA] = HomeController.PHISHEL;
                }
                return Json(new { EstadoSolicitud = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { EstadoSolicitud = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult estaFarmaticaActiva()
        {
            bool esFarmaticaActiva = false;
            if (HomeController.FARMATICA.Equals(Session[HomeController.FARMACIA])) esFarmaticaActiva = true;
            return Json(new { EstadoSolicitud = esFarmaticaActiva }, JsonRequestBehavior.AllowGet);
        }
    }
}