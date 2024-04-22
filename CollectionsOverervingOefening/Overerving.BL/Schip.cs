using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overerving.BL
{
    public class Schip 
    {
        private double lengte;
        private double breedte;
        private double tonnage;
        private string naam;
        public double Lengte { get { return lengte; }
            set
            {if (value< 0) throw new Exception("lengteset"); lengte= value; }
        }
        public double Breedte
        {
            get { return breedte; }
            set { if (value < 0) throw new Exception("breedteset"); breedte = value; }
        }
        public double Tonnage { get { return tonnage; }
            set { if (value < 0) throw new Exception("tonnageset"); tonnage = value; } 
        }
        public string Naam
        {
            get { return naam; }
            set { if (value.IsNullOrEmpty()) throw new Exception("naamset"); naam = value; }
        }
        public int? Id;

        public Schip(double lengte, double breedte, double tonnage, string naam)
        {
            Lengte = lengte;
            Breedte = breedte;
            Tonnage = tonnage;
            Naam = naam;
        }

        public override bool Equals(object obj)
        {
            if (obj is Schip)
            {
                Schip compSchip = (Schip)obj;
                if (Id.HasValue && compSchip.Id.HasValue)
                {
                    if (Id == compSchip.Id) return true; else return false;
                }
                else
                {
                    return Lengte == compSchip.Lengte &&
                           Breedte == compSchip.Breedte &&
                           Tonnage == compSchip.Tonnage &&
                           Naam == compSchip.Naam;
                }
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Lengte, Breedte, Tonnage, Naam);
        }

    }
}
