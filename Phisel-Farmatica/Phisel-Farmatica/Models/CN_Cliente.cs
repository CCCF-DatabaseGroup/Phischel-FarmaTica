using System.Data;

namespace Phisel_Farmatica.Models
{
    public class CN_Cliente
    {
        private static DataRow clienteActual;
        private static int idCliente;

        public CN_Cliente()
        {

        }

        // obtener

        public static DataRow getCliente(int idCliente)
        {
            return new CD_Cliente().obtenerDatos(idCliente);
        }

        public static string header_Prioridad()
        {
            return new CD_Cliente().header_prioridad;
        }

        public static string header_historialPadecimientos()
        {
            return new CD_Cliente().header_padecimientos;
        }


    }
}
