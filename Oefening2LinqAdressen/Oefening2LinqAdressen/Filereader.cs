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

                    // Check if the current province exists
                    if (provincie.Naam != provincieNaam)
                    {
                        provincie = new Provincie { Naam = provincieNaam };
                        steden = new List<Stad>();
                        provincie.Steden = steden;
                    }

                    // Check if the current city exists
                    Stad stad = steden.Find(s => s.Naam == stadNaam);
                    if (stad == null)
                    {
                        stad = new Stad { Naam = stadNaam, Straatnamen = new List<Straatnamen>() }; // Initialize the Straatnamen list
                        steden.Add(stad);
                    }

                    // Add street name to the city
                    stad.Straatnamen.Add(new Straatnamen { Naam = straatNaam });
                }
            }

            return provincie;
        }
        public void GetSortedProvincieNamen()
        {
            Provincie provincie = ReadDataFromFile();

            // Create a list of all province names
            List<string> provincieNamen = provincie.Steden.Select(s => s.Naam).Distinct().ToList();

            // Sort the list alphabetically
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

            // Find the city (Stad) with the specified name
            Stad foundStad = provincie.Steden.FirstOrDefault(s => s.Naam == stad);
            if (foundStad == null)
            {
                Console.WriteLine($"Gemeente '{stad}' niet gevonden.");
                return;
            }

            // Print each street name for the municipality
            Console.WriteLine($"Straatnamen voor gemeente '{stad}':");
            foreach (var straat in foundStad.Straatnamen)
            {
                Console.WriteLine(straat.Naam);
            }
        }
    }
}
    


