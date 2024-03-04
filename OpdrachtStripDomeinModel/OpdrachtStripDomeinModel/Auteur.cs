using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpdrachtStripDomeinModel
{
    internal class Auteur
    {
        // via  setters zullen de naam en de email verkregen worden
        public Auteur(string naam, string email)
        {
            Naam = naam;
            Email = email;
        }

        public string Naam { get; set; }
        public string Email { get; set; }
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
