using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OpdrachtStripDomeinModel
{
    internal class Uitgeverij
    {
        public Uitgeverij(string naam, string adress)
        {
            Naam = naam;
            Adress = adress;
        }

        public string Naam { get; set; }
        public string Adress { get; set; }

        public List<int> randomList = new List<int>();
        public int Id()
        {
            int num = 1;
            do
            {
                Random rnd = new Random();
                num = rnd.Next(0, 10000);
                if (!randomList.Contains(num))
                {
                    randomList.Add(num);
                    
                }
            }
            while (!randomList.Contains(num));
            return num;
        }
    }
}
