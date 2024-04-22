using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class Cruiseschip : PassagiersSchip
    {
        public Cruiseschip(int aantalPassagiers, List<string> traject, double lengte, double breedte, double tonnage, string naam) : base(aantalPassagiers, traject, lengte, breedte, tonnage, naam)
        {
        }
    }
}
