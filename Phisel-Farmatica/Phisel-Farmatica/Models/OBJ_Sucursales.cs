using System;
using System.Data;

namespace Phisel_Farmatica.Models
{
    public class OBJ_Sucursales
    {
        private DataTable tablaSucursales;
        private DataRow sucursalActual;
        private DataRow direccionSucursal;
        public int idSucursal;
        public int idLocalidad;
        public string provinciaSucursalActual;
        public string cantonSucursalActual;
        public string distritoSucursalActual;

        public OBJ_Sucursales()
        {

        }

        public DataTable setSucursalesPorFarmacia(int idFarmacia)
        {
            tablaSucursales = CN_Sucursales.obtenerSucursalesPorFarmacia(idFarmacia);
            return tablaSucursales;
        }

        public void setSucursalActual(int indiceSucursal)
        {
            sucursalActual = tablaSucursales.Rows[indiceSucursal];
            idSucursal = Convert.ToInt32(sucursalActual[CN_Sucursales.header_idSucursal()]);
            idLocalidad = Convert.ToInt32(sucursalActual[CN_Sucursales.header_idLocalidadSucursal()]);
            direccionSucursal = CN_Localidad.obtenerDireccion(idLocalidad);
            provinciaSucursalActual = direccionSucursal[CN_Localidad.header_nombreProvincia()].ToString();
            cantonSucursalActual = direccionSucursal[CN_Localidad.header_nombreCanton()].ToString();
            distritoSucursalActual = direccionSucursal[CN_Localidad.header_nombreDistrito()].ToString();
        }

        public string header_idSucursal()
        {
            return CN_Sucursales.header_idSucursal();
        }

        public string header_nombreSucursal()
        {
            return CN_Sucursales.header_nombreSucursal();
        }
    }
}
