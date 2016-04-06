using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Phisel_Farmatica.Models
{
    public class Empleado:DatabaseConnectorObject
    {

        public int _IdEmpleado { get; set; }
        public int _CedulaEmpleado { get; set; }
        public string _NombreEmpleado { get; set; }
        public string _PrimerApellidoEmpleado { get; set; }
        public string _SegundoApellidoEmpleado { get; set; }
        public int _TelefonoEmpleado { get; set; }
        public int _SalarioEmpleado { get; set; }

        protected override object contextualizar(DataRow pTablaDatos)
        {
            return new
            {
                _IdEmpleado = _IdEmpleado,
                _CedulaEmpleado = _CedulaEmpleado,
                _NombreEmpleado = _NombreEmpleado,
                _PrimerApellidoEmpleado = _PrimerApellidoEmpleado,
                _SegundoApellidoEmpleado = _SegundoApellidoEmpleado,
                _TelefonoEmpleado = _TelefonoEmpleado,
                _SalarioEmpleado = _SalarioEmpleado
            };
        }

        static public bool cambiarSalarario(int pIdAdministrador, int pIdUsuario,decimal Salario)
        {
            //agregar parametros aqui
            return true;
        }

        public bool despedir()
        {
            //agregar parametros aqui
            return true;
        }

        public bool aceptarRenuncia()
        {
            //agregar parametros aqui
            return true;
        }

        protected override List<SqlParameter> obtenerParametrosObtencion()
        {
            throw new NotImplementedException();
        }

        protected override List<SqlParameter> obtenerParametrosInsercion()
        {
            throw new NotImplementedException();
        }

        protected override string obtenerProcedimientoDeObtencion()
        {
            throw new NotImplementedException();
        }

        protected override string obtenerProcedimientoDeInsercion()
        {
            throw new NotImplementedException();
        }
    }
}