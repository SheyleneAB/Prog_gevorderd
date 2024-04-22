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
        public void VoegVlootToe(Vloot vlot)
        {
            if ((vlot == null) || (Vloten.Contains(vlot))) throw new Exception("Voegvloottoe");
            Vloten.Add(vlot);
        }
        public void VerwijderVloot(string vlootNaam)
        {
            bool boolnamen = false;
            if ((vlootNaam == null))
            {
                throw new Exception("VerwijderVlot");
            }
            foreach(var vlot in Vloten)
            {
                if(vlot.naam.Contains(vlootNaam)) {
                    boolnamen = true;
                }
            }
            if (!boolnamen)
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
        }
        public void VerplaatsSchipNaarAndereVloot(Schip schip, Vloot bronVloot, Vloot doelVloot)
        {
            if (bronVloot == null || doelVloot == null)
            {
                Console.WriteLine("Bronvloot of doelvloot bestaat niet.");
                return;
            }

            if (bronVloot.Schepen.Contains(schip))
            {
                doelVloot.VoegSchipToe(schip);
                bronVloot.VerwijderSchip(schip);
                Console.WriteLine($"Schip '{schip.Naam}' is succesvol verplaatst naar vloot '{doelVloot.naam}'.");
            }
            else
            {
                Console.WriteLine($"Schip '{schip.Naam}' bevindt zich niet in de bronvloot '{bronVloot.naam}'.");
            }
        }
        public void OverzichtHavensPerRederij(Rederij rederij)
        {
            if (rederij == null)
            {
                Console.WriteLine("Rederij bestaat niet.");
                return;
            }

            var gesorteerdeHavens = rederij.havens.OrderBy(haven => haven).ToList();

            Console.WriteLine($"Overzicht van havens van rederij :");
            foreach (var haven in gesorteerdeHavens)
            {
                Console.WriteLine(haven);
            }
        }
        public void VoegHavensToe(string haven)
        {
            if (( haven == null) || (havens.Contains(haven))) throw new Exception("Voeghaventoe");
        }
        public void VerwijderHavens(string haven)
        {
            if ((haven == null) || (!havens.Contains(haven))) throw new Exception("Verwijderhaven");
            havens.Remove(haven);

        }
        public Dictionary<Vloot, double> TonnageVloten() 
        {
            var tonnagePerVloot = new Dictionary<Vloot, double>();
            foreach (var vloot in Vloten)
            {
                double totaleTonnage = vloot.Schepen.Sum(schip => schip.Tonnage);
                tonnagePerVloot.Add(vloot, totaleTonnage);
                tonnagePerVloot.OrderByDescending(kv => kv.Value).ToDictionary(kv => kv.Key, kv => kv.Value);
            }
            return tonnagePerVloot;
        }
        public double AantalVolume() 
        {
            double totaalVolumeTankers = 0;
            foreach (var vloot in Vloten)
            {
                totaalVolumeTankers += vloot.Schepen.OfType<TankerSchip>().Sum(schip => (schip as TankerSchip)?.Volume ?? 0);
            }
            return totaalVolumeTankers;
        }

        public double TotaleCargowaarde()
        {
            double totaleCargowaarde = 0;
            foreach (var vloot in Vloten)
            {
                foreach (var schip in vloot.Schepen)
                {
                    if (schip is CargoSchip)
                    {
                        totaleCargowaarde += ((CargoSchip)schip).Cargowaarde;
                    }
                }
            }
            return totaleCargowaarde;
        }

        public int BeschikbareSleepboten()
        {
            return Vloten.Sum(vloot => vloot.Schepen.OfType<Sleepboot>().Count());
        }

        public Schip ZoekSchipOpNaam(string schipNaam)
        {
            return Vloten.SelectMany(vloot => vloot.Schepen)
                         .FirstOrDefault(schip => schip.Naam == schipNaam);
        }
        public int TotaalAantalPassagiers()
        {
            int totaalAantalPassagiers = 0;
            foreach (var vloot in Vloten)
            {
                totaalAantalPassagiers += vloot.Schepen.OfType<PassagiersSchip>().Sum(schip => schip.AantalPassagiers);
            }
            return totaalAantalPassagiers;
        }

        
    }
}
