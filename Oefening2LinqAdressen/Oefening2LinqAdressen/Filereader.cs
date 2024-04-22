using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oefening2LinqAdressen
{
    internal class Filereader
    {
        private string filePath;

        public Filereader(string filePath)
        {
            this.filePath = filePath;
        }

        public Provincie ReadDataFromFile()
        {
            Provincie provincie = new Provincie();
            List<Stad> steden = new List<Stad>();
            List<Straatnamen> straatnamen = new List<Straatnamen>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    string provincieNaam = parts[0];
                    string stadNaam = parts[1];
                    string straatNaam = parts[2];

                    
                    if (provincie.Naam != provincieNaam)
                    {
                        provincie = new Provincie { Naam = provincieNaam };
                        steden = new List<Stad>();
                        provincie.Steden = steden;
                    }

                   
                    Stad stad = steden.Find(s => s.Naam == stadNaam);
                    if (stad == null)
                    {
                        stad = new Stad { Naam = stadNaam, Straatnamen = new List<Straatnamen>() }; // Initialize the Straatnamen list
                        steden.Add(stad);
                    }

                    
                    stad.Straatnamen.Add(new Straatnamen { Naam = straatNaam });
                }
            }

            return provincie;
        }
        public void GetSortedProvincieNamen()
        {
            Provincie provincie = ReadDataFromFile();

            
            List<string> provincieNamen = provincie.Steden.Select(s => s.Naam).Distinct().ToList();

            
            provincieNamen.Sort();

            Console.WriteLine("Provincie namen, alfabetisch gesorteerd:");
            foreach (var naam in provincieNamen)
            {
                Console.WriteLine(naam);
            }
        }
        public void PrintStraatnamenForGemeente(string stad)
        {
            //Heeft werk nodig
            Provincie provincie = ReadDataFromFile();

            
            Stad foundStad = provincie.Steden.FirstOrDefault(s => s.Naam == stad);
            if (foundStad == null)
            {
                Console.WriteLine($"Gemeente '{stad}' niet gevonden.");
                return;
            }

           
            Console.WriteLine($"Straatnamen voor gemeente '{stad}':");
            foreach (var straat in foundStad.Straatnamen)
            {
                Console.WriteLine(straat.Naam);
            }
        }
        public void PrintMeesttFrequenteStraatnamen()
        {
            Provincie provincie = ReadDataFromFile();

            
            Dictionary<string, int> streetCounts = new Dictionary<string, int>();
            foreach (var stad in provincie.Steden)
            {
                foreach (var straat in stad.Straatnamen)
                {
                    if (!streetCounts.ContainsKey(straat.Naam))
                    {
                        streetCounts.Add(straat.Naam, 1);
                    }
                    else
                    {
                        streetCounts[straat.Naam]++;
                    }
                }
            }

           
            var mostFrequentStreet = streetCounts.OrderByDescending(kv => kv.Value).FirstOrDefault();
            if (mostFrequentStreet.Equals(default(KeyValuePair<string, int>)))
            {
                Console.WriteLine("No street names found.");
                return;
            }

            
            foreach (var stad in provincie.Steden.OrderBy(s => s.Naam))
            {
                foreach (var straat in stad.Straatnamen.Where(s => s.Naam == mostFrequentStreet.Key))
                {
                    Console.WriteLine($"Provincie: {provincie.Naam}, Gemeente: {stad.Naam}, Straat: {straat.Naam}");
                }
            }
        }
        public void PrintMostFrequentStraatnamen(int numberOfStreetNames)
        {
            Provincie provincie = ReadDataFromFile();

            
            Dictionary<string, int> streetCounts = new Dictionary<string, int>();
            foreach (var stad in provincie.Steden)
            {
                foreach (var straat in stad.Straatnamen)
                {
                    if (!streetCounts.ContainsKey(straat.Naam))
                    {
                        streetCounts.Add(straat.Naam, 1);
                    }
                    else
                    {
                        streetCounts[straat.Naam]++;
                    }
                }
            }

            
            var mostFrequentStreets = streetCounts.OrderByDescending(kv => kv.Value).Take(numberOfStreetNames).ToList();
            if (mostFrequentStreets.Count == 0)
            {
                Console.WriteLine("No street names found.");
                return;
            }

            
            foreach (var street in mostFrequentStreets)
            {
                Console.WriteLine($"Most Frequent Street: {street.Key}, Occurrences: {street.Value}");
                foreach (var stad in provincie.Steden.OrderBy(s => s.Naam))
                {
                    foreach (var straat in stad.Straatnamen.Where(s => s.Naam == street.Key))
                    {
                        Console.WriteLine($"Provincie: {provincie.Naam}, Gemeente: {stad.Naam}, Straat: {straat.Naam}");
                    }
                }
                Console.WriteLine();
            }
        }
        public void PrintGemeenschapStraatnamentssnGemeentes(string gemeente1, string gemeente2)
        {
            Provincie provincie = ReadDataFromFile();

            
            Stad stad1 = provincie.Steden.FirstOrDefault(s => s.Naam == gemeente1);
            Stad stad2 = provincie.Steden.FirstOrDefault(s => s.Naam == gemeente2);

            if (stad1 == null || stad2 == null)
            {
                Console.WriteLine("Een of beide gemeenten niet gevonden.");
                return;
            }

            
            var GemeenschappelijkeStraatnamen = stad1.Straatnamen.Select(straat1 => straat1.Naam)
                                                      .Intersect(stad2.Straatnamen.Select(straat2 => straat2.Naam))
                                                      .ToList();

            
            if (GemeenschappelijkeStraatnamen.Count == 0)
            {
                Console.WriteLine("Geen gemeenschappelijke straatnamen gevonden.");
                return;
            }

            Console.WriteLine($"Gemeenschappelijke straatnamen tussen {gemeente1} en {gemeente2}:");
            foreach (var straatNaam in GemeenschappelijkeStraatnamen)
            {
                Console.WriteLine(straatNaam);
            }
        }
        public void PrintStraatnamenEnkelnGemeente(string gemeente)
        {
            //Klopt niet!!!
            Provincie provincie = ReadDataFromFile();

            // Zoek de Stad voor de opgegeven gemeente
            Stad stad = provincie.Steden.FirstOrDefault(s => s.Naam == gemeente);

            if (stad == null)
            {
                Console.WriteLine("Gemeente niet gevonden.");
                return;
            }

            // Verzamel alle straatnamen in de opgegeven gemeente
            var straatnamenInGemeente = stad.Straatnamen.Select(straat => straat.Naam).ToList();

            // Voor elke andere gemeente behalve de opgegeven gemeente, verzamel alle straatnamen
            List<string> straatnamenInAndereGemeenten = new List<string>();
            foreach (var andereStad in provincie.Steden.Where(s => s.Naam != gemeente))
            {
                straatnamenInAndereGemeenten.AddRange(andereStad.Straatnamen.Select(straat => straat.Naam));
            }

            // Identificeer de straatnamen die alleen voorkomen in de opgegeven gemeente en niet in andere gemeenten
            var straatnamenAlleenInGemeente = straatnamenInGemeente.Except(straatnamenInAndereGemeenten).ToList();

            // Geef de straatnamen weer die alleen voorkomen in de opgegeven gemeente
            if (straatnamenAlleenInGemeente.Count == 0)
            {
                Console.WriteLine("Geen straatnamen gevonden die alleen voorkomen in de opgegeven gemeente.");
                return;
            }

            Console.WriteLine($"Straatnamen die alleen voorkomen in de gemeente '{gemeente}':");
            foreach (var straatNaam in straatnamenAlleenInGemeente)
            {
                Console.WriteLine(straatNaam);
            }
        }
        public void PrintGemeenteMetMeesteStraatnamen()
        {
            Provincie provincie = ReadDataFromFile();

            // de gemeente met het hoogste aantal straatnamen
            var gemeenteMetMeesteStraatnamen = provincie.Steden
                .OrderByDescending(s => s.Straatnamen.Count)
                .FirstOrDefault();

            if (gemeenteMetMeesteStraatnamen != null)
            {
                Console.WriteLine($"Gemeente met het hoogste aantal straatnamen: {gemeenteMetMeesteStraatnamen.Naam} ({gemeenteMetMeesteStraatnamen.Straatnamen.Count} straatnamen)");
            }
            else
            {
                Console.WriteLine("Geen gemeenten gevonden.");
            }
        }
        public void PrintLangsteStraatnaam()
        {
            Provincie provincie = ReadDataFromFile();

            //langste straatnaam 
            var langsteStraatnaam = provincie.Steden
                .SelectMany(s => s.Straatnamen) 
                .OrderByDescending(straat => straat.Naam.Length) 
                .FirstOrDefault(); 

            if (langsteStraatnaam != null)
            {
                Console.WriteLine($"Langste straatnaam: {langsteStraatnaam.Naam}");
            }
            else
            {
                Console.WriteLine("Geen straatnamen gevonden.");
            }
        }
        public void PrintLangsteStraatnaamMetLocatie()
        {
            //Heeft werk nodig
            Provincie provincie = ReadDataFromFile();

            // langste straatnaam en bijbehorende locatie bij te houden
            string langsteStraatnaam = null;
            string gemeente = null;
            string provincieNaam = null;

            // de langste straatnaam en bijbehorende locatie te vinden
            foreach (var stad in provincie.Steden)
            {
                var langsteStraatInStad = stad.Straatnamen.OrderByDescending(straat => straat.Naam.Length).FirstOrDefault();
                if (langsteStraatInStad != null && (langsteStraatnaam == null || langsteStraatInStad.Naam.Length > langsteStraatnaam.Length))
                {
                    langsteStraatnaam = langsteStraatInStad.Naam;
                    gemeente = stad.Naam;
                    provincieNaam = provincie.Naam;
                }
            }

            // Geef de langste straatnaam samen met de bijbehorende gemeente en provincie weer
            if (langsteStraatnaam != null)
            {
                Console.WriteLine($"Langste straatnaam: {langsteStraatnaam}");
                Console.WriteLine($"Gemeente: {gemeente}");
                Console.WriteLine($"Provincie: {provincieNaam}");
            }
            else
            {
                Console.WriteLine("Geen straatnamen gevonden.");
            }
        }
        public void PrintUniekeStraatnamenMetLocatie()
        {
            Provincie provincie = ReadDataFromFile();

            //unieke straatnamen en bijbehorende locaties te vinden
            var uniekeStraatnamenMetLocatie = provincie.Steden
                .SelectMany(stad => stad.Straatnamen.Select(straat => new
                {
                    Straatnaam = straat.Naam,
                    Gemeente = stad.Naam,
                    Provincie = provincie.Naam
                }))
                .GroupBy(straat => straat.Straatnaam) // Groepeer op straatnaam
                .Select(group => group.First()) // Selecteer het eerste element van elke groep (unieke straatnaam)
                .OrderBy(straat => straat.Straatnaam); // Sorteer op straatnaam

            // Geef de unieke straatnamen samen met de bijbehorende gemeente en provincie weer
            if (uniekeStraatnamenMetLocatie.Any())
            {
                Console.WriteLine("Unieke straatnamen met locatie:");
                foreach (var straat in uniekeStraatnamenMetLocatie)
                {
                    Console.WriteLine($"Straatnaam: {straat.Straatnaam}, Gemeente: {straat.Gemeente}, Provincie: {straat.Provincie}");
                }
            }
            else
            {
                Console.WriteLine("Geen unieke straatnamen gevonden.");
            }
        }
        public void PrintUniekeStraatnamenVoorGemeente(string gemeente)
        {
            Provincie provincie = ReadDataFromFile();

            // Zoek de stad voor de opgegeven gemeente
            var stad = provincie.Steden.FirstOrDefault(s => s.Naam == gemeente);

            if (stad == null)
            {
                Console.WriteLine($"Gemeente '{gemeente}' niet gevonden.");
                return;
            }

            //unieke straatnamen voor de opgegeven gemeente te vinden
            var uniekeStraatnamen = stad.Straatnamen
                .GroupBy(straat => straat.Naam) // Groepeer op straatnaam
                .Where(group => group.Count() == 1) // Filter op straatnamen die slechts één keer voorkomen (unieke straatnamen)
                .Select(group => group.Key); // Selecteer de straatnamen uit de groepen

            // Geef de unieke straatnamen weer voor de opgegeven gemeente
            if (uniekeStraatnamen.Any())
            {
                Console.WriteLine($"Unieke straatnamen voor gemeente '{gemeente}':");
                foreach (var straatnaam in uniekeStraatnamen)
                {
                    Console.WriteLine(straatnaam);
                }
            }
            else
            {
                Console.WriteLine($"Geen unieke straatnamen gevonden voor gemeente '{gemeente}'.");
            }
        }

    }
}
    


