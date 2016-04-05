using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phisel_Farmatica.Models
{
    public class CD_Persona
    {
        public string header_cedula = "Cedula";
        public string header_nombre = "Nombre_persona";
        public string header_apellido1 = "Apellido1";
        public string header_apellido2 = "Apellido2";
        public string header_telefono = "Telefono_persona";
        public string header_fechaNacimiento = "Fecha_nacimiento";
        public string header_idDireccion = "Id_localidad_persona";
        private string error = "Error al intentar ejecutar el procedimiento almacenado: ";

        public CD_Persona()
        {

        }

        public DataRow obtenerDatos(int pIdUsuario)
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();
            string procedimiento = "obtenerDatosPersona";

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
