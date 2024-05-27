using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_BL.Model
{
    public class Offerte
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public Klant Klant { get; set; }
        public Dictionary< Product, int> Producten { get; set; }
        public bool AfhalenBool { get; set; }
        public bool PlaatsenBool { get; set; }
    }
}
