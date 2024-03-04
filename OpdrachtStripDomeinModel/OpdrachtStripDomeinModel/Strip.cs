using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpdrachtStripDomeinModel
{
    internal class Strip
    {
        private string _titel;
        // De titel van een strip mag niet leeg zijn. -> Bij het instellen van de titel zal er een private variabele aangemaakt
        // worden, als deze variabele een spatie of een null waarde is, dan zal er een nieuwe domeinexception
        // gegeven worden dat zegt dat dit niet mag

        private HashSet<Auteur> auteurstrip;
        public Strip(string titel, HashSet<Auteur> auteurStrip, Uitgeverij uitgeverijStrip, Reeks reeksStrip)
        {
            Titel = titel;
            AuteurStrip = auteurStrip;
            UitgeverijStrip = uitgeverijStrip;
            ReeksStrip = reeksStrip;
        }

        public Strip(  string titel, HashSet<Auteur> auteurStrip, Uitgeverij uitgeverijStrip)
        {
            Titel = titel;
            AuteurStrip = auteurStrip;
            UitgeverijStrip = uitgeverijStrip;
           
        }


        public string Titel { get { return _titel; } 
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomeinException("De titel is null");
                _titel = value;
            } 
        }
               // de auteur hashset mag niet leeg zijn, dus er zal een domeinexception gegeven worden als de hashset leeg is
               // auteurs kunnen toegevoegd of verwijderd worden in de hashset. als er teveel auteurs verwijderd worden zal er 
               //een exception gethrowd worden.
        public HashSet <Auteur> AuteurStrip { get { return auteurstrip; }
            set 
            {
                if(value.Count <= 0 )
                {
                    throw new DomeinException("Er is geen Auteur");
                }
                auteurstrip = value;
            }  
        }
        public Uitgeverij UitgeverijStrip { get; set; }
        public Reeks ReeksStrip {get; set; }
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
