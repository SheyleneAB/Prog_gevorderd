using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisStatsBL.Exceptions;
using VisStatsBL.interfaces;
using VisStatsBL.MODEL;

namespace VisStatsBL.Manager
{
    public class VisStatManager
    {
        private IFileProcessor fileProcessor;
        private IVisStatRepository visStatsRepository;

        public VisStatManager(IFileProcessor fileProcessor, IVisStatRepository visStatsRepositrory)
        {
            this.fileProcessor = fileProcessor;
            this.visStatsRepository = visStatsRepositrory;
        }

        public void UploadVissoorten(string fileName) 
        {
            List<string> soorten = fileProcessor.LeesSoorten(fileName);
            List<Vissoort> vissoorten = MaakVissoorten(soorten);
            foreach (Vissoort vissoort in vissoorten)
            {
                if (!visStatsRepository.HeeftVissoort(vissoort))
                    visStatsRepository.SchrijfVissoort(vissoort);
            }
        }
        private List<Vissoort> MaakVissoorten (List<string> soorten)
        {
            Dictionary<string, Vissoort> visSoorten = new();
            foreach (var soort in soorten)
            {
                if (!visSoorten.ContainsKey(soort))
                {
                    try
                    {
                        visSoorten.Add(soort, new Vissoort(soort));
                    }
                    catch (DomeinException)
                    {

                    }
                }
            }
            return visSoorten.Values.ToList();
        }
    }
}
