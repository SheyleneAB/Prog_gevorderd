using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    internal class Olietanker : TankerSchip
    {
        public Olietanker(LadingOlietanker lading, double volume,  double lengte, double breedte, double tonnage, string naam) 
            : base(volume, lengte, breedte, tonnage, naam)
        {

        }

       
        public LadingOlietanker lading { get; set; }
    }
}
