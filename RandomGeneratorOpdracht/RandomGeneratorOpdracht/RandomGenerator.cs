using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGeneratorOpdracht
{
    internal class RandomGenerator : IGenerator
    {
        private static RandomGenerator instance = null; 
        private Random random;

        private const int Min = 1;
        private const int Max = 15;
        private List<int> Uniekewaarden; 
        private bool extrawaarden; // Flag to track if extra values have been generated

        // Private constructor to prevent external instantiation
        private RandomGenerator()
        {
            random = new Random();
            Uniekewaarden = GenereerUniekeWaarden();
            extrawaarden = false;
        }

        // Static property to access the singleton instance
        public static RandomGenerator Instance
        {
            get
            {
                // Lazy initialization
                if (instance == null)
                {
                    instance = new RandomGenerator();
                }
                return instance;
            }
        }

        public List<int> GeefUniekeGetal(int aantal)
        {
            List<int> verkregenwaarden = new List<int>();

            
            if (aantal > Uniekewaarden.Count)
            {
                if (!extrawaarden)
                {
                    
                    while (Uniekewaarden.Count < aantal)
                    {
                        Uniekewaarden.Add(0);
                    }
                    extrawaarden = true;
                }
                else
                {
                    for (int i = 0; i < aantal; i++)
                    {
                        verkregenwaarden.Add(0);
                    }
                    return verkregenwaarden;
                }
            }

            
            for (int i = 0; i < aantal; i++)
            {
                verkregenwaarden.Add(Uniekewaarden[i]);
            }

            return verkregenwaarden;
        }

        private List<int> GenereerUniekeWaarden()
        {
            List<int> waarden = new List<int>();
            for (int i = Min; i <= Max; i++)
            {
                waarden.Add(i);
            }
            return waarden;
        }
    }
}
