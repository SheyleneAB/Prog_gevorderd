using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fitness_oefening.Program;

namespace Fitness_oefening
{
    public class Sessie
    {
        private int klantnr;
        private int sessienr;
        private double gemsnelheid;
        private int tijdsduurmin;
        public Sessie(int sessienr, DateTime datum, double gemsnelheid, int tijdsduurmin, HashSet<Interval> intervalsessie, int klantnr)
        {
            Sessienr = sessienr;
            Datum = datum;
            Gemsnelheid = gemsnelheid;
            Tijdsduurmin = tijdsduurmin;
            Intervalsessie = intervalsessie;
            Klantnr = klantnr;
        }
        public int Klantnr
        {
            get => klantnr;
            set
            {
                if (value > 0)
                    klantnr = value;
                else
                    throw new ArgumentException("Klantnr is fout");
            }
        }
        public int Sessienr
        {
            get => sessienr;
            set
            {
                if (value > 0)
                {
                    sessienr = value;
                }
                else
                {
                    throw new ArgumentException("sessienr is fout");
                }
            }
        }
        public DateTime Datum { get; set; }
        public double Gemsnelheid
        {
            get => gemsnelheid;
            set
            {
                if (value > 5 && value <= 22)
                {
                    gemsnelheid = value;
                }
                else
                {
                    throw new ArgumentException("gemsnelheid is fout");
                }
            }
        }
        public int Tijdsduurmin { get => tijdsduurmin; 
            set
            {
                if (value > 5 && value <= 22)
                {
                    tijdsduurmin = value;
                }
                else
                {
                    throw new ArgumentException("tijdsduurmin is fout");
                }
            }
        }
        public HashSet<Interval> Intervalsessie;
    }
}
