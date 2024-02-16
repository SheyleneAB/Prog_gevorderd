using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fitness_oefening.Program;

namespace Fitness_oefening
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string inputFile = "insertRunningTest.txt";
                string outputFile = "insertRunning.sql";

                using (StreamReader sr = new StreamReader(inputFile))
                using (StreamWriter sw = new StreamWriter(outputFile)) 
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Replace("insert into runningsession values", "").Trim();
                        string[] values = line.Replace("(", "").Replace(")", "").Split(',');
                    }
                }

                Console.WriteLine("SQL statements appended to the existing SQL file successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout: {ex.Message}");
            }
            Console.ReadLine();
        }
    }
        
    
}
