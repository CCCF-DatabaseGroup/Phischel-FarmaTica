using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phisel_Farmatica.Models
{
    class ProductoPedido : DatabaseConnectorObject
    {



        public const string PROCEDIMIENTO = "verComprasDeUnPedido";
        public const string ID_PRODUCTO = "Id_producto";
        public const string NOMBRE_PRODUCTO = "Nombre_producto";
        public const string PESCRIPCION = "Prescripcion";
        public const string PRECIO_PRODUCTO = "Precio_pedido";
        public const string CANTIDAD_PRODUCTO = "Cantidad_pedido";
        
        
        public const string PARAM_ID_PEDIDO = "@idPedido";


        public string _ProcedimientoActivo = PROCEDIMIENTO;

        public int _IdProducto { get; set; }
        public int _IdPedido { get; set; }
        public string _Nombre { get; set; }
        public bool _Prescripcion { get; set; }
        public int _Cantidad { get; set; }
        public decimal _Precio { get; set; }


        public ProductoPedido(int pIdPedido) {
            _IdPedido = pIdPedido;
        }


        protected override object contextualizar(DataRow pTablaDatos)
        {
            _IdProducto = (int)pTablaDatos[ID_PRODUCTO];
            object tmp = pTablaDatos[NOMBRE_PRODUCTO];
            _Nombre = (tmp != null)?(string)tmp:"";
            _Prescripcion = (bool)pTablaDatos[PESCRIPCION];
            _Cantidad = (int)pTablaDatos[CANTIDAD_PRODUCTO];
            _Precio = (decimal)pTablaDatos[PRECIO_PRODUCTO];

            return new
            {
                IdProducto = _IdProducto,
                Nombre = _Nombre,
                Prescripcion = _Prescripcion ? "Si" : "No",
                Cantidad = _Cantidad,
                Precio = _Precio
            };
        }

        protected override List<SqlParameter> obtenerParametrosInsercion()
        {
            throw new NotImplementedException();
        }

        protected override List<SqlParameter> obtenerParametrosObtencion()
        {
            _Parametros = new List<SqlParameter>();
            _Parametros.Add(new SqlParameter(PARAM_ID_PEDIDO, _IdPedido));
            return _Parametros;
            
        }

        protected override string obtenerProcedimientoDeInsercion()
        {
            throw new NotImplementedException();
        }

        protected override string obtenerProcedimientoDeObtencion()
        {
            return _ProcedimientoActivo;
        }
    }
}
