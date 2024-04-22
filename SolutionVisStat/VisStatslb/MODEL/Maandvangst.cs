using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisStatsBL.MODEL
{
    public class Maandvangst
    {
        public int Jaar;
        public int Maand;
        public double Totaal;


        public Maandvangst(int jaar, int maand, double totaal)
        {
            Jaar = jaar;
            Maand = maand;
            Totaal = totaal;
        }
    }
}
