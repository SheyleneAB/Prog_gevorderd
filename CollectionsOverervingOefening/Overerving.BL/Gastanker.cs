using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class Gastanker : TankerSchip
    {
        public Gastanker(Ladinggastanker lading, double volume, double lengte, double breedte, double tonnage, string naam) 
            : base(volume, lengte, breedte, tonnage, naam)
        {
            Lading = lading;
        }

        

        public Ladinggastanker Lading { get; set; }
        
    }
}
