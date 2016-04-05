using System;
using System.Data;

namespace Phisel_Farmatica.Models
{
    public class CN_Productos
    {
        public static DataTable mostrarProductosPorSucursal(int sucursal, int numeroPagina, int registrosPorPagina)
        {
            return new CD_Productos().mostrarProductosPorSucursal(sucursal, numeroPagina, registrosPorPagina);
        }

        public static int tamanoProductosPorSucursal(int parRegistrosPorPagina)
        {
            return new CD_Productos().tamanoProductosPorSucursal(parRegistrosPorPagina);
        }

        public static DataTable mostrarProductoBuscadoPorSucursal(string productoBuscado, int sucursal, int numeroPagina, int registrosPorPagina)
        {
            return new CD_Productos().mostrarProductoBuscadoPorSucursal(productoBuscado, sucursal, numeroPagina, registrosPorPagina);
        }

        public static int tamanoProductoBuscadoPorSucursal(string productoBuscado, int sucursal, int registrosPorPagina)
        {
            return new CD_Productos().tamanoProductoBuscadoPorSucursal(productoBuscado, sucursal, registrosPorPagina);
        }

        // no implementado

        public static DataTable ListaProductos()
        {
            return new CD_Productos().ListaProductos();
        }

        public static decimal PrecioProducto(int parIdProducto)
        {
            return new CD_Productos().PrecioProducto(parIdProducto);
        }

        public static DataTable Mostrar(int parNumeroPagina, int parRegistrosPorPagina)
        {
            return new CD_Productos().Mostrar(parNumeroPagina, parRegistrosPorPagina);
        }

        public static String Eliminar(int parId_Producto)
        {
            CD_Productos productos = new CD_Productos();
            productos.Id_Producto = parId_Producto;

            return productos.Eliminar(productos);
        }

        public static String Insertar(String parNombre_Producto, String parNombre_Categoria,
            decimal parPrecio_Unitario, String parDetalles)
        {
            CD_Productos productos = new CD_Productos();
            productos.Nombre_Producto = parNombre_Producto;
            productos.Nombre_Categoria = parNombre_Categoria;
            productos.Precio_Unitario = parPrecio_Unitario;
            productos.Detalles = parDetalles;

            return productos.Insertar(productos);
        }

        public static String Editar(int parId_Producto, String parNombre_Producto,
            String parNombre_Categoria, decimal parPrecio_Unitario, String parDetalles)
        {
            CD_Productos productos = new CD_Productos();
            productos.Id_Producto = parId_Producto;
            productos.Nombre_Producto = parNombre_Producto;
            productos.Nombre_Categoria = parNombre_Categoria;
            productos.Precio_Unitario = parPrecio_Unitario;
            productos.Detalles = parDetalles;

            return productos.Editar(productos);
        }

    }
}
