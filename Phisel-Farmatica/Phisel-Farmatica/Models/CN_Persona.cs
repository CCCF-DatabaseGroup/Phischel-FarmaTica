using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phisel_Farmatica.Models
{
    public class CN_Persona
    {

        // obtener

        public static DataRow getDatosPersona(int idPersona)
        {
            return new CD_Persona().obtenerDatos(idPersona);
        }

        // headers

        public static string header_cedula()
        {
            return new CD_Persona().header_cedula;
        }

        public static string header_nombre()
        {
            return new CD_Persona().header_nombre;
        }

        public static string header_apellido1()
        {
            return new CD_Persona().header_apellido1;
        }

        public static string header_apellido2()
        {
            return new CD_Persona().header_apellido2;
        }

        public static string header_telefono()
        {
            return new CD_Persona().header_telefono;
        }

        public static string header_fechaNacimiento()
        {
            return new CD_Persona().header_fechaNacimiento;
        }

        public static string header_idDireccion()
        {
            return new CD_Persona().header_idDireccion;
        }


    }
}
