using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_BL.Exceptions;

namespace TC_BL.Model
{
    public class Klant
    {
        public Klant() { }
        public Klant( int id, string naam, string adres)
        {
            Naam = naam;
            Id = id;
            Adres = adres;
        }
        private string naam;
        private int id;
        private string adres;
        public string Naam { get { return naam; }
        set
        {
                if (string.IsNullOrEmpty(value)) throw new DomeinException("Klantnaam is null"); naam = value;
        }
        }
        public int Id { get { return id; } 
            set 
            {
                if (value < 0) throw new DomeinException("SetKlantid"); id = value;
            } 
        }
        public string Adres { get { return adres; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new DomeinException("Klantadres is null"); adres = value;
            }
        }
    }
}
