using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    internal class Gastanker :Schip
    {
            public double? Cargowaarde { get; set; }
            public int volume { get; set; }
            public Ladinggastanker lading { get; set; }
        public Gastanker(double? cargowaarde)
        {
            Cargowaarde = cargowaarde;
        }
    }
}
