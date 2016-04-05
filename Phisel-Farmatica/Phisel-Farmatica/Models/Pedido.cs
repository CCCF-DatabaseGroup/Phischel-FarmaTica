using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Phisel_Farmatica.Models
{
    public class Pedido:DatabaseConnectorObject
    {

        public int _IdPedido { get; set; }
        public string _NombreCliente { get; set; }
        public string _ApellidoCliente { get; set; }
        public decimal _AlCobro { get; set; }
        public DateTime _HoraYFecha { get; set; }

        protected override object contextualizar(DataRow pTablaDatos)
        {
            return new
            {
                IdPedido = _IdPedido,
                NombreCliente = _NombreCliente,
                ApellidoCliente = _ApellidoCliente,
                AlCobro = _AlCobro,
                HoraYFecha = _HoraYFecha
            };
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