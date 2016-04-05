using System;
using System.Data;

namespace Phisel_Farmatica.Models
{
    public class CN_Farmacias
    {
        public static int Phishel = 1;
        public static int Farmatica = 2;
        public static int numeroFarmacia =1;
        private static DataRow farmaciaActual;

        public static void setFarmaciaActual(int famacia)
        {
            farmaciaActual = new CD_Farmacias().obtenerFarmacia(famacia);
            numeroFarmacia = Convert.ToInt32(farmaciaActual[header_idFarmacia()]);
        }

        private static string header_idFarmacia()
        {
            return new CD_Farmacias().header_idFarmacia;
        }
    }
}
