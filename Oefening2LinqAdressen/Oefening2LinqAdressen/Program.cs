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
            string gemeentenaam = "Schilde";

            fileReader.GetSortedProvincieNamen();
            fileReader.PrintStraatnamenForGemeente(gemeentenaam);

            Console.ReadLine();
        }
    }
}
