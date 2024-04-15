using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainingsdata.bl.model
{
    public class Trainingdata
    {
        public DateTime DatumUur { get; set; }
        public int Tijdsduur { get; set; }
        public int GemiddeldeWattage { get; set; }
        public int MaximumWattage { get; set; }
        public int GemiddeldeCadans { get; set; }
        public int MaximumCadans { get; set; }
        public string Trainingstype { get; set; }
        public string Commentaar { get; set; }
        public int Klantnummer { get; set; }


        public Trainingdata
            (DateTime datumUur, int tijdsduur, int gemiddeldeWattage, int maximumWattage,
                            int gemiddeldeCadans, int maximumCadans, string trainingstype, string commentaar,
                            int klantnummer)
        {
            DatumUur = datumUur;
            Tijdsduur = tijdsduur;
            GemiddeldeWattage = gemiddeldeWattage;
            MaximumWattage = maximumWattage;
            GemiddeldeCadans = gemiddeldeCadans;
            MaximumCadans = maximumCadans;
            Trainingstype = trainingstype;
            Commentaar = commentaar;
            Klantnummer = klantnummer;
        }
    }
}

