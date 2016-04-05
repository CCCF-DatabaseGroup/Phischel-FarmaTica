
namespace Phisel_Farmatica.Models
{
    public class CN_Conexion
    {
        public static string ChequearConexion()
        {
            return new CD_Conexion().ChequearConexion();
        }
    }
}
