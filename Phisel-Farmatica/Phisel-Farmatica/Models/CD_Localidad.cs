using System;
using System.Data;
using System.Data.SqlClient;

namespace Phisel_Farmatica.Models
{
    public class CD_Localidad
    {
        public string header_nombreProvincia = "Nombre_provincia";
        public string header_idProvincia = "Id_provincia";
        public string header_nombreCanton = "Nombre_canton";
        public string header_idCanton = "Id_canton";
        public string header_nombreDistrito = "Nombre_distrito";
        public string header_idDistrito = "Id_distrito";
        private string error = "Error al intentar ejecutar el procedimiento almacenado: ";


        public CD_Localidad()
        {

        }

        public DataRow obtenerDireccion(int pIdLocalidad)
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();
            string procedimiento = "obtenerDireccion";

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = procedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter IdLocalidad = new SqlParameter();
                IdLocalidad.ParameterName = "@IdLocalidad";
                IdLocalidad.SqlDbType = SqlDbType.Int;
                IdLocalidad.Value = pIdLocalidad;
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

        public int obtenerIdLocalidad(int pProvincia, int pCanton, int pDistrito)
        {
            int idLocalidad = 0;
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "obtenerIdLocalidad";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter IdProvincia = new SqlParameter();
                IdProvincia.ParameterName = "@IdProvincia";
                IdProvincia.SqlDbType = SqlDbType.Int;
                IdProvincia.Value = pProvincia;
                SqlComando.Parameters.Add(IdProvincia);

                SqlParameter IdCanton = new SqlParameter();
                IdCanton.ParameterName = "@IdCanton";
                IdCanton.SqlDbType = SqlDbType.Int;
                IdCanton.Value = pCanton;
                SqlComando.Parameters.Add(IdCanton);

                SqlParameter IdDistrito = new SqlParameter();
                IdDistrito.ParameterName = "@IdDistrito";
                IdDistrito.SqlDbType = SqlDbType.Int;
                IdDistrito.Value = pDistrito;
                SqlComando.Parameters.Add(IdDistrito);

                SqlParameter IdLocalidad = new SqlParameter();
                IdLocalidad.ParameterName = "@IdLocalidad";
                IdLocalidad.SqlDbType = SqlDbType.Int;
                IdLocalidad.Direction = ParameterDirection.Output;
                SqlComando.Parameters.Add(IdLocalidad);

                SqlComando.ExecuteNonQuery();

                idLocalidad = (int)SqlComando.Parameters["@IdLocalidad"].Value;
            }

            catch (Exception ex)
            {
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado Produccion.TamañoProductos. " + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return idLocalidad;
        }      

        public DataTable mostrarProvincias()
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "mostrarProvincias";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }

            catch (Exception ex)
            {
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado Produccion.MostrarProductos. " + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return TablaDatos;
        }       

        public DataTable mostrarCantones(int pProvincia)
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "mostrarCantones";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter IdProvincia = new SqlParameter();
                IdProvincia.ParameterName = "@IdProvincia";
                IdProvincia.SqlDbType = SqlDbType.Int;
                IdProvincia.Value = pProvincia;
                SqlComando.Parameters.Add(IdProvincia);

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }

            catch (Exception ex)
            {
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado Produccion.MostrarProductos. " + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return TablaDatos;
        }

        public DataTable mostrarDistritos(int pProvincia, int pCanton)
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "mostrarDistritos";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter IdProvincia = new SqlParameter();
                IdProvincia.ParameterName = "@IdProvincia";
                IdProvincia.SqlDbType = SqlDbType.Int;
                IdProvincia.Value = pProvincia;
                SqlComando.Parameters.Add(IdProvincia);

                SqlParameter IdCanton = new SqlParameter();
                IdCanton.ParameterName = "@IdCanton";
                IdCanton.SqlDbType = SqlDbType.Int;
                IdCanton.Value = pCanton;
                SqlComando.Parameters.Add(IdCanton);

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }

            catch (Exception ex)
            {
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado Produccion.MostrarProductos. " + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return TablaDatos;
        }

    }
}
