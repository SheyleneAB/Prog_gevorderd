using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static double GetClamped(double min, double getal, double max)
        {
            double result = min;
            if (getal > min)
            {
                if (getal > max)
                {
                    result = max;
                }
                else
                {
                    result = getal;
                }
            }
            return result;
        }

        public static void Main(string[] args)
        {
            double[] getallen = { -1, 0, 3.6, 6, 100 };
            for (int i = 0; i < getallen.Length; i++)
            {
                double clamped = GetClamped(3, getallen[i], 6.4);
                Console.WriteLine($"voor {i} geeft dit {clamped}");
            }
            Console.ReadLine();
        }
    }
}
