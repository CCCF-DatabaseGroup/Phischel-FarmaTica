using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phisel_Farmatica.Models
{
    public abstract class DatabaseConnectorObject
    {
        public const string CONNECTION_STRING = "workstation id=PhishelFarmatica.mssql.somee.com;packet size=4096;user id=crisrivlop_SQLLogin_1;pwd=4pwidhq39j;data source=PhishelFarmatica.mssql.somee.com;persist security info=False;initial catalog=PhishelFarmatica";


        private SqlConnection SqlConexion;
        private DataTable TablaDatos;

        protected List<SqlParameter> _Parametros;


        private void abrirConeccion(string pProcedimiento,List<SqlParameter> pParametros)
        {


            SqlConexion = new SqlConnection();
            TablaDatos = new DataTable();


            try
            {
                SqlConexion.ConnectionString = CONNECTION_STRING;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = pProcedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter parametro in pParametros)
                {
                    SqlComando.Parameters.Add(parametro);
                }


                SqlComando.ExecuteNonQuery();

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
                SqlComando.Parameters.Clear();
            }

            catch (Exception ex)
            {
                throw new Exception(pProcedimiento + "\n" + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }
        }

        public IEnumerable<object> obtenerIterativamente()
        {
            abrirConeccion(obtenerProcedimientoDeObtencion(), obtenerParametrosObtencion());

            if (TablaDatos.Rows.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("Tabla datos > 0");
                foreach (DataRow row in TablaDatos.Rows)
                {
                    System.Diagnostics.Debug.WriteLine("... explorando opciones");
                    yield return contextualizar(row);
                }
                
            }

        }

        public bool obtener()
        {
            abrirConeccion(obtenerProcedimientoDeObtencion(), obtenerParametrosObtencion());

            if (TablaDatos.Rows.Count > 0)
            {
                contextualizar(TablaDatos.Rows[0]);
                return true;

            }
            return false;

        }
        public object obtenerJson()
        {
            abrirConeccion(obtenerProcedimientoDeObtencion(), obtenerParametrosObtencion());
            if (TablaDatos.Rows.Count > 0)
            {
                return contextualizar(TablaDatos.Rows[0]);
            }
            return null;
        }


        protected bool conexionGenerica(string pProcedimiento, List<SqlParameter> pParametros)
        {
            bool aRetornar = false;
            SqlConexion = new SqlConnection();


            try
            {
                SqlConexion.ConnectionString = CONNECTION_STRING;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = pProcedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                List<SqlParameter> _Parametros = pParametros;

                foreach (SqlParameter parametro in _Parametros)
                {
                    SqlComando.Parameters.Add(parametro);
                }


                SqlComando.ExecuteNonQuery();
                aRetornar = true;
            }

            catch (Exception ex)
            {
                throw new Exception(obtenerProcedimientoDeInsercion() + "\n" + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return aRetornar;

        }



        public bool insertar()
        {
            bool aRetornar = conexionGenerica(obtenerProcedimientoDeInsercion(), obtenerParametrosInsercion());
            
            return aRetornar;

        }

        /*
        public bool eliminar()
        {
            bool aRetornar = conexionGenerica(obtenerProcedimientoDeInsercion(), obtenerParametrosInsercion());
            return aRetornar;
        }

         */




        protected abstract object contextualizar(DataRow pTablaDatos);
        protected abstract List<SqlParameter> obtenerParametrosObtencion();
        protected abstract List<SqlParameter> obtenerParametrosInsercion();
        protected abstract string obtenerProcedimientoDeObtencion();
        protected abstract string obtenerProcedimientoDeInsercion();


    }
}