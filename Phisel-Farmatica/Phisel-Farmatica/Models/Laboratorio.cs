using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phisel_Farmatica.Models
{
    class Laboratorio : DatabaseConnectorObject
    {
        public int _IdLaboratorio { get; set; }
        public string _NombreLaboratorio { get; set; }
        
        public const string PROCEDIMIENTO_SALIDA = "mostrarLaboratorios";

         public const string PROCEDIMIENTO_INSERCION = " ";


        public const string HEADER_ID_LABORATORIO = "Id_laboratorio";
        public const string HEADER_NOMBRE_LABORATORIO = "Nombre_laboratorio";


        public const string PARAMETRO_ID_LABORATORIO = "@IdLaboratorio";


        protected override object contextualizar(DataRow pTablaDatos)
        {
            _IdLaboratorio = (int)pTablaDatos[HEADER_ID_LABORATORIO];
            _NombreLaboratorio = (string)pTablaDatos[HEADER_NOMBRE_LABORATORIO];
            return new {
                IdLaboratorio = _IdLaboratorio,
                NombreLaboratorio = _NombreLaboratorio
        };
        }

        protected override List<SqlParameter> obtenerParametrosInsercion()
        {
            _Parametros = new List<SqlParameter>();
            _Parametros.Add(new SqlParameter(PARAMETRO_ID_LABORATORIO,_IdLaboratorio));
            return _Parametros;
        }

        protected override List<SqlParameter> obtenerParametrosObtencion()
        {
            _Parametros = new List<SqlParameter>();
            return _Parametros;
        }

        protected override string obtenerProcedimientoDeInsercion()
        {
            return PROCEDIMIENTO_INSERCION;
        }

        protected override string obtenerProcedimientoDeObtencion()
        {
            return PROCEDIMIENTO_SALIDA;
        }
    }
}
