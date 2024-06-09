using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_BL.Model;

namespace TC_BL.Interfaces
{
    public interface ITCRepository
    {
        public void SchrijfKlant(Klant klant);
        public bool HeeftKlant(Klant klant);
        bool HeeftProduct(Product product);
        void SchrijfProduct(Product product);
        public Dictionary<int, Klant> LeesAlleKlanten();
        bool HeeftOfferte(Offerte offerte);
        void SchrijfOfferte(Offerte offerte);
        public Dictionary<int, Product> LeesAlleProducten();
        Klantengeg LeesKlantengegnaam(string klantnaam);
        Klantengeg LeesKlantengegid(int klantid);
        public List<Offerte> HaalOfferteOp(int? offerteId, Klant? klant, DateTime? datum);
        public void UpdateOfferte(Offerte offerte, Dictionary<Product, int> proddel, Dictionary<Product, int> produpdate, Dictionary<Product, int> prodnew);
    }
}
