using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oefening2LinqAdressen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\elyne\\Downloads\\adresInfo\\adresInfo.txt";

            Filereader fileReader = new Filereader(filePath);
            Provincie provincie = fileReader.ReadDataFromFile();

            fileReader.GetSortedProvincieNamen();
            fileReader.PrintStraatnamenForGemeente("Pelt");
            fileReader.PrintMeesttFrequenteStraatnamen();
            fileReader.PrintMostFrequentStraatnamen(5);
            fileReader.PrintGemeenschapStraatnamentssnGemeentes("Pelt", "Riemst");
            fileReader.PrintStraatnamenEnkelnGemeente("Pelt");
            fileReader.PrintGemeenteMetMeesteStraatnamen();
            fileReader.PrintLangsteStraatnaam();
            fileReader.PrintLangsteStraatnaamMetLocatie();
            fileReader.PrintUniekeStraatnamenMetLocatie();
            fileReader.PrintUniekeStraatnamenVoorGemeente("Pelt");

                Console.ReadLine();
        }
    }
}
