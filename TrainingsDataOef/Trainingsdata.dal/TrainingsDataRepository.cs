using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingsdata.bl.model;
using System.IO;
using Trainingsdata.bl.managers;

namespace Trainingsdata.dal
{
    public class TrainingsDataRepository : IFileProcessor
    
    {
        string filePath = "C:\\Users\\elyne\\Downloads\\CyclingData.txt";
        public List<Trainingdata> GetAllTrainingData(string filePath)
        {

            List<Trainingdata> trainingDataList = new List<Trainingdata>();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length == 9)
                {
                    DateTime datumUur = DateTime.Parse(parts[0].Trim('\''));
                    int tijdsduur = int.Parse(parts[1]);
                    int gemiddeldeWattage = int.Parse(parts[2]);
                    int maximumWattage = int.Parse(parts[3]);
                    int gemiddeldeCadans = int.Parse(parts[4]);
                    int maximumCadans = int.Parse(parts[5]);
                    string trainingstype = parts[6].Trim('\'');
                    string commentaar = parts[7].Trim('\'');
                    int klantnummer = int.Parse(parts[8]);

                    Trainingdata trainingData = new Trainingdata(datumUur, tijdsduur, gemiddeldeWattage, maximumWattage,
                                                                 gemiddeldeCadans, maximumCadans, trainingstype, commentaar,
                                                                 klantnummer);
                    trainingDataList.Add(trainingData);
                }
                else
                {
                    Console.WriteLine($"Failed to parse line: {line}");
                }
            }

            return trainingDataList;
        }

    }
}
