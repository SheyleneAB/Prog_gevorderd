using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    internal class Olietanker : Schip
    {
        public double cargowaarde { get; set; }
        public int volume { get; set; }
        public LadingOlietanker lading { get; set; }
    }
}
