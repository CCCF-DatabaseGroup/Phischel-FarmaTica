using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phisel_Farmatica.Models
{
    class Dependiente : DatabaseConnectorObject
    {

        //Nickname,Nombre_persona,Apellido1,Apellido2,Cedula,Telefono_persona,[Salario_dependiente]
        public string _Nickname { get; set; }
        public string _Nombre_persona { get; set; }
        public string _Apellido1 { get; set; }
        public string _Apellido2 { get; set; }
        public int _Cedula { get; set; }
        public int _Telefono_persona { get; set; }
        public int _Salario_dependiente { get; set; }
        public int _Id_dependiente { get; set; }
        public int _Id_sucursal { get; set; }


        public Dependiente(int pIdSucursal)
        {
            _Id_sucursal = pIdSucursal;
        }

        public const string HEADER_NICKNAME = "Nickname";
        public const string HEADER_NOMBRE_PERSONA = "Nombre_persona";
        public const string HEADER_APELLIDO_1 = "Apellido1";
        public const string HEADER_APELLIDO_2 = "Apellido2";
        public const string HEADER_CEDULA = "Cedula";
        public const string HEADER_TELEFONO_PERSONA = "Telefono_persona";
        public const string HEADER_SALARIO_DEPENDIENTE = "Salario_dependiente";
        public const string HEADER_ID_DEPENDIENTE = "Id_usuario";

        public const string PARAM_ID_SUCURSAL = "@IdSucursal";


        public const string PROCEDIMIENTO = "obtenerDependienteDeSucursal";


        protected override object contextualizar(DataRow pTablaDatos)
        {
            object tmp = pTablaDatos[HEADER_NICKNAME];
            _Nickname = (tmp != null) ? (string)tmp : "";
            tmp = pTablaDatos[HEADER_NOMBRE_PERSONA];
            _Nombre_persona = (tmp != null) ? (string)tmp : "";
            tmp = pTablaDatos[HEADER_APELLIDO_1];
            _Apellido1 = (tmp != null) ? (string)tmp : "";
            tmp = pTablaDatos[HEADER_APELLIDO_2];
            _Apellido2 = (tmp != null) ? (string)tmp : "";
            tmp = pTablaDatos[HEADER_CEDULA];
            _Cedula = (int)tmp;
            tmp = pTablaDatos[HEADER_TELEFONO_PERSONA];
            _Telefono_persona = (int)tmp;
            tmp = pTablaDatos[HEADER_SALARIO_DEPENDIENTE];
            _Salario_dependiente = (int)tmp;
            tmp = pTablaDatos[HEADER_ID_DEPENDIENTE];
            _Id_dependiente = (int)tmp;

            //pTablaDatos

            return new {
                Nickname = _Nickname,
                Nombre_persona = _Nombre_persona,
                Apellido1 = _Apellido1,
                Apellido2 = _Apellido2,
                Cedula = _Cedula,
                Telefono_persona = _Telefono_persona,
                Salario_dependiente = _Salario_dependiente,
                Id_dependiente = _Id_dependiente
            };
        }

        protected override List<SqlParameter> obtenerParametrosInsercion()
        {
            throw new NotImplementedException();
        }

        protected override List<SqlParameter> obtenerParametrosObtencion()
        {
            _Parametros = new List<SqlParameter>();
            SqlParameter pSucursal = new SqlParameter();
            pSucursal.ParameterName = PARAM_ID_SUCURSAL;
            pSucursal.SqlDbType = SqlDbType.Int;
            pSucursal.Value = _Id_sucursal;
            _Parametros.Add(pSucursal);
            return _Parametros;
        }

        protected override string obtenerProcedimientoDeInsercion()
        {
            throw new NotImplementedException();
        }

        protected override string obtenerProcedimientoDeObtencion()
        {
            return PROCEDIMIENTO;
        }
    }
}
