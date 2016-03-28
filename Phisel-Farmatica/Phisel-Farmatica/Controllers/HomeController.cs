using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public const int USUARIO_ADMINISTRADOR = 1;
        public const int USUARIO_DOCTOR = 2;
        public const int USUARIO_DEPENDIENTE = 3;
        public const int USUARIO_CLIENTE = 4;

        public const string TIPO_DE_USUARIO = "Tipo_usuario";
        public const string NICKNAME_USUARIO = "Nickname";
        public const string CONSTRASENIA_USUARIO = "Contrasena";
        public const string CORREO_ELECTRONICO_USUARIO = "Correo_electronico";
        public const string TABLA_USUARIO = "USUARIO";
        public const string CONNECTION_STRING = "Data Source=GAMER-PC\\UNDERGROUND;Initial Catalog = Test2; Integrated Security = True";

        // GET: Home
        public ActionResult Index(string a)
        {
            System.Diagnostics.Debug.WriteLine("\nIndex called");
            System.Diagnostics.Debug.WriteLine(Session["UserId"]);
            if (Session[NICKNAME_USUARIO] != null)
            {
                return View("~/Views/Home/Index2.cshtml");
            }
            else return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult Index2(string data)
        {
            System.Diagnostics.Debug.WriteLine("\nIndex 2 called");
            return View("~/Views/Home/Index2.cshtml");
        }
        private string getUserConsultString(string userId, string userPassword)
        {
            return "SELECT " +
                CONSTRASENIA_USUARIO + ","
                + NICKNAME_USUARIO + ","
                + CORREO_ELECTRONICO_USUARIO + ","
                +  TIPO_DE_USUARIO +
                " FROM " +  TABLA_USUARIO + " WHERE ( \'" 
                + userId + "\' = " + NICKNAME_USUARIO +  " OR \'" 
                + userId + "\' =" + CORREO_ELECTRONICO_USUARIO + ") AND \'" + userPassword
                + "\' = "+  CONSTRASENIA_USUARIO +";";
        }

        [HttpPost]
        public JsonResult loginUser(string userId, string userPassword)
        {
            SqlConnection sqlcon;
            SqlCommand sqlcommand;
            SqlDataReader sqlreader;
            string messageReturn = "";
            try
            {
                sqlcon = new SqlConnection(CONNECTION_STRING);
                sqlcon.Open();
                string commando = getUserConsultString(userId, userPassword);
                System.Diagnostics.Debug.WriteLine(commando);
                sqlcommand = new SqlCommand(commando,sqlcon);

                sqlreader = sqlcommand.ExecuteReader();
                if (sqlreader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("\nExiste un usuario");
                    Session.Add(NICKNAME_USUARIO, sqlreader[NICKNAME_USUARIO]);
                    Session.Add(TIPO_DE_USUARIO, sqlreader[TIPO_DE_USUARIO]);
                    Session.Add(CORREO_ELECTRONICO_USUARIO, sqlreader[CORREO_ELECTRONICO_USUARIO]);
                    messageReturn = "Conectado";
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("\nRayos!");
                    messageReturn = "Contraseña o usuario invalidos";
                }
                sqlcon.Close();
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine("\nRayos!");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return Json(new { Response = messageReturn}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getUserName()
        {
            return Json(new { UserId = (string)Session[NICKNAME_USUARIO] }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult logoutUser()
        {
            Session.Clear();
            return Json(new { UserId = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}
