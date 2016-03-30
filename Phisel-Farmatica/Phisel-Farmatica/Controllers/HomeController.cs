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
        public const string USUARIO_ADMINISTRADOR_TOTAL = "Administrador total";
        public const string USUARIO_ADMINISTRADOR_FARMATICA = "Administrador de Farmatica";
        public const string USUARIO_DEPENDIENTE_FARMATICA = "Dependiente de Farmatica";
        public const string USUARIO_ADMINISTRADOR_PHISHEL = "Administrador de Phishel";
        public const string USUARIO_DEPENDIENTE_PHISHEL = "Dependiente de Phishel";
        public const string USUARIO_CLIENTE = "Cliente";
        public const string FARMACIA = "Farmacia";
        public const string FARMATICA = "Farmatica";
        public const string PHISHEL = "Phishel";

        public const string PROVINCIA = "Provincia";

        public const string RANGO_USUARIO = "Nombre_rango";
        public const string ID_USUARIO = "Id_usuario";
        public const string CONNECTION_STRING = "workstation id=PhishelFarmatica.mssql.somee.com;packet size=4096;user id=crisrivlop_SQLLogin_1;pwd=4pwidhq39j;data source=PhishelFarmatica.mssql.somee.com;persist security info=False;initial catalog=PhishelFarmatica";

        // GET: Home
        public ActionResult Index(string a)
        {
            System.Diagnostics.Debug.WriteLine("\nIndex called");
            System.Diagnostics.Debug.WriteLine(Session["UserId"]);
            if (Session[ID_USUARIO] != null)
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
            return "exec acceso '" + userId + "', '" + userPassword + "';";
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
                    Session.Add(ID_USUARIO, sqlreader[ID_USUARIO]);
                    Session.Add(RANGO_USUARIO, sqlreader[RANGO_USUARIO]);
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
            return Json(new { UserId = Session[ID_USUARIO] }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult logoutUser()
        {
            Session.Clear();
            return Json(new { UserId = "" }, JsonRequestBehavior.AllowGet);
        }


    }
}
