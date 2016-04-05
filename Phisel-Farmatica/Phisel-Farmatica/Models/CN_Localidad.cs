using System.Data;

namespace Phisel_Farmatica.Models
{
    public class CN_Localidad
    {
        private static DataRow localidadActual;
        public static int idLocalidad;
        public static string provincia;
        public static string canton;
        public static string distrito;
        
        // obtener

        public static void setDireccion(int idLocalidad)
        {
            localidadActual = new CD_Localidad().obtenerDireccion(idLocalidad);
            provincia = localidadActual[header_nombreProvincia()].ToString();
            canton = localidadActual[header_nombreCanton()].ToString();
            distrito = localidadActual[header_nombreDistrito()].ToString();
            //return localidadActual;
        }

        public static DataRow obtenerDireccion(int idLocalidad)
        {
            return new CD_Localidad().obtenerDireccion(idLocalidad);
        }

        public static int obtenerIdLocalidad(int pProvincia, int pCanton, int pDistrito)
        {
            return new CD_Localidad().obtenerIdLocalidad(pProvincia, pCanton, pDistrito);
        }

        // mostrar

        public static DataTable mostrarProvincias()
        {
            return new CD_Localidad().mostrarProvincias();
        }

        public static DataTable mostrarCantones(int pProvincia)
        {
            return new CD_Localidad().mostrarCantones(pProvincia);
        }

        public static DataTable mostrarDistritos(int pProvincia, int pCanton)
        {
            return new CD_Localidad().mostrarDistritos(pProvincia, pCanton);
        }

        // headers

        public static string header_idProvincia()
        {
            return new CD_Localidad().header_idProvincia;
        }

        public static string header_nombreProvincia()
        {
            return new CD_Localidad().header_nombreProvincia;
        }

        public static string header_idCanton()
        {
            return new CD_Localidad().header_idCanton;
        }

        public static string header_nombreCanton()
        {
            return new CD_Localidad().header_nombreCanton;
        }

        public static string header_idDistrito()
        {
            return new CD_Localidad().header_idDistrito;
        }

        public static string header_nombreDistrito()
        {
            return new CD_Localidad().header_nombreDistrito;
        }

    }
}

