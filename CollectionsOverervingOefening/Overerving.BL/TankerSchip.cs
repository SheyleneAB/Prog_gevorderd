using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class TankerSchip : Schip
    {
        public TankerSchip(double volume, double lengte, double breedte, double tonnage, string naam) 
            : base(lengte, breedte, tonnage, naam)
        {
            Volume = volume;
            
        }

        

        public double Volume { get; set; }
       
    }
}
