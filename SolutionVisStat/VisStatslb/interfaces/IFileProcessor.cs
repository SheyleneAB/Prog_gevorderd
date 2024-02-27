using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisStatsBL.MODEL;

namespace VisStatsBL.interfaces
{
    public interface IFileProcessor
    {
        public List<string> LeesSoorten(string fileName);
        public List<string> LeesHavens(string fileName);
        public List<VisStatsDataRecord> LeesStatistieken(string fileName, List<Vissoort> vissoorten, List<Haven> havens);
    }
    
}
