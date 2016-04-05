using System;
using System.Data;

namespace Phisel_Farmatica.Models
{
    public class CN_Sucursales
    {
               public static DataTable obtenerSucursalesPorFarmacia(int farmaciaActual)
        {
            return new CD_Sucursales().mostrarSucursalesPorFarmacia(farmaciaActual);
        }

        public static DataRow obtenerDireccionSucursal(int idLocalidad)
        {
            return new CD_Localidad().obtenerDireccion(idLocalidad);
        }

        public static string header_idSucursal()
        {
            return new CD_Sucursales().header_idSucursal;
        }

        public static string header_nombreSucursal()
        {
            return new CD_Sucursales().header_nombreSucursal;
        }

        public static string header_idLocalidadSucursal()
        {
            return new CD_Sucursales().header_idLocalidadSucursal;
        }

        
    }
}
