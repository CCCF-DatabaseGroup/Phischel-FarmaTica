using System;
using System.Data;

namespace Phisel_Farmatica.Models
{
    public class OBJ_Usuario
    {
        public static int idUsuarioActual;
        public static string cedula;
        public static string nombre;
        public static string apellido1;
        public static string apellido2;
        public static string telefono;
        public static string fechaNacimiento;
        public static int idDireccion;
        public static string provincia;
        public static string canton;
        public static string distrito;
        public static int prioridad;
        public static string historialPadecimientos;
        public static string contrasenaActual;
        public static int rangoUsuario;

        public OBJ_Usuario()
        {

        }

        public static bool setUsuario(string nickName, string contrasena)
        {
            DataRow usuario = CN_Usuario.obtenerUsuario(nickName, contrasena);

            if (usuario != null)
            {
                idUsuarioActual = Convert.ToInt32(usuario[CN_Usuario.header_idUsuario()].ToString());
                contrasenaActual = usuario[CN_Usuario.header_Contrasena()].ToString();
                rangoUsuario = Convert.ToInt32(usuario[CN_Usuario.header_Id_rango_TU()].ToString());
                setPersona();
                setCliente();
                setDireccion();
                return true;
            }
            else
            {
                return false;
            }

        }

        private static void setPersona()
        {
            DataRow persona = CN_Persona.getDatosPersona(idUsuarioActual);
            cedula = persona[CN_Persona.header_cedula()].ToString();
            nombre = persona[CN_Persona.header_nombre()].ToString();
            apellido1 = persona[CN_Persona.header_apellido1()].ToString();
            apellido2 = persona[CN_Persona.header_apellido2()].ToString();
            telefono = persona[CN_Persona.header_telefono()].ToString();
            fechaNacimiento = persona[CN_Persona.header_fechaNacimiento()].ToString();
            idDireccion = Convert.ToInt32(persona[CN_Persona.header_idDireccion()].ToString());
        }

        private static void setCliente()
        {
            DataRow cliente = CN_Cliente.getCliente(OBJ_Usuario.idUsuarioActual);
            prioridad = Convert.ToInt32(cliente[CN_Cliente.header_Prioridad()].ToString());
            historialPadecimientos = cliente[CN_Cliente.header_historialPadecimientos()].ToString();

        }

        private static void setDireccion()
        {
            DataRow direccion = CN_Localidad.obtenerDireccion(idDireccion);
            provincia = direccion[CN_Localidad.header_nombreProvincia()].ToString();
            canton = direccion[CN_Localidad.header_nombreCanton()].ToString();
            distrito = direccion[CN_Localidad.header_nombreDistrito()].ToString();

        }




    }
}
