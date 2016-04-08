using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Phisel_Farmatica.Models
{
    public class Producto : DatabaseConnectorObject
    {

        public const string PROCEDIMIENTO = "obtenerProductoDeSucursal";
        public const string PROCEDIMIENTO_OBTENER_TODOS = "mostrarTodosLosProductos";
        public const string PROCEDIMIENTO_OBTENCION_POR_ID = "obtenerProductoDeSucursalPorId";
        public const string PROCEDIMIENTO_INSERCION = "insertarProducto";
        public const string PROCEDIMIENTO_EDICION = "actualizarProducto";
        public const string NOMBRE_PRODUCTO = "Nombre_producto";
        public const string DESCRIPCION_PRODUCTO = "Descripcion";
        public const string PRESCRIPCION_PRODUCTO = "Prescripcion";
        public const string CANTIDAD_BODEGA = "Cantidad_bodega";
        public const string PRECIO_PRODUCTO = "Precio_sucursal";
        public const string ID_PRODUCTO = "Id_producto";
        public const string FARMACIA_PARAM = "@Farmacia";
        public const string SUCURSAL_PARAM = "@Sucursal";
        public const string CATEGORIA_PARAM = "@Categoria";
        public const string ID_PRODUCTO_PARAM = "@IdProducto";
        public const string PARAM_ID_LABORATORIO_PRODUCTO = "@idLaboratorio";
        public const string PARAM_NOMBRE_PRODUCTO = "@nombre";
        public const string PARAM_PRESCRIPCION_PRODUCTO = "@prescripcion";
        public const string PARAM_ID_CATEGORIA_PRODUCTO = "@idCategoria";
        public const string PARAM_DESCRIPCION_PRODUCTO = "@descripcion";
        public const string PARAM_EXITO = "@exito";


        public string _ProcedimientoActivo = PROCEDIMIENTO;

        public int _IdProducto { get; set; }
        public int _IdCategoria { get; set; }
        public int _IdLaboratorio { get; set; }        
        public string _Nombre { get; set; }
        public string _NombreSucursal { get; set; }
        public string _NombreFarmacia { get; set; }
        public string _NombreCategoria { get; set; }
        public string _Descripcion { get; set; }
        public bool _Prescripcion { get; set; }
        public int _Cantidad { get; set; }
        public decimal? _Precio { get; set; }


        public Producto()
        {

        }

        public Producto(string pFarmacia, string pSucursal, string pCategoria)
        {
            this._NombreFarmacia = pFarmacia;
            this._NombreSucursal = pSucursal;
            this._NombreCategoria = pCategoria;
            obtenerParametrosObtencionSinfiltro();
            _ProcedimientoActivo = PROCEDIMIENTO;
        }


        public Producto(int pIdProducto,string pFarmacia, string pSucursal)
        {
            this._NombreFarmacia = pFarmacia;
            this._NombreSucursal = pSucursal;
            this._IdProducto = pIdProducto;
            obtenerParametrosObtencionConfiltro();
            _ProcedimientoActivo = PROCEDIMIENTO_OBTENCION_POR_ID;

        }

        public Producto(string pNombreProducto, int pIdCategoria, int pIdLaboratorio, bool pPreescripcion, string pDescripcion)
        {
            this._Nombre = pNombreProducto;
            this._IdCategoria = pIdCategoria;
            this._IdLaboratorio = pIdLaboratorio;
            this._Prescripcion = pPreescripcion;
            this._Descripcion = pDescripcion;
        }


        public Producto(int pIdProducto, string pNombreProducto, int pIdCategoria, int pIdLaboratorio, bool pPreescripcion, string pDescripcion)
        {
            this._Nombre = pNombreProducto;
            this._IdProducto = pIdProducto;
            this._IdCategoria = pIdCategoria;
            this._IdLaboratorio = pIdLaboratorio;
            this._Prescripcion = pPreescripcion;
            this._Descripcion = pDescripcion;
        }




        protected override object contextualizar(DataRow pTablaDatos)
        {
            _IdProducto = (int)pTablaDatos[ID_PRODUCTO];
            _Nombre = (string)pTablaDatos[NOMBRE_PRODUCTO];
            _Descripcion = (string)pTablaDatos[DESCRIPCION_PRODUCTO];
            _Prescripcion = (bool)pTablaDatos[PRESCRIPCION_PRODUCTO];
            _Cantidad = (int)pTablaDatos[CANTIDAD_BODEGA];
            _Precio = (decimal)pTablaDatos[PRECIO_PRODUCTO];

            return new
            {
                IdProducto = _IdProducto,
                Nombre = _Nombre,
                Descripcion = _Descripcion,
                Prescripcion = _Prescripcion ? "Si" : "No",
                Cantidad = _Cantidad,
                Precio = _Precio,
                Sucursal = _NombreSucursal
            };
        }




        protected void obtenerParametrosObtencionSinfiltro()
        {
            _Parametros = new List<SqlParameter>();

            SqlParameter pSucursal = new SqlParameter();
            pSucursal.ParameterName = SUCURSAL_PARAM;
            pSucursal.SqlDbType = SqlDbType.VarChar;
            pSucursal.Value = _NombreSucursal;

            SqlParameter pFarmacia = new SqlParameter();
            pFarmacia.ParameterName = FARMACIA_PARAM;
            pFarmacia.SqlDbType = SqlDbType.VarChar;
            pFarmacia.Value = _NombreFarmacia;


            SqlParameter pNombreCategoria = new SqlParameter();
            pNombreCategoria.ParameterName = CATEGORIA_PARAM;
            pNombreCategoria.SqlDbType = SqlDbType.VarChar;
            pNombreCategoria.Value = _NombreCategoria;

            _Parametros.Add(pSucursal);
            _Parametros.Add(pFarmacia);
            _Parametros.Add(pNombreCategoria);
        }

        public IEnumerable<object> obtenerTodos()
        {
            _Parametros = new List<SqlParameter>();
            _ProcedimientoActivo = PROCEDIMIENTO_OBTENER_TODOS;
            List<object> toReturn = new  List<object>();
            abrirConeccion(_ProcedimientoActivo, _Parametros);
            foreach (DataRow row in TablaDatos.Rows)
            {
                toReturn.Add(new
                {
                    IdProducto = (int)row[ID_PRODUCTO],
                    Nombre = (string)row[NOMBRE_PRODUCTO]
                });
            }
            return toReturn;
        }



        public override bool insertar()
        {
            _Parametros = new List<SqlParameter>();
            _Parametros.Add(new SqlParameter(PARAM_ID_LABORATORIO_PRODUCTO,_IdLaboratorio));
            _Parametros.Add(new SqlParameter(PARAM_NOMBRE_PRODUCTO,_Nombre));
            _Parametros.Add(new SqlParameter(PARAM_PRESCRIPCION_PRODUCTO,_Prescripcion));
            _Parametros.Add(new SqlParameter(PARAM_ID_CATEGORIA_PRODUCTO,_IdCategoria));
            _Parametros.Add(new SqlParameter(PARAM_DESCRIPCION_PRODUCTO,_Descripcion));
            SqlParameter pExito = new SqlParameter();
            pExito.ParameterName = PARAM_EXITO;
            pExito.SqlDbType = SqlDbType.Bit;
            pExito.Direction = ParameterDirection.Output;
            _Parametros.Add(pExito);
            bool aRetornar = conexionGenerica(PROCEDIMIENTO_INSERCION, _Parametros);

            return aRetornar;

        }

        public override bool editar()
        {
            _Parametros = new List<SqlParameter>();
            _Parametros.Add(new SqlParameter(ID_PRODUCTO_PARAM, _IdProducto));
            _Parametros.Add(new SqlParameter(PARAM_NOMBRE_PRODUCTO, _Nombre));
            _Parametros.Add(new SqlParameter(PARAM_ID_LABORATORIO_PRODUCTO, _IdLaboratorio));
            _Parametros.Add(new SqlParameter(PARAM_PRESCRIPCION_PRODUCTO, _Prescripcion));
            _Parametros.Add(new SqlParameter(PARAM_ID_CATEGORIA_PRODUCTO, _IdCategoria));
            _Parametros.Add(new SqlParameter(PARAM_DESCRIPCION_PRODUCTO, _Descripcion));
            SqlParameter pExito = new SqlParameter();
            pExito.ParameterName = PARAM_EXITO;
            pExito.SqlDbType = SqlDbType.Bit;
            pExito.Direction = ParameterDirection.Output;
            _Parametros.Add(pExito);
            bool aRetornar = conexionGenerica(PROCEDIMIENTO_EDICION, _Parametros);

            return aRetornar;

        }



        protected void obtenerParametrosObtencionConfiltro()
        {

            _Parametros = new List<SqlParameter>();

            SqlParameter pSucursal = new SqlParameter();
            pSucursal.ParameterName = SUCURSAL_PARAM;
            pSucursal.SqlDbType = SqlDbType.VarChar;
            pSucursal.Value = _NombreSucursal;

            SqlParameter pFarmacia = new SqlParameter();
            pFarmacia.ParameterName = FARMACIA_PARAM;
            pFarmacia.SqlDbType = SqlDbType.VarChar;
            pFarmacia.Value = _NombreFarmacia;

            SqlParameter pIdProducto = new SqlParameter();
            pIdProducto.ParameterName = ID_PRODUCTO_PARAM;
            pIdProducto.SqlDbType = SqlDbType.Int;
            pIdProducto.Value = _IdProducto;

            _Parametros.Add(pSucursal);
            _Parametros.Add(pFarmacia);
            _Parametros.Add(pIdProducto);
        }

        protected override List<SqlParameter> obtenerParametrosObtencion()
        {
            return _Parametros;
        }

        protected override string obtenerProcedimientoDeObtencion()
        {
            return _ProcedimientoActivo;
        }

        protected override string obtenerProcedimientoDeInsercion()
        {
            return "";// throw new NotImplementedException();
        }


        protected override List<SqlParameter> obtenerParametrosInsercion()
        {
            throw new NotImplementedException();
        }

    }
}