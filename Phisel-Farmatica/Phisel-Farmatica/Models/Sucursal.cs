using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Phisel_Farmatica.Models
{
    public class Sucursal : DatabaseConnectorObject
    {

        public static string Farmacia { set; get; }
        public static string Provincia { set; get; }
        public static string NombreSucursal { set; get; }
        public static string Distrito { set; get; }
        public static string Canton { set; get; }
        public static int Id_sucursal { set; get; }

        public const string FARMACIA_PARAM = "@farmacia";
        public const string PROVINCIA_PARAM = "@provincia";
        public const string PROCEDIMIENTO = "obtenerSucursalesEnProvinciaPorFarmacia";



        public const string ID_SUCURSAL = "Id_sucursal";
        public const string NOMBRE_SUCURSAL_HEADER = "Nombre_sucursal";
        public const string DISTRITO_HEADER = "Nombre_distrito";
        public const string CANTON_HEADER = "Nombre_canton";


        public Sucursal(string pProvincia,string pFarmacia)
        {
            Farmacia = pFarmacia;
            Provincia = pProvincia;
        }

        private Sucursal(string pNombreSucursal, string pDistrito,string pCanton)
        {
            NombreSucursal = pNombreSucursal;
            Distrito = pDistrito;
            Canton = pCanton;
        }


        protected override object contextualizar(DataRow pTablaDatos)
        {
            NombreSucursal = (string)pTablaDatos[NOMBRE_SUCURSAL_HEADER];
            Distrito = (string)pTablaDatos[DISTRITO_HEADER];
            Canton = (string)pTablaDatos[CANTON_HEADER];
            Id_sucursal = (int)pTablaDatos[ID_SUCURSAL];

            return new {
                Nombre = NombreSucursal,
                Distrito = Distrito,
                Canton = Canton,
                IdSucursal = Id_sucursal
            };
        }

        protected override List<SqlParameter> obtenerParametrosObtencion()
        {
            _Parametros = new List<SqlParameter>();

            SqlParameter pProvincia = new SqlParameter();
            pProvincia.ParameterName = PROVINCIA_PARAM;
            pProvincia.SqlDbType = SqlDbType.VarChar;
            pProvincia.Value = Provincia;

            SqlParameter pFarmacia = new SqlParameter();
            pFarmacia.ParameterName = FARMACIA_PARAM;
            pFarmacia.SqlDbType = SqlDbType.VarChar;
            pFarmacia.Value = Farmacia;

            _Parametros.Add(pProvincia);
            _Parametros.Add(pFarmacia);
            return _Parametros;
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