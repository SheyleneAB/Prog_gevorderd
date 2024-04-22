using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class Containerschip : CargoSchip
    {
        public int Aantalcontainers;

        public Containerschip(int aantalcontainers, double lengte, double breedte, double tonnage, string naam, double cargowaarde) 
            : base(lengte, breedte, tonnage, naam, cargowaarde)
        {
            Aantalcontainers = aantalcontainers;
        }

    }
}
