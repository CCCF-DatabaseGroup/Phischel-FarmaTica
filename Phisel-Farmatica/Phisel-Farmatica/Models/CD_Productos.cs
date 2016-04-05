using System;
using System.Data;
using System.Data.SqlClient;

namespace Phisel_Farmatica.Models
{
    public class CD_Productos
    {
        public int Id_Producto { get; set; }
        public string Nombre_Producto { get; set; }
        public string Nombre_Categoria { get; set; }
        public decimal Precio_Unitario { get; set; }
        public string Detalles { get; set; }
        public string Nombre_Buscado { get; set; }
        private string error = "Error al intentar ejecutar el procedimiento almacenado: ";

        public CD_Productos()
        {

        }

        // implementados

        public DataTable mostrarProductosPorSucursal(int pSucursal, int pNumeroPagina, int pRegistrosPorPagina)
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();
            string procedimiento = "mostrarProductosPorSucursal";

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = procedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter IdSucursal = new SqlParameter();
                IdSucursal.ParameterName = "@IdSucursal";
                IdSucursal.SqlDbType = SqlDbType.Int;
                IdSucursal.Value = pSucursal;
                SqlComando.Parameters.Add(IdSucursal);

                SqlParameter NumeroPagina = new SqlParameter();
                NumeroPagina.ParameterName = "@NumeroPagina";
                NumeroPagina.SqlDbType = SqlDbType.Int;
                NumeroPagina.Value = pNumeroPagina;
                SqlComando.Parameters.Add(NumeroPagina);

                SqlParameter RegistrosPorPagina = new SqlParameter();
                RegistrosPorPagina.ParameterName = "@RegistrosPorPagina";
                RegistrosPorPagina.SqlDbType = SqlDbType.Int;
                RegistrosPorPagina.Value = pRegistrosPorPagina;
                SqlComando.Parameters.Add(RegistrosPorPagina);

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }

            catch (SqlException ex)
            {
                TablaDatos = null;
                throw new Exception(error + procedimiento + "\n" + ex.Message, ex);
            }

            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            return TablaDatos;
        }

        public int tamanoProductosPorSucursal(int parRegistrosPorPagina)
        {
            SqlConnection SqlConexion = new SqlConnection();
            string procedimiento = "TamanoProductos";
            int totalPaginas = 1;

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = procedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParRegistrosPorPagina = new SqlParameter();
                ParRegistrosPorPagina.ParameterName = "@RegistrosPorPagina";
                ParRegistrosPorPagina.SqlDbType = SqlDbType.Int;
                ParRegistrosPorPagina.Value = parRegistrosPorPagina;
                SqlComando.Parameters.Add(ParRegistrosPorPagina);

                SqlParameter ParTotalPaginas = new SqlParameter();
                ParTotalPaginas.ParameterName = "@TotalPaginas";
                ParTotalPaginas.SqlDbType = SqlDbType.Int;
                ParTotalPaginas.Direction = ParameterDirection.Output;
                SqlComando.Parameters.Add(ParTotalPaginas);

                SqlComando.ExecuteNonQuery();

                totalPaginas = (int)SqlComando.Parameters["@TotalPaginas"].Value;
            }

            catch (Exception ex)
            {
                throw new Exception(error + procedimiento + "\n" + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return totalPaginas;
        }

        public DataTable mostrarProductoBuscadoPorSucursal(string pProductoBuscado, int pSucursal, int pNumeroPagina, int pRegistrosPorPagina)
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();
            string procedimiento = "mostrarProductoBuscadoPorSucursal";

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = procedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_Buscado = new SqlParameter();
                ParNombre_Buscado.ParameterName = "@ProductoBuscado";
                ParNombre_Buscado.SqlDbType = SqlDbType.VarChar;
                ParNombre_Buscado.Size = pProductoBuscado.Length;
                ParNombre_Buscado.Value = pProductoBuscado;
                SqlComando.Parameters.Add(ParNombre_Buscado);

                SqlParameter IdSucursal = new SqlParameter();
                IdSucursal.ParameterName = "@IdSucursal";
                IdSucursal.SqlDbType = SqlDbType.Int;
                IdSucursal.Value = pSucursal;
                SqlComando.Parameters.Add(IdSucursal);

                SqlParameter NumeroPagina = new SqlParameter();
                NumeroPagina.ParameterName = "@NumeroPagina";
                NumeroPagina.SqlDbType = SqlDbType.Int;
                NumeroPagina.Value = pNumeroPagina;
                SqlComando.Parameters.Add(NumeroPagina);

                SqlParameter RegistrosPorPagina = new SqlParameter();
                RegistrosPorPagina.ParameterName = "@RegistrosPorPagina";
                RegistrosPorPagina.SqlDbType = SqlDbType.Int;
                RegistrosPorPagina.Value = pRegistrosPorPagina;
                SqlComando.Parameters.Add(RegistrosPorPagina);

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }

            catch (Exception ex)
            {
                TablaDatos = null;
                throw new Exception(error + procedimiento + "\n" + ex.Message, ex);
            }

            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            return TablaDatos;
        }

        public int tamanoProductoBuscadoPorSucursal(string pProductoBuscado, int pSucursal, int pRegistrosPorPagina)
        {
            SqlConnection SqlConexion = new SqlConnection();
            string procedimiento = "tamanoProductoBuscadoPorSucursal";
            int totalPaginas = 1;

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = procedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_Buscado = new SqlParameter();
                ParNombre_Buscado.ParameterName = "@ProductoBuscado";
                ParNombre_Buscado.SqlDbType = SqlDbType.VarChar;
                ParNombre_Buscado.Size = pProductoBuscado.Length;
                ParNombre_Buscado.Value = pProductoBuscado;
                SqlComando.Parameters.Add(ParNombre_Buscado);

                SqlParameter IdSucursal = new SqlParameter();
                IdSucursal.ParameterName = "@IdSucursal";
                IdSucursal.SqlDbType = SqlDbType.Int;
                IdSucursal.Value = pSucursal;
                SqlComando.Parameters.Add(IdSucursal);

                SqlParameter ParRegistrosPorPagina = new SqlParameter();
                ParRegistrosPorPagina.ParameterName = "@RegistrosPorPagina";
                ParRegistrosPorPagina.SqlDbType = SqlDbType.Int;
                ParRegistrosPorPagina.Value = pRegistrosPorPagina;
                SqlComando.Parameters.Add(ParRegistrosPorPagina);

                SqlParameter ParTotalPaginas = new SqlParameter();
                ParTotalPaginas.ParameterName = "@TotalPaginas";
                ParTotalPaginas.SqlDbType = SqlDbType.Int;
                ParTotalPaginas.Direction = ParameterDirection.Output;
                SqlComando.Parameters.Add(ParTotalPaginas);

                SqlComando.ExecuteNonQuery();

                totalPaginas = (int)SqlComando.Parameters["@TotalPaginas"].Value;
            }

            catch (Exception ex)
            {
                throw new Exception(error + procedimiento + "\n" + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return totalPaginas;
        }

        // no implementados

        public CD_Productos(int parId_Producto, string parNombre_Producto, string parNombre_Categoria,
    decimal parPrecio_Unitario, string parDetalles, string parNombre_Buscado)
        {
            Id_Producto = parId_Producto;
            Nombre_Producto = parNombre_Producto;
            Nombre_Categoria = parNombre_Categoria;
            Precio_Unitario = parPrecio_Unitario;
            Detalles = parDetalles;
            Nombre_Buscado = parNombre_Buscado;
        }

        public DataTable Mostrar(int pNumeroPagina, int pRegistrosPorPagina)
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();
            string procedimiento = "mostrarProductos";

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = procedimiento;
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNumeroPagina = new SqlParameter();
                ParNumeroPagina.ParameterName = "@NumeroPagina";
                ParNumeroPagina.SqlDbType = SqlDbType.Int;
                ParNumeroPagina.Value = pNumeroPagina;
                SqlComando.Parameters.Add(ParNumeroPagina);

                SqlParameter ParRegistrosPorPagina = new SqlParameter();
                ParRegistrosPorPagina.ParameterName = "@RegistrosPorPagina";
                ParRegistrosPorPagina.SqlDbType = SqlDbType.Int;
                ParRegistrosPorPagina.Value = pRegistrosPorPagina;
                SqlComando.Parameters.Add(ParRegistrosPorPagina);

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }

            catch (SqlException ex)
            {
                TablaDatos = null;
                throw new Exception(error + procedimiento + "\n" + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return TablaDatos;
        }



        public DataTable ListaProductos()
        {
            DataTable TablaDatos = new DataTable();
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Produccion.ListaProductos";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }

            catch (Exception ex)
            {
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado Produccion.ListaProductos. " + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return TablaDatos;
        }

        public decimal PrecioProducto(int parIdBuscado)
        {
            decimal precio = 0;
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Produccion.PrecioProducto";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdBuscado = new SqlParameter();
                ParIdBuscado.ParameterName = "@IdBuscado";
                ParIdBuscado.SqlDbType = SqlDbType.Int;
                ParIdBuscado.Value = parIdBuscado;
                SqlComando.Parameters.Add(ParIdBuscado);

                SqlParameter ParPrecio = new SqlParameter();
                ParPrecio.ParameterName = "@Precio";
                ParPrecio.SqlDbType = SqlDbType.Money;
                ParPrecio.Direction = ParameterDirection.Output;
                SqlComando.Parameters.Add(ParPrecio);

                SqlComando.ExecuteNonQuery();

                precio = Convert.ToDecimal(ParPrecio.Value.ToString());

            }

            catch (Exception ex)
            {
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado Produccion.PrecioProducto. " + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return precio;
        }






        public string Insertar(CD_Productos parProductos)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Produccion.InsertarProducto";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_Producto = new SqlParameter();
                ParNombre_Producto.ParameterName = "@Nombre_Producto";
                ParNombre_Producto.SqlDbType = SqlDbType.VarChar;
                ParNombre_Producto.Size = parProductos.Nombre_Producto.Length;
                ParNombre_Producto.Value = parProductos.Nombre_Producto;
                SqlComando.Parameters.Add(ParNombre_Producto);

                SqlParameter ParNombre_Categoria = new SqlParameter();
                ParNombre_Categoria.ParameterName = "@Nombre_Categoria";
                ParNombre_Categoria.SqlDbType = SqlDbType.VarChar;
                ParNombre_Categoria.Size = parProductos.Nombre_Categoria.Length;
                ParNombre_Categoria.Value = parProductos.Nombre_Categoria;
                SqlComando.Parameters.Add(ParNombre_Categoria);

                SqlParameter ParPrecio_Unitario = new SqlParameter();
                ParPrecio_Unitario.ParameterName = "@Precio_Unitario";
                ParPrecio_Unitario.SqlDbType = SqlDbType.Money;
                ParPrecio_Unitario.Value = parProductos.Precio_Unitario;
                SqlComando.Parameters.Add(ParPrecio_Unitario);

                SqlParameter ParDetalles = new SqlParameter();
                ParDetalles.ParameterName = "@Detalles";
                ParDetalles.SqlDbType = SqlDbType.VarChar;
                ParDetalles.Size = parProductos.Detalles.Length;
                ParDetalles.Value = parProductos.Detalles;
                SqlComando.Parameters.Add(ParDetalles);

                SqlComando.ExecuteNonQuery();
                Respuesta = "Y";
            }

            catch (SqlException ex)
            {
                if (ex.Number == 8152)
                {
                    Respuesta = "Has introducido demasiados caracteres en uno de los campos";
                }
                else if (ex.Number == 2627)
                {
                    Respuesta = "Ya existe un producto con ese Nombre";
                }
                else if (ex.Number == 515)
                {
                    Respuesta = "No puedes dejar los campos Nombre y Categoría vacíos";
                }
                else if (ex.Number == 50000)
                {
                    Respuesta = "Debes ingresar el nombre de una Categoría existente. Para ingresar una nueva Categoría, primero debes crearla.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado Produccion.InsertarProducto. " + ex.Message;
                }
            }

            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            return Respuesta;
        }

        public string Eliminar(CD_Productos parProductos)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Produccion.EliminarProducto";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParId_Producto = new SqlParameter();
                ParId_Producto.ParameterName = "@Id_Producto";
                ParId_Producto.SqlDbType = SqlDbType.Int;
                ParId_Producto.Value = parProductos.Id_Producto;
                SqlComando.Parameters.Add(ParId_Producto);

                SqlComando.ExecuteNonQuery();
                Respuesta = "Y";
            }

            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    Respuesta = "No puedes eliminar un producto presente en un Pedido. Debes eliminar o actualizar los Pedidos antes de eliminar el producto.";
                }

                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado Produccion.EliminarProducto. " + ex.Message;
                }
            }

            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            return Respuesta;
        }

        public string Editar(CD_Productos parProductos)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = Conexion.BasedeDatos;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Produccion.EditarProducto";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParId_Producto = new SqlParameter();
                ParId_Producto.ParameterName = "@Id_Producto";
                ParId_Producto.SqlDbType = SqlDbType.Int;
                ParId_Producto.Value = parProductos.Id_Producto;
                SqlComando.Parameters.Add(ParId_Producto);

                SqlParameter ParNombre_Producto = new SqlParameter();
                ParNombre_Producto.ParameterName = "@Nombre_Producto";
                ParNombre_Producto.SqlDbType = SqlDbType.VarChar;
                ParNombre_Producto.Size = parProductos.Nombre_Producto.Length;
                ParNombre_Producto.Value = parProductos.Nombre_Producto;
                SqlComando.Parameters.Add(ParNombre_Producto);

                SqlParameter ParNombre_Categoria = new SqlParameter();
                ParNombre_Categoria.ParameterName = "@Nombre_Categoria";
                ParNombre_Categoria.SqlDbType = SqlDbType.VarChar;
                ParNombre_Categoria.Size = parProductos.Nombre_Categoria.Length;
                ParNombre_Categoria.Value = parProductos.Nombre_Categoria;
                SqlComando.Parameters.Add(ParNombre_Categoria);

                SqlParameter ParPrecio_Unitario = new SqlParameter();
                ParPrecio_Unitario.ParameterName = "@Precio_Unitario";
                ParPrecio_Unitario.SqlDbType = SqlDbType.Money;
                ParPrecio_Unitario.Value = parProductos.Precio_Unitario;
                SqlComando.Parameters.Add(ParPrecio_Unitario);

                SqlParameter ParDetalles = new SqlParameter();
                ParDetalles.ParameterName = "@Detalles";
                ParDetalles.SqlDbType = SqlDbType.VarChar;
                ParDetalles.Size = parProductos.Detalles.Length;
                ParDetalles.Value = parProductos.Detalles;
                SqlComando.Parameters.Add(ParDetalles);

                SqlComando.ExecuteNonQuery();
                Respuesta = "Y";
            }

            catch (SqlException ex)
            {
                if (ex.Number == 8152)
                {
                    Respuesta = "Has introducido demasiados caracteres en uno de los campos";
                }
                else if (ex.Number == 2627)
                {
                    Respuesta = "Ya existe un producto con ese Nombre";
                }
                else if (ex.Number == 515)
                {
                    Respuesta = "No puedes dejar los campos Nombre y Categoría vacíos";
                }
                else if (ex.Number == 50000)
                {
                    Respuesta = "Debes ingresar el nombre de una Categoría existente. Para ingresar una nueva Categoría, primero debes crearla.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado Produccion.EditarProducto. " + ex.Message;
                }
            }

            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            return Respuesta;
        }
    }
}