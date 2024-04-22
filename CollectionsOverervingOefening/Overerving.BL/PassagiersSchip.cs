using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class PassagiersSchip : Schip
    {
        public PassagiersSchip(int aantalPassagiers, List<string> traject, double lengte, double breedte, double tonnage, string naam) : base(lengte, breedte, tonnage, naam)
        {
            AantalPassagiers = aantalPassagiers;
            this.traject = traject;
        }

        public int AantalPassagiers { get; set; }
        public List<string> traject { get; set; }
    }
}

