using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class Vloot
    {
        public string naam;
        public List<Schip> Schepen;

        public Vloot(string naam, List<Schip> schepen)
        {
            this.naam = naam;
            Schepen = schepen;
        }

        public void VoegSchipToe(Schip schip)
        {
            if (schip != null || !Schepen.Contains(schip))
            {
                Schepen.Add(schip);
            }
        }

        public void VerwijderSchip(Schip schip)
        {
            if (Schepen.Count != 1 || schip != null)
            {
                Schepen.Remove(schip);
            }
            else throw new Exception("verwijderschip");
        }
        public override bool Equals(object obj)
        {
            if (obj is Vloot)
            {
                Vloot compVloot = (Vloot)obj;



                return Schepen == compVloot.Schepen &&
                       naam == compVloot.naam;
                
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Schepen, naam);
        }
    }
}
