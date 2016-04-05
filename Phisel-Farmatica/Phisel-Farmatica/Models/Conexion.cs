using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Phisel_Farmatica.Models
{
    public class Conexion

    {
        //public static String BasedeDatos = "Data Source=PhishelFarmatica.mssql.somee.com;Initial Catalog=PhishelFarmatica;Persist Security Info=True;User ID=crisrivlop_SQLLogin_1;Password=4pwidhq39j";
        public static String BasedeDatos = "Data Source=.;Initial Catalog=PhishelFarmatica;Integrated Security=True";

        public String ChequearConexion()
        {
            String mensaje = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();
                mensaje = "Y";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                SqlConexion.Close();
            }

            return mensaje;
        }
    }
}