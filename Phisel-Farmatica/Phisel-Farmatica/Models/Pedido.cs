﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Phisel_Farmatica.Models
{
    public class Pedido:DatabaseConnectorObject
    {

        // ,
        //, 
        //,
        //,
        //,
        //,
        //S.
        //,SUM(Precio_pedido* Cantidad_pedido) AS Deuda

        public int _IdPedido { get; set; }
        public DateTime _Fecha_Hora_Ordenado { get; set; }
        public DateTime _Fecha_Hora_Requerido { get; set; }
        public int _Estado_Pedido { get; set; }
        public int _IdEmpleado { get; set; }
        public string _Nickname { get; set; }
        public string _NombreCliente { get; set; }
        public string _ApellidoCliente { get; set; }
        public decimal _Deuda { get; set; }


        public const string PARAM_ID_DEPENDIENTE = "@idDependiente";
        public const string PROCEDIMIENTO_SOLICITAR_PEDIDOS = "solicitarLosPedidosDeUnaSucursalMedianteIdDependiente";


        public const string HEADER_ID_PEDIDO = "Id_pedido";
        public const string HEADER_FECHA_REQUERIDA = "Fecha_requerido";
        public const string HEADER_HORA_REQUERIDA = "Hora_requerido";
        public const string HEADER_HORA_ORDENADA = "Hora_ordenado";
        public const string HEADER_FECHA_ORDENADA = "Fecha_ordenado";
        public const string HEADER_ESTADO_PEDIDO = "Estado_pedido";
        public const string HEADER_ID_DEPENDIENTE = "Id_pedido_dependiente";
        public const string HEADER_NICKNAME = "Nickname";
        public const string HEADER_NOMBRE_CLIENTE = "Nombre_persona";
        public const string HEADER_APELLIDO_CLIENTE = "Apellido1";
        public const string HEADER_DEUDA = "Deuda";

        public string _ProcedimientoActivo = PROCEDIMIENTO_SOLICITAR_PEDIDOS;


        public Pedido(int pIdEmpleado )
        {
            _IdEmpleado = pIdEmpleado;
        }

        protected override object contextualizar(DataRow pTablaDatos)
        {
            object tmp;
            _IdPedido = (int)pTablaDatos[HEADER_ID_PEDIDO];
            tmp = pTablaDatos[HEADER_FECHA_ORDENADA];
            _Fecha_Hora_Ordenado = (tmp != null)?(DateTime)tmp :new DateTime();
            TimeSpan tmp2 = (TimeSpan)pTablaDatos[HEADER_HORA_REQUERIDA];
            _Fecha_Hora_Ordenado.AddHours(tmp2.Hours);
            _Fecha_Hora_Ordenado.AddMinutes(tmp2.Minutes);
            tmp = pTablaDatos[HEADER_FECHA_REQUERIDA];
            _Fecha_Hora_Requerido = (tmp != null) ? (DateTime)tmp : new DateTime();
            tmp2 = (TimeSpan)pTablaDatos[HEADER_HORA_REQUERIDA];
            _Fecha_Hora_Requerido = _Fecha_Hora_Requerido.AddHours(tmp2.Hours);
            _Fecha_Hora_Requerido = _Fecha_Hora_Requerido.AddMinutes(tmp2.Minutes);
            _Estado_Pedido = (int)pTablaDatos[HEADER_ESTADO_PEDIDO];
            tmp = pTablaDatos[HEADER_NICKNAME];
            _Nickname = (tmp != null) ? (string)tmp :"";
            /*tmp = pTablaDatos[HEADER_NOMBRE_CLIENTE];
            _NombreCliente = (tmp != null) ? (string)tmp : ""; ;
            tmp = pTablaDatos[HEADER_APELLIDO_CLIENTE];
            _ApellidoCliente = (tmp != null) ? (string)tmp : ""; ;
            */        
            _Deuda = (decimal)pTablaDatos[HEADER_DEUDA];

            return new
            {
                IdPedido = _IdPedido,
                NombreCliente = _NombreCliente,
                ApellidoCliente = _ApellidoCliente,
                Nickname = _Nickname,
                AlCobro = _Deuda,
                HoraYFecha = _Fecha_Hora_Requerido.ToString()
            };
        }

        protected override List<SqlParameter> obtenerParametrosObtencion()
        {

            List<SqlParameter> _Parametros = new List<SqlParameter>();

            SqlParameter pIdEmpleado = new SqlParameter();
            pIdEmpleado.ParameterName = PARAM_ID_DEPENDIENTE;
            pIdEmpleado.SqlDbType = SqlDbType.Int;
            pIdEmpleado.Value = _IdEmpleado;

            _Parametros.Add(pIdEmpleado);
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