namespace Phisel_Farmatica.Models
{
    public class CD_Conexion
    {
        public string ChequearConexion()
        {
            return new Conexion().ChequearConexion();
        }
    }
}

        
