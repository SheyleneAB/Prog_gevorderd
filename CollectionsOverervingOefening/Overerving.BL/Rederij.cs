using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class Rederij
    {
        public List<Vloot> Vloten;
        public List<string> havens;
        public void VoegVlootToe()
        {

        }
        public void VerwijderVloot(string vlootNaam)
        {
            foreach (var vloot in Vloten)
            {
                if (vloot.naam == vlootNaam)
                {
                    Vloten.Remove(vloot);
                    return;
                }
            }
        }
        public void VoegHavensToe()
        {

        }
        public void VerwijderHavens()
        {

        }
        public double TotaleCargowaarde()
        {
            double totaleCargowaarde = 0;

            foreach (var vloot in Vloten)
            {
                foreach (var schip in vloot.Schepen)
                {
                    if (!(schip is Olietanker) && !(schip is Gastanker))
                    {
                        if (schip is ISchipMetCargowaarde)
                        {
                            totaleCargowaarde += ((ISchipMetCargowaarde)schip).Cargowaarde;
                        }
                    }
                }
            }

            return totaleCargowaarde;
        }
        public int TotaalPassagiers()
        {
            return 0;
        }
        public Dictionary<Vloot, double> TonnageVloten() 
        {
            return new Dictionary<Vloot, double>();
        }
        public double AantalVolume() 
        {
            return 0; 
        }
        public List<Schip> BeschikbareSleepboten () 
        {
            return new List<Schip>();
        }
        public Schip InfoSchip(string s)
        {
            
            foreach (var vloot in Vloten)
            {
                foreach (var schip in vloot.Schepen)
                {
                    if (schip.Naam == s)
                    {
                        return schip;
                    }
                }
            }
            return null;
        }
        /*
        •De totale cargowaarde van deschepen die tot een rederij behoren.  
        •Het totaal aantal passagiers.
        •De tonnage per vloot op te lijsten (van groot naar klein).
        •Het totaal aantal volume die de tankers kunnen vervoeren.
        •De beschikbare sleepboten
        */
    }
}
