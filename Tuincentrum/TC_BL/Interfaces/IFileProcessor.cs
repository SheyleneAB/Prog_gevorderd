using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_BL.Model;

namespace TC_BL.Interfaces
{
    public interface IFileProcessor
    {
        public List<Klant> LeesKlanten(string fileName);
        public List<Product> LeesProducten(string fileName);
        public Dictionary<int, Offerte> LeesOffertes(string fileName, Dictionary<int, Klant> klanten);
    }
}
