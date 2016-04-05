using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phisel_Farmatica.Models
{
    public class OBJ_Conexion
    {
        public static string conectar()
        {
            return CN_Conexion.ChequearConexion();
        }
    }
}
