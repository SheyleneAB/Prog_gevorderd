using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_BL.Exceptions;

namespace TC_BL.Model
{
    public class Offerte
    {
        private Dictionary<Product, int> producten = new Dictionary<Product, int>();
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        private Klant klant;
        public Offerte() { }
        public Offerte(DateTime datum, Klant klant, bool afhalenBool, bool plaatsenBool)
        {
            Datum = datum;
            Klant = klant;
            AfhalenBool = afhalenBool;
            PlaatsenBool = plaatsenBool;
        }

        public Klant Klant { get { return klant; } set {
                
                    if (value == null) throw new DomeinException("klant is null");
                    klant = value;
                
        } }
        public IReadOnlyDictionary< Product, int> Producten {
            get { return producten; }
            set
            {
                if (value == null || value.Count < 1) throw new DomeinException("offerte-setproducten");
                producten = (Dictionary<Product, int>)value;
            }
        }
        public bool AfhalenBool { get; set; }
        public bool PlaatsenBool { get; set; }
        public void VoegProductToe(Product product, int aantal)
        {
            if ((producten.Keys.Contains(product)) || (product == null)) throw new DomeinException("offerte-voegproducten");
            producten.Add(product, aantal );
        }
        public void VerwijderProduct(Product product)
        {
            if ((producten.Count == 1) || (!producten.Keys.Contains(product)) || (product == null)) throw new DomeinException("strip-verwijderauteur");
            producten.Remove(product);
        }
        public double prijsberekenen()
        {
            double prijs = 0;
            foreach (Product product in Producten.Keys)
            {
                prijs = prijs + (product.Prijs * Producten[product]);
            }
            if (prijs> 5000)
            {
                prijs = prijs * 0.90;
            }
            if (prijs > 2000 && prijs<5000)
            {
                prijs = prijs * 0.95;
            }
            if (AfhalenBool is false)
            {
                if (prijs < 500)
                {
                    prijs = prijs + 100;
                }
                if (prijs < 1000 && prijs > 500)
                {
                    prijs = prijs + 50;
                }
            }
            if (PlaatsenBool is true)
            {
                if (prijs < 2000)
                {
                    prijs = prijs * 1.15;
                }
                if (prijs > 2000 && prijs<5000)
                {
                    prijs = prijs * 1.10;
                }
                else
                {
                    prijs = prijs * 1.05;
                }
            }
            return prijs;
        }
    }
}
