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
        public Klant Klant { get; set; }
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

    }
}
