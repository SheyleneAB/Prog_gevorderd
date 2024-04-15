using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class Schip : ISchipMetCargowaarde
    {
        public double lengte;
        public double breedte;
        public double tonnage;
        public string Naam;
        /* equal methode toevoegen voor dubbels*/
        public double? Cargowaarde { get; set; }
    }
}
