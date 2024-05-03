using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tc_BUL.Model
{
    internal class Product
    {
        public int Id { get; set; }
        public string Nednaam { get; set; }
        public string Wetnaam { get; set;}
        public string Beschrijving { get; set; }
        public double Prijs {  get; set; }
    }
}
