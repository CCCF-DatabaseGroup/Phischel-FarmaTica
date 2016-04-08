using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phisel_Farmatica.Models;

namespace Phisel_Farmatica.Controllers
{
    public class HomeController : Controller
    {

        /**
        0: Administrador Total
        1: administrador De Phisel
        2: administrador De Farmatica
        3: Dependiente de Phisel
        4: Dependiente de Farmatica
        5: cliente
        */
        public const string USUARIO_ADMINISTRADOR_TOTAL = "Administrador total";
        public const string USUARIO_ADMINISTRADOR_FARMATICA = "Administrador de Farmatica";
        public const string USUARIO_DEPENDIENTE_FARMATICA = "Dependiente de Farmatica";
        public const string USUARIO_ADMINISTRADOR_PHISHEL = "Administrador de Phishel";
        public const string USUARIO_DEPENDIENTE_PHISHEL = "Dependiente de Phishel";
        public const string USUARIO_CLIENTE = "Cliente";
        public const string FARMACIA = "Farmacia";
        public const string FARMATICA = "Farmatica";
        public const string PHISHEL = "Phishel";
        public const string CARRITO = "Carrito";
        

        public const string PROVINCIA = "Provincia";

        public const string RANGO_USUARIO = "Nombre_rango";
        public const string ID_USUARIO = "Id_usuario";
        public const string CONNECTION_STRING = "workstation id=PhishelFarmatica.mssql.somee.com;packet size=4096;user id=crisrivlop_SQLLogin_1;pwd=4pwidhq39j;data source=PhishelFarmatica.mssql.somee.com;persist security info=False;initial catalog=PhishelFarmatica";

        // GET: Home
        public ActionResult Index(string a)
        {
            System.Diagnostics.Debug.WriteLine("\nIndex called");
            System.Diagnostics.Debug.WriteLine(Session["UserId"]);
            string rango = (string)Session[RANGO_USUARIO];
            if (rango != null && !USUARIO_CLIENTE.Equals(rango))
            {
                if (rango.Equals(USUARIO_DEPENDIENTE_FARMATICA) || rango.Equals(USUARIO_DEPENDIENTE_PHISHEL))
                {
                    return View("~/Views/Home/DependienteHome.cshtml");
                }
                else if (rango.Equals(USUARIO_ADMINISTRADOR_FARMATICA) || rango.Equals(USUARIO_ADMINISTRADOR_PHISHEL))
                {
                    return View("~/Views/Home/AdministradorHome.cshtml");
                }
                else
                {
                    return View("~/Views/Home/SuperAdministratorHome.cshtml");
                }

            }
            else return View("~/Views/Home/Index.cshtml");
        }




        [HttpPost]
        public JsonResult loginUser(string userId, string userPassword)
        {
            if (userId == null) userId = "";
            if (userPassword == null) userPassword = "";

            Usuario usuario = new Usuario(userId, userPassword);
            var messageReturn = "";            
            if (usuario.obtener())
            {
                System.Diagnostics.Debug.WriteLine("\nExiste un usuario");
                Session.Add(ID_USUARIO, usuario._IdUsuario);
                Session.Add(RANGO_USUARIO, usuario._RangoUsuario);
                if (usuario._RangoUsuario.Equals(USUARIO_DEPENDIENTE_FARMATICA) ||
                    usuario._RangoUsuario.Equals(USUARIO_ADMINISTRADOR_FARMATICA))
                {
                    Session.Add(FARMACIA, FARMATICA);
                }
                else if (usuario._RangoUsuario.Equals(USUARIO_DEPENDIENTE_PHISHEL) ||
                    usuario._RangoUsuario.Equals(USUARIO_ADMINISTRADOR_PHISHEL))
                {
                    Session.Add(FARMACIA, PHISHEL);
                }
                messageReturn = "Conectado";
            }
            else{
                System.Diagnostics.Debug.WriteLine("\nRayos!");
                messageReturn = "Contraseña o usuario invalidos";
            }
            return Json(new { Response = messageReturn }, JsonRequestBehavior.AllowGet);
        }




        public JsonResult getUserName()
        {
            return Json(new { UserId = Session[ID_USUARIO] }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult logoutUser()
        {
            Session.Clear();
            return Json(new { UserId = "" }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult obtenerPedidos()
        {
            if (Session[ID_USUARIO] == null)
            {
                return Json(new { } , JsonRequestBehavior.AllowGet);
            }
            else
            {
                int UserId = (int)Session[ID_USUARIO];
                Pedido pedido = new Pedido(UserId);
                List<object> toReturn = pedido.obtenerIterativamente().ToList();
                return Json(toReturn, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult obtenerListadeProductosdePedido(int PidPedido)
        {
            if (Session[ID_USUARIO] == null)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int UserId = (int)Session[ID_USUARIO];
                Pedido pedido = new Pedido(UserId);
                List<object> toReturn = pedido.obtenerIterativamente().ToList();
                return Json(toReturn, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult obtenerDependientes(int pIdSucursal)
        {
            if (Session[ID_USUARIO] == null)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Dependiente dependiente = new Dependiente(pIdSucursal);
                List<object> toReturn = dependiente.obtenerIterativamente().ToList();
                return Json(toReturn, JsonRequestBehavior.AllowGet);
            }

        }


    }
}
