using System;
using System.Data;
using System.Data.SqlClient;

namespace Phisel_Farmatica.Models
{
    public class CD_Farmacias
    {
        public string header_idFarmacia = "Id_farmacia";
        private string error = "Error al intentar ejecutar el procedimiento almacenado: ";

        public DataRow obtenerFarmacia(int pFamacia)
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();
            string procedimiento = "obtenerFarmacia";

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = procedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter IdLocalidad = new SqlParameter();
                IdLocalidad.ParameterName = "@IdFarmacia";
                IdLocalidad.SqlDbType = SqlDbType.Int;
                IdLocalidad.Value = pFamacia;
                SqlComando.Parameters.Add(IdLocalidad);

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
