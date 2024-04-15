using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqOef
{
    internal class Program
    {
        
        
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\elyne\\Downloads\\CyclingData.txt";
            TrainingsDataRepository dataRepository = new TrainingsDataRepository();
                List<Trainingdata> trainingDataList = dataRepository.GetAllTrainingData(filePath);



            Wattagetussen(trainingDataList);
            Wattagetussenendurance(trainingDataList);

            KlantenMeerDan(trainingDataList);
            KadansenGemiddeldeKlein(trainingDataList);
            TopKlanten(trainingDataList);
            HevigeSessiesweergeven(trainingDataList);
            Trainingstatistiekenperklant(trainingDataList, 10);
            Weergeefklantmetcriteria(trainingDataList);
            Trainingstatistiekenperklant(trainingDataList, 10);

            Console.ReadLine();
                
        }

           
        public static void Wattagetussen(List<Trainingdata> trainingDataList)
        {
            Console.WriteLine("wattage met gemiddelde wattage onder 125 en maximum wattage over 200 ");
            foreach (var x in trainingDataList.Where(x => x.GemiddeldeWattage < 125 && x.MaximumWattage > 200))
            {
                Console.WriteLine($" {x.DatumUur} {x.Tijdsduur} {x.GemiddeldeWattage} {x.MaximumWattage} {x.GemiddeldeCadans}{x.MaximumCadans} {x.Trainingstype} {x.Commentaar}  {x.Klantnummer}");
              
            }
        }
        public static void Wattagetussenendurance(List<Trainingdata> trainingDataList)
        {
            Console.WriteLine("wattage met gemiddelde wattage onder 125 en maximum wattage over 200  en type endurance");
            foreach (var x in trainingDataList.Where(x => x.GemiddeldeWattage < 125 && x.MaximumWattage > 200 && x.Trainingstype == "endurance"))
            {
                Console.WriteLine($" {x.DatumUur} {x.Tijdsduur} {x.GemiddeldeWattage} {x.MaximumWattage} {x.GemiddeldeCadans}{x.MaximumCadans} {x.Trainingstype} {x.Commentaar}  {x.Klantnummer}");

            }
        }
        public static void KlantenMeerDan(List<Trainingdata> trainingDataList)
        {
            
            var filteredData = trainingDataList
                           .Where(data => data.DatumUur.Year == 2021 &&
                                          data.DatumUur.Month == 8 &&
                                          data.GemiddeldeWattage > 350)
                           .GroupBy(data => data.Klantnummer)
                           .Where(group => group.Count() > 5)
                           .Select(group => group.Key);

           
            if (filteredData.Any())
            {
                Console.WriteLine("Klanten die meer dan 5 trainingssessies hebben in augustus:");
                foreach (var klantnummer in filteredData)
                {
                    Console.WriteLine($"Klantnummer: {klantnummer}");
                }
            }
            else
            {
                Console.WriteLine("Er zijn geen klanten die voldoen aan de criteria.");
            }
        }
        public static void KadansenGemiddeldeKlein (List<Trainingdata> trainingDataList)
        {
            Console.WriteLine("de gemiddelde wattage kleiner is dan 100 of gemiddelde kadans kleiner dan 75");
            foreach (var x in trainingDataList.Where(x => x.DatumUur.Year == 2021 &&
                               (x.GemiddeldeWattage < 100 || x.GemiddeldeCadans < 75)))
            {
                Console.WriteLine($" {x.DatumUur} {x.Tijdsduur} {x.GemiddeldeWattage} {x.MaximumWattage} {x.GemiddeldeCadans}{x.MaximumCadans} {x.Trainingstype} {x.Commentaar}  {x.Klantnummer}");

            }
        }
        public static void TopKlanten (List<Trainingdata> trainingDataList)
        {
            var groupedData = trainingDataList
            .Where(data => data.DatumUur.DayOfWeek == DayOfWeek.Monday)
            .GroupBy(data => data.Klantnummer)
            .Select(group => (Klantnummer: group.Key, Count: group.Count()))
            .OrderByDescending(group => group.Count)
            .Take(3);

            Console.WriteLine("Top drie klanten met de meeste trainingen op maandag:");
            foreach (var customer in groupedData)
            {
                Console.WriteLine($"Klantnummer: {customer.Klantnummer}, Aantal trainingen: {customer.Count}");
            }
        }
        public static void HevigeSessiesweergeven(List<Trainingdata> trainingDataList)
        {
            int hardesessies = trainingDataList
                .Count(data => data.Trainingstype == "interval" &&
                               data.Tijdsduur > 60 &&
                               data.MaximumWattage > 300);

            int hevigesessies = trainingDataList
                .Count(data => data.Trainingstype == "endurance" &&
                               data.Tijdsduur > 100 &&
                               data.GemiddeldeWattage > 200);

            int kortmaarhevig = trainingDataList
                .Count(data => data.Trainingstype == "interval" &&
                               data.Tijdsduur < 45 &&
                               data.GemiddeldeWattage > 250);

            Console.WriteLine("Aantal trainingsessies voor verschillende zware trainingen:");
            Console.WriteLine($"Hard training sessions: {hardesessies}");
            Console.WriteLine($"Heavy training sessions: {hevigesessies}");
            Console.WriteLine($"Short but heavy training sessions: {kortmaarhevig}");
        }
        public static void Trainingstatistiekenperklant(List<Trainingdata> trainingDataList, int klantnummer)
        {
            // Filter de klant met de juiste nummer
            var klantTrainingData = trainingDataList
                .Where(data => data.Klantnummer == klantnummer);

            // aantal treiningstijd
            int totalTrainingTime = klantTrainingData.Sum(data => data.Tijdsduur);

            // langste en kortste trainingsteid
            int shortestTrainingTime = klantTrainingData.Min(data => data.Tijdsduur);
            int longestTrainingTime = klantTrainingData.Max(data => data.Tijdsduur);

            // gemiddelde 
            int averageTrainingTime = (int)klantTrainingData.Average(data => data.Tijdsduur);
            int numberOfSessions = klantTrainingData.Count();

            Console.WriteLine($"Voor klantnummer {klantnummer}:");
            Console.WriteLine($"Totale trainingstijd: {totalTrainingTime} minuten");
            Console.WriteLine($"Kortste trainingsduur: {shortestTrainingTime} minuten");
            Console.WriteLine($"Langste trainingsduur: {longestTrainingTime} minuten");
            Console.WriteLine($"Gemiddelde trainingsduur: {averageTrainingTime} minuten");
            Console.WriteLine($"Aantal sessies: {numberOfSessions}");
        }
        public static void Weergeefklantmetcriteria(List<Trainingdata> trainingDataList)
        {
            
            var filteredData = trainingDataList
                .Where(data => data.GemiddeldeWattage > 350 &&
                               data.Tijdsduur > 100 &&
                               Math.Abs(data.MaximumCadans - data.GemiddeldeCadans) < 10)
                .OrderByDescending(data => data.GemiddeldeCadans)
                .ThenBy(data => data.Tijdsduur)
                .ThenByDescending(data => data.GemiddeldeWattage);

            if (filteredData.Any())
            {
                var klant = filteredData.First();
                Console.WriteLine("De klant die voldoet aan de criteria");
                Console.WriteLine($"Klantnummer: {klant.Klantnummer}, Gemiddelde cadans: {klant.GemiddeldeCadans}, Tijdsduur: {klant.Tijdsduur}, Gemiddelde wattage: {klant.GemiddeldeWattage}");

            }
            else
            {
                Console.WriteLine("Er is geen klant die voldoet aan de criteria.");
            }
        }

    }
}
    

