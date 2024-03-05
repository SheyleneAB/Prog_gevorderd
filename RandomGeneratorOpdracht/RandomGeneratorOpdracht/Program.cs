using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGeneratorOpdracht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RandomGenerator randomGenerator = RandomGenerator.Instance;
            List<int> randomGetallen = randomGenerator.GeefUniekeGetal(20);

            Console.WriteLine("Gegenereerde waardes");
            foreach (int getal in randomGetallen)
            {
                Console.WriteLine(getal);
            }
            Console.ReadLine();
        }
    }
}
