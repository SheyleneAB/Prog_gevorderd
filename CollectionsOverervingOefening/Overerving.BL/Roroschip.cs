using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class Roroschip : Containerschip
    {
        public int AantalAutos { get; set; }
        public int AantalTrucks { get; set; }

        public Roroschip(int aantalAutos, int aantalTrucks, int aantalcontainers, double lengte, double breedte,
            double tonnage, string naam, double cargowaarde) 
            : base(aantalcontainers, lengte, breedte, tonnage, naam, cargowaarde)

        {
            AantalAutos = aantalAutos;
            AantalTrucks = aantalTrucks;
        }

    }
}
