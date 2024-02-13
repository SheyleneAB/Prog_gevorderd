using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisStatslb.interfaces
{
    internal interface IFileProcessor
    {
        public List<string> LeesSoorten(string fileName);
    }
}
