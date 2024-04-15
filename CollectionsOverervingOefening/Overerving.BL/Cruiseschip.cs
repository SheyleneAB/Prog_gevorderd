using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class Cruiseschip :Schip
    {
        public int aantalpassagiers { get; set; }
        public List<string> traject { get; set; }
    }
}
