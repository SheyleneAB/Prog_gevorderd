using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisStatsBL.interfaces
{
    public interface IFileProcessor
    {
        public List<string> LeesSoorten(string fileName);
        public List<string> LeesHavens(string fileName);
    }
    
}
