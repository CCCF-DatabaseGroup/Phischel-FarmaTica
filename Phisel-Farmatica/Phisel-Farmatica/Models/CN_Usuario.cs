using System.Data;

namespace Phisel_Farmatica.Models
{
    public class CN_Usuario
    {

        public static int obtenerIdUsuario(string nickname, string contrasena)
        {
            return new CD_Usuario().obtenerIdUsuario(nickname, contrasena);
        }

        public static DataRow obtenerUsuario(string nickname, string contrasena)
        {
            return new CD_Usuario().obtenerUsuario(nickname, contrasena);
        }

        public static string header_idUsuario()
        {
            return new CD_Usuario().header_idUsuario;
        }

        public static string header_Nickname()
        {
            return new CD_Usuario().header_nickname;
        }

        public static string header_Contrasena()
        {
            return new CD_Usuario().header_contrasena;
        }

        public static string header_Correo_electronico()
        {
            return new CD_Usuario().header_correo_electronico;
        }

        public static string header_Id_rango_TU()
        {
            return new CD_Usuario().header_id_rango_TU;
        }

    }
}
