using System;
using System.Collections.Generic;

namespace OpdrachtStripDomeinModel
{
    public class Reeks
    {
        public Reeks(int reeksnummer)
        {
            Reeksnummer = reeksnummer;
        }
        public string Naam { get; set; }
        public int Reeksnummer { get; set; }
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