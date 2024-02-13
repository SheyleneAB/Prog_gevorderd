using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisStatslb.interfaces;
using VisStatslb.MODEL;

namespace VisStatslb.Manager
{
    internal class VisStatManager
    {
        private IFileProcessor fileProcessor;
        private IVisStatsRepository visStatsRepositrory;
        public void UploadVissoorten(string fileName) 
        {
            List<string> soorten = fileProcessor.LeesSoorten(fileName);
            List<Vissoort> vissoorten = MaakVissoorten(soorten);
            foreach (Vissoort vissoort in vissoorten)
            {
                if (!visStatsRepositrory.HeeftVissoort(vissoort))
                    visStatsRepositrory.schrijfVissoort(vissoort);
            }
        }
        private List<Vissoort> MaakVissoorten (List<string> soorten)
        {
            //todo implementeer maaksoorten
            return null;
        }
    }
}
