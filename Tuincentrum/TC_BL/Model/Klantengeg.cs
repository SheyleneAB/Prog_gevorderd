using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_BL.Model
{
    public class Klantengeg
    {
        public Klantengeg(int id, string naam, string adres, int aantaloffertes, List<Offerte> offertenummber, double totaalPrijs)
        {
            Id = id;
            Naam = naam;
            Adres = adres;
            Aantaloffertes = aantaloffertes;
            Offertenummber = offertenummber;
            TotaalPrijs = totaalPrijs;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        public int Aantaloffertes { get; set; }
        public List<Offerte> Offertenummber { get; set; }
        public double TotaalPrijs { get; set; }
    }
}
