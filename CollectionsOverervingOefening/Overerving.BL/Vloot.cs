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
        public void VoegSchipToe(Schip schip)
        {
            if (!Schepen.Contains(schip))
            {
                Schepen.Add(schip);
            }
        }

        public void VerwijderSchip(Schip schip)
        {
            if (Schepen.Count != 1)
            {
                Schepen.Remove(schip);
            }
            else throw new Exception("verwijderschip");
        }
        /* aanpassen van onlyreadlist naar private bij schepen */
    }
}
