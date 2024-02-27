using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_oefening
{
    public class Interval
    {   
        public Interval(int sequentienr, double loopsnelheid, int tijdsduursec)
        {
            Sequentienr = sequentienr;
            Loopsnelheid = loopsnelheid;
            Tijdsduursec = tijdsduursec;
        }
        public int Sequentienr { get; set; }
        public double Loopsnelheid { get; set; }
        public int Tijdsduursec { get; set; }
    }
}
