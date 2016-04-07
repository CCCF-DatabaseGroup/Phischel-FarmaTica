using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Phisel_Farmatica.Models
{
    public class CategoriaProducto : DatabaseConnectorObject
    {

        public string Nombre { get; set; }

        public const string NOMBRE_CATEGORIA_HEADER = "Nombre_categoria";
        public const string HEADER_ID_CATEGORIA = "Id_categoria";

        public const string PROCEDIMIENTO = "obtenerCategoriaProducto";

        protected override object contextualizar(DataRow pTablaDatos)
        {

            Nombre = (string)pTablaDatos[NOMBRE_CATEGORIA_HEADER];
            return new { Nombre = Nombre, IdCategoria = (int)pTablaDatos[HEADER_ID_CATEGORIA] };
        }

        protected override List<SqlParameter> obtenerParametrosObtencion()
        {
            return new List<SqlParameter>();
        }

        protected override string obtenerProcedimientoDeObtencion()
        {
            return PROCEDIMIENTO;
        }

        protected override string obtenerProcedimientoDeInsercion()
        {
            throw new NotImplementedException();
        }

        protected override List<SqlParameter> obtenerParametrosInsercion()
        {
            throw new NotImplementedException();
        }
    }
}