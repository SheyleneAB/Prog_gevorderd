using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tc_BUL.Model
{
    internal class Offerte
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int Klantid { get; set; }
        public Dictionary<int, Product> Producten { get; set; }
        public bool AfhalenBool { get; set; }
        public bool PlaatsenBool { get; set; }

    }
}
