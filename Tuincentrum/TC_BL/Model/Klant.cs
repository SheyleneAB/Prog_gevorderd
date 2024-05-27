using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Naam { get; set; }
        public int Id { get; set; }
        public string Adres { get; set; }
    }
}
