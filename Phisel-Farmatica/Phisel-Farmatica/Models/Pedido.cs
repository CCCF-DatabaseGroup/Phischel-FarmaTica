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
        public int _IdEmpleado { get; set; }
        public string _NombreCliente { get; set; }
        public string _ApellidoCliente { get; set; }
        public decimal _AlCobro { get; set; }
        public DateTime _HoraYFecha { get; set; }


        public const string ID_EMPLEADO_PARAM = "@IdEmpleado";
        public const string PROCEDIMIENTO_SOLICITAR_PEDIDOS = "solicitarPedido";


        public string _ProcedimientoActivo = PROCEDIMIENTO_SOLICITAR_PEDIDOS;


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

            List<SqlParameter> _Parametros = new List<SqlParameter>();

            SqlParameter pIdEmpleado = new SqlParameter();
            pIdEmpleado.ParameterName = ID_EMPLEADO_PARAM;
            pIdEmpleado.SqlDbType = SqlDbType.Int;
            pIdEmpleado.Value = _IdEmpleado;

            return _Parametros;
        }

        protected override List<SqlParameter> obtenerParametrosInsercion()
        {
            throw new NotImplementedException();
        }

        protected override string obtenerProcedimientoDeObtencion()
        {
            return _ProcedimientoActivo;
        }

        protected override string obtenerProcedimientoDeInsercion()
        {
            throw new NotImplementedException();
        }
    }
}