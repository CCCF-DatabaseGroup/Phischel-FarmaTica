﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Phisel_Farmatica.Models;

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
                return View();
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
            string provincia = (string)Session[HomeController.PROVINCIA];
            string farmacia = (string)Session[HomeController.FARMACIA];
            System.Diagnostics.Debug.WriteLine("Farmacia " +provincia);
            System.Diagnostics.Debug.WriteLine("Provincia " + farmacia);
            Sucursal sucursal = new Sucursal(provincia, farmacia);
            var sucursales = sucursal.obtenerIterativamente().ToList();
            return Json(sucursales, JsonRequestBehavior.AllowGet);
        }


        /**
            Se solicita la lista completa de las categorias de producto
        */
        public JsonResult obtenerListaCategoria()
        {

            /**
            Formato Json:
            [{Nombre:""},...,{Nombre:""}]
            */
            CategoriaProducto categoria = new CategoriaProducto();
            var categorias = categoria.obtenerIterativamente().ToList();
            return Json(categorias, JsonRequestBehavior.AllowGet);
        }


        /**
            Recibe 10 productos, solicita los primeros diez con el pProductoIntervalo = 1,
            los segundos 10 son con pProductoIntervalo=11; osea que pProductoIntervalo cumple con
            la funcion matematica pProductoIntervalo=10*k+1, con k un numero entero positivo
        */
        [HttpPost]
        public JsonResult obtenerProductoEnSucursal(string pSucursal,string pCategoria)
        {
            /**
            Formato Json:
                1) [{EstadoSolicitud:true},{CodigoDeProducto:"", Nombre:"", CantidadDisponible:""},...,
                    {CodigoDeProducto:"", Nombre:"", CantidadDisponible:""}
                    ]
                2) [{EstadoSolicitud:false}]
            */
            if (pSucursal == null)
            {
                pSucursal = "";
            }
            if (pCategoria == null) pCategoria = "";

            Producto producto = new Producto((string)Session[HomeController.FARMACIA], pSucursal, pCategoria);

            List<object> productos = producto.obtenerIterativamente().ToList();
            //sqlreader = doQuery("exec obtenerProductoDeSucursal '"+ Session[HomeController.FARMACIA] + "', '" + pSucursal + "', '" + pCategoria +"';");
            return Json(productos, JsonRequestBehavior.AllowGet);
        }



        public JsonResult obtenerTodosProducto()
        {
            Producto producto = new Producto();

            List<object> productos = producto.obtenerTodos().ToList();
            //sqlreader = doQuery("exec obtenerProductoDeSucursal '"+ Session[HomeController.FARMACIA] + "', '" + pSucursal + "', '" + pCategoria +"';");
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
        public JsonResult editarProducto(int pIdProducto,string pNombreProducto, int pIdCategoria, int pIdLaboratorio, bool pPreescripcion, string pDescripcion)
        {
            Producto producto = new Producto(pIdProducto, pNombreProducto, pIdCategoria, pIdLaboratorio, pPreescripcion, pDescripcion);
            producto.editar();
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
        public JsonResult registrarProducto(string pNombreProducto, int pIdCategoria,int pIdLaboratorio,bool pPreescripcion, string pDescripcion)
        {
            Producto producto = new Producto(pNombreProducto, pIdCategoria, pIdLaboratorio,pPreescripcion, pDescripcion);
            producto.insertar();
            //Pendiente actualizacion de datos, tiene que hacerse desde AngularJs
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
            
            return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
        }


        /**
            agregarAlCarrito: agrega al carrito un producto
        */
        [HttpPost]
        public JsonResult agregarAlCarrito(string pSucursal, string pFarmacia,int pProductoId)
        {
            Producto producto = new Producto(pProductoId, (string)Session[HomeController.FARMACIA], pSucursal);
            object carrito = Session[HomeController.CARRITO];
            if (carrito == null)
            {
                Session.Add(HomeController.CARRITO, new List<Producto>());
                carrito = Session[HomeController.CARRITO];
            }
            ((List<Producto>)carrito).Add(producto);
            return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
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


        public JsonResult obtenerListaLaboratorio()
        {
            Laboratorio laboratorio = new Laboratorio();
            List<object> laboratorios= laboratorio.obtenerIterativamente().ToList();
            return Json(laboratorios, JsonRequestBehavior.AllowGet);
        }


        public JsonResult estaFarmaticaActiva()
        {
            bool esFarmaticaActiva = false;
            if (HomeController.FARMATICA.Equals(Session[HomeController.FARMACIA])) esFarmaticaActiva = true;
            return Json(new { EstadoSolicitud = esFarmaticaActiva }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult test(List<int> ptest)
        {
            int obj = (int)ptest[0];
            System.Diagnostics.Debug.WriteLine("testeando test...");
            System.Diagnostics.Debug.WriteLine(ptest);

            return Json(new { EstadoSolicitud = true }, JsonRequestBehavior.AllowGet);
        }

    }
}