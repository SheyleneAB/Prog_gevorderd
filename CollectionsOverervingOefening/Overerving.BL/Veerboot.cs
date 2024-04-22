using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    internal class Veerboot : PassagiersSchip
    {
        public Veerboot(int aantalPassagiers, List<string> traject, double lengte, double breedte, double tonnage, string naam) : base(aantalPassagiers, traject, lengte, breedte, tonnage, naam)
        {
        }
    }
}
