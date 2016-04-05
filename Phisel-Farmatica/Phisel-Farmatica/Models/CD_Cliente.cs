using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Phisel_Farmatica.Models
{
    public class CD_Cliente
    {
        public string header_padecimientos = "Historial_padecimientos";
        public string header_prioridad = "Prioridad";
        private string error = "Error al intentar ejecutar el procedimiento almacenado: ";

        public DataRow obtenerDatos(int pIdUsuario)
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();
            string procedimiento = "obtenerDatosCliente";

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = procedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter IdUsuario = new SqlParameter();
                IdUsuario.ParameterName = "@IdUsuario";
                IdUsuario.SqlDbType = SqlDbType.Int;
                IdUsuario.Value = pIdUsuario;
                SqlComando.Parameters.Add(IdUsuario);

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }

            catch (SqlException ex)
            {
                TablaDatos = null;
                throw new Exception(error + procedimiento + "\n" + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return TablaDatos.Rows[0];
        }
    }
}