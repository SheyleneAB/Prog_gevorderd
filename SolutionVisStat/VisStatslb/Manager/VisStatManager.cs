using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        public void UploadVisHavens (string fileName)
        {
            List<string> havens = fileProcessor.LeesHavens(fileName);
            List<Haven> visHavens = MaakHavens(havens);
            foreach (Haven haven in visHavens)
            {
                if (!visStatsRepository.HeeftHaven(haven))
                    visStatsRepository.SchrijfHaven(haven);
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
        private List<Haven> MaakHavens (List<string> havens)
        {
            Dictionary<string, Haven> visHavens = new();
            foreach( var haven in havens)
            {
                if (!visHavens.ContainsKey(haven))
                {
                    try
                    {
                        visHavens.Add(haven, new Haven(haven));
                    }
                    catch (DomeinException)
                    {

                    }
                }
            }
            return visHavens.Values.ToList();
        }
        public void UploadStatistieken(string fileName)
        {
            try
            {
                if (!visStatsRepository.IsOpgeladen(fileName))
                {
                    List<Haven> havens = visStatsRepository.LeesHavens();
                    List<Vissoort> soorten = visStatsRepository.LeesVissoorten();
                    List<VisStatsDataRecord> data = fileProcessor.LeesStatistieken(fileName, soorten, havens);
                    visStatsRepository.SchrijfStatiestieken(data, fileName);
                }
            }
            catch (Exception ex) { throw new ManagerException("uploadstatieken", ex); }
        }
        public List<Haven> GeefHaven()
        {
            try
            {
                return visStatsRepository.LeesHavens();
            }
            catch (Exception)
            {
                throw new ManagerException("GeefHavens");
            }
        }
        public List<Vissoort> GeefVissoorten()
        {

            try
            {
                return visStatsRepository.LeesVissoorten();
            }
            catch (Exception)
            {
                throw new ManagerException("GeefVissoorten");
            }
        }
        public List<int> GeefJaartallen()
        {
            try
            {
                return visStatsRepository.LeesJaartallen();
            }
            catch (Exception)
            {
                throw new ManagerException("GeefJaartallen");
            }
        }
        public List<Jaarvangst> GeefVangst(int jaar, Haven haven, List<Vissoort> vissoorts, Eenheid eenheid)
        {
            try
            {
                return visStatsRepository.LeesStatistieken(jaar, haven, vissoorts, eenheid);
            }
            catch (Exception ex)
            {
                throw new ManagerException("Jaarvangst");
            }
        }
    }
}
