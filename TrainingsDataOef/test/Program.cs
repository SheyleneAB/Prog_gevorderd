using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingsdata.bl.model;
using Trainingsdata.dal;

namespace Test 
{ 
    class Program
    {
        static void Main(string[] args)
        {
           
            TrainingsDataRepository dataRepository = new TrainingsDataRepository();
            List<Trainingdata> trainingDataList = dataRepository.GetAllTrainingData(filePath);

            // Displaying the training data
            foreach (var trainingData in trainingDataList)
            {
                DisplayTrainingData(trainingData);
            }
        }

        public static void DisplayTrainingData(Trainingdata trainingData)
        {
            Console.WriteLine($"Datum + uur: {trainingData.DatumUur}");
            Console.WriteLine($"Tijdsduur: {trainingData.Tijdsduur} minuten");
            Console.WriteLine($"Gemiddelde wattage: {trainingData.GemiddeldeWattage}");
            Console.WriteLine($"Maximum wattage: {trainingData.MaximumWattage}");
            Console.WriteLine($"Gemiddelde cadans: {trainingData.GemiddeldeCadans}");
            Console.WriteLine($"Maximum cadans: {trainingData.MaximumCadans}");
            Console.WriteLine($"Trainingstype: {trainingData.Trainingstype}");
            Console.WriteLine($"Commentaar: {trainingData.Commentaar}");
            Console.WriteLine($"Klantnummer: {trainingData.Klantnummer}");
            Console.WriteLine();
        }
    }
}