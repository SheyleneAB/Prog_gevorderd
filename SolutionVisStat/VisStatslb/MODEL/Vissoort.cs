using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisStatslb.Exceptions;

namespace VisStatslb.MODEL
{
    public class Vissoort
    {
        public Vissoort( string naam)
        {
           Naam = naam;
        }
        public Vissoort (int? id, string naam)
        {
            Id = id;
            Naam = naam;
        }
        public int? Id;
        public string Naam
        {
            get { return Naam; }
            set { if (!string.IsNullOrWhiteSpace(value))
                    throw new DomeinException("Vissoort_naam");
                Naam = value; }
        }
    }
}
