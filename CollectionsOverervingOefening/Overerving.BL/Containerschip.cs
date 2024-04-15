using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class Containerschip : Schip
    {
        public int Aantalcontainers;
        public double? Cargowaarde;
        public Containerschip(double? cargowaarde)
        {
            Cargowaarde = cargowaarde;
        }
    }
}
