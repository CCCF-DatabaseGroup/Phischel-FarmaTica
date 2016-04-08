using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Phisel_Farmatica.Models
{
    public class ProductoSucursal:DatabaseConnectorObject
    {
        public int _IdSucursal { set; get; }
        public decimal _Costo { set; get; }
        public int _CantidadEnBodega { set; get; }

        public const string PROCEDIMIENTO_INSERCION = "";
        public const string PROCEDIMIENTO_EDICION = "";
        public const string PROCEDIMIENTO_OBTENCION = "";

        public const string HEADER_ID_SUCURSAL = "";
        public const string HEADER_COSTO = "";
        public const string HEADER_CANTIDAD_EN_BODEGA = "";

        protected override object contextualizar(DataRow pTablaDatos)
        {
            _IdSucursal = (int)pTablaDatos[HEADER_ID_SUCURSAL];
            _Costo = (decimal)pTablaDatos[HEADER_COSTO];
            _CantidadEnBodega = (int)pTablaDatos[HEADER_CANTIDAD_EN_BODEGA]; ;
            return new
            {
                IdSucursal  =  _IdSucursal,
                Costo = _Costo,
                CantidadEnBodega = _CantidadEnBodega
            };
        }


        public override bool insertar()
        {
            _Parametros = new List<SqlParameter>();
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            bool aRetornar = conexionGenerica(PROCEDIMIENTO_INSERCION, _Parametros);

            return aRetornar;

        }

        public override bool editar()
        {
            _Parametros = new List<SqlParameter>();
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            bool aRetornar = conexionGenerica(PROCEDIMIENTO_EDICION, _Parametros);

            return aRetornar;

        }


        protected override List<SqlParameter> obtenerParametrosObtencion()
        {
            _Parametros = new List<SqlParameter>();
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            return _Parametros;
        }

        protected override List<SqlParameter> obtenerParametrosInsercion()
        {
            _Parametros = new List<SqlParameter>();
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            _Parametros.Add(new SqlParameter());
            return _Parametros;
        }

        protected override string obtenerProcedimientoDeObtencion()
        {
            return PROCEDIMIENTO_OBTENCION;
        }

        protected override string obtenerProcedimientoDeInsercion()
        {
            return PROCEDIMIENTO_INSERCION;
        }
    }
}