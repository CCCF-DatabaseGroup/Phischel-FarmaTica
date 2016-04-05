using System;
using System.Data;
using System.Data.SqlClient;

namespace Phisel_Farmatica.Models
{
    public class CD_Usuario
    {
        // Id_usuario, Nickname, Contrasena, Correo_electronico, Id_rango_TU
        public string header_idUsuario = "Id_usuario";
        public string header_nickname = "Nickname";
        public string header_contrasena = "Contrasena";
        public string header_correo_electronico = "Correo_electronico";
        public string header_id_rango_TU = "Id_rango_TU";
        private string error = "Error al intentar ejecutar el procedimiento almacenado: ";

        public CD_Usuario()
        {

        }

        public int obtenerIdUsuario(string pNickname, string pContrasena)
        {
            SqlConnection SqlConexion = new SqlConnection();
            string procedimiento = "obtenerIdUsuario";
            int id_usuario = 1;

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = procedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter NickName = new SqlParameter();
                NickName.ParameterName = "@nickname";
                NickName.SqlDbType = SqlDbType.VarChar;
                NickName.Value = pNickname;
                SqlComando.Parameters.Add(NickName);

                SqlParameter Contrasena = new SqlParameter();
                Contrasena.ParameterName = "@contrasena";
                Contrasena.SqlDbType = SqlDbType.VarChar;
                Contrasena.Value = pContrasena;
                SqlComando.Parameters.Add(Contrasena);

                SqlParameter IdUsuario = new SqlParameter();
                IdUsuario.ParameterName = "@idUsuario";
                IdUsuario.SqlDbType = SqlDbType.Int;
                IdUsuario.Direction = ParameterDirection.Output;
                SqlComando.Parameters.Add(IdUsuario);

                SqlComando.ExecuteNonQuery();

                id_usuario = (int)SqlComando.Parameters["@idUsuario"].Value;
            }

            catch (Exception ex)
            {
                throw new Exception(error + procedimiento + "\n" + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return id_usuario;
        }

        public DataRow obtenerUsuario(string pNickname, string pContrasena)
        {
            SqlConnection SqlConexion = new SqlConnection();
            DataTable TablaDatos = new DataTable();
            string procedimiento = "obtenerUsuario";

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = procedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter NickName = new SqlParameter();
                NickName.ParameterName = "@nickname";
                NickName.SqlDbType = SqlDbType.VarChar;
                NickName.Value = pNickname;
                SqlComando.Parameters.Add(NickName);

                SqlParameter Contrasena = new SqlParameter();
                Contrasena.ParameterName = "@contrasena";
                Contrasena.SqlDbType = SqlDbType.VarChar;
                Contrasena.Value = pContrasena;
                SqlComando.Parameters.Add(Contrasena);

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }

            catch (Exception ex)
            {
                throw new Exception(error + procedimiento + "\n" + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            if (TablaDatos.Rows.Count > 0)
            {
                return TablaDatos.Rows[0];
            }
            else
            {
                return null;
            }

            
        }


    }
}
