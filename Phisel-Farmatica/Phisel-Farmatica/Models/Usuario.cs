using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phisel_Farmatica.Models
{
    public class Usuario : DatabaseConnectorObject
    {

        public string _Nickname { get; set; }
        public string _CorreoElectronico { get; set; }
        public string _Contrasena { get; set; }
        public int _IdUsuario { get; set; }
        public string _RangoUsuario { get; set; }
        public bool _esValido { get; set; }

        public const string NICKNAME_PARAM = "@usuario";
        public const string NICKNAME_INSERT_PARAM = "@nickname";
        public const string ID_USUARIO_INSERT_PARAM = "@idUsuario";
        public const string CONTRASENA_PARAM = "@Contrasena";
        public const string CORREO_ELECTRONICO_PARAM = "@correo_electronico";


        public const string ID_USUARIO_HEADER = "Id_usuario";

        public const string PROCEDIMIENTO = "acceso";
        public const string PROCEDIMIENTO_INSERCION = "RegistrarUsuario";


        private Usuario(int pIdUsuario, string pRangoUsuario)
        {
            _IdUsuario = pIdUsuario;
            _RangoUsuario = pRangoUsuario;
        }

        public Usuario(string pNickname, string pContrasena, string pCorreoElectronico)
        {
            this._Nickname = pNickname;
            this._Contrasena = pContrasena;
            this._CorreoElectronico = pCorreoElectronico;
        }


        public Usuario(string pNickname,string pContrasena)
        {
            _esValido = false;
            
            this._Nickname = pNickname;
            this._Contrasena = pContrasena;


            
        }

        protected override object contextualizar(DataRow pPersona)
        {
            _esValido = true;
            _IdUsuario = (int)pPersona[ID_USUARIO_HEADER];
            _RangoUsuario = (string)pPersona["Nombre_rango"];
            System.Diagnostics.Debug.WriteLine("Id usuario: " + _IdUsuario);
            System.Diagnostics.Debug.WriteLine("Rango usuario: " + _RangoUsuario);
            return (object)(new Usuario(_IdUsuario, _RangoUsuario));
        }

        protected override List<SqlParameter> obtenerParametrosObtencion()
        {
            List<SqlParameter> _Parametros = new List<SqlParameter>();

            SqlParameter pNickName = new SqlParameter();
            pNickName.ParameterName = NICKNAME_PARAM;
            pNickName.SqlDbType = SqlDbType.VarChar;
            pNickName.Value = _Nickname;

            SqlParameter pContrasena = new SqlParameter();
            pContrasena.ParameterName = CONTRASENA_PARAM;
            pContrasena.SqlDbType = SqlDbType.VarChar;
            pContrasena.Value = _Contrasena;

            _Parametros.Add(pNickName);
            _Parametros.Add(pContrasena);
            return _Parametros;
        }

        protected override string obtenerProcedimientoDeObtencion()
        {
            return PROCEDIMIENTO;
        }

        protected override string obtenerProcedimientoDeInsercion()
        {
            return PROCEDIMIENTO_INSERCION;
        }

        protected override List<SqlParameter> obtenerParametrosInsercion()
        {
            List<SqlParameter> _Parametros = new List<SqlParameter>();

            SqlParameter pNickName = new SqlParameter();
            pNickName.ParameterName = NICKNAME_INSERT_PARAM;
            pNickName.SqlDbType = SqlDbType.VarChar;
            pNickName.Value = _Nickname;

            SqlParameter pContrasena = new SqlParameter();
            pContrasena.ParameterName = CONTRASENA_PARAM;
            pContrasena.SqlDbType = SqlDbType.VarChar;
            pContrasena.Value = _Contrasena;

            SqlParameter pCorreoElectronico = new SqlParameter();
            pCorreoElectronico.ParameterName = CORREO_ELECTRONICO_PARAM;
            pCorreoElectronico.SqlDbType = SqlDbType.VarChar;
            pCorreoElectronico.Value = _CorreoElectronico;


            SqlParameter pIdUsuario = new SqlParameter();
            pIdUsuario.ParameterName = ID_USUARIO_INSERT_PARAM;
            pIdUsuario.SqlDbType = SqlDbType.Int;
            pIdUsuario.Direction = ParameterDirection.Output;


            _Parametros.Add(pNickName);
            _Parametros.Add(pContrasena);
            _Parametros.Add(pCorreoElectronico);
            _Parametros.Add(pIdUsuario);
            return _Parametros;
        }
    }
}