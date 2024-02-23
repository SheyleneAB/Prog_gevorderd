using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisStatsBL.Exceptions;

namespace VisStatsBL.MODEL
{
    public class Haven
    {
        private string naam;

        public Haven ( string naam)
        {
            Naam = naam;
        }
        public Haven (int? id, string naam)
        {
            Id = id;
            Naam = naam;
        }
        public int? Id;
        public string Naam
        {
            get { return naam; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomeinException("Vissoort_naam");
                naam = value;
            }
        }

    }
}
