using System;
using System.Data;
using System.Data.SqlClient;

namespace Phisel_Farmatica.Models
{
    public class CD_Sucursales
    {
        public string header_idSucursal = "Id_sucursal";
        public string header_nombreSucursal = "Nombre_sucursal";
        public string header_idLocalidadSucursal = "Id_localidad_sucursal";
        private string error = "Error al intentar ejecutar el procedimiento almacenado: ";

        public CD_Sucursales()
        {

        }

        public DataTable mostrarSucursalesPorFarmacia(int pIdFarmacia)
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();
            string procedimiento = "mostrarSucursalesPorFarmacia";

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = procedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter IdFarmacia = new SqlParameter();
                IdFarmacia.ParameterName = "@IdFarmacia";
                IdFarmacia.SqlDbType = SqlDbType.Int;
                IdFarmacia.Value = pIdFarmacia;
                SqlComando.Parameters.Add(IdFarmacia);

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

            return TablaDatos;
        }

    }
}
