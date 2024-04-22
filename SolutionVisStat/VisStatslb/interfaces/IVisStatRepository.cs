using VisStatsBL.MODEL;

namespace VisStatsBL.interfaces
{
    public interface IVisStatRepository {
        bool HeeftVissoort(Vissoort vissoort);
        void SchrijfVissoort(Vissoort vissoort);
        public void SchrijfHaven(Haven haven);
        public bool HeeftHaven(Haven haven);
        bool IsOpgeladen(string fileName);
        void SchrijfStatiestieken(List<VisStatsDataRecord> data, string fileName);
        List<Haven> LeesHavens();
        List<Vissoort> LeesVissoorten();
        List<int> LeesJaartallen();
        List<Jaarvangst> LeesStatistieken(int jaar, Haven haven, List<Vissoort> vissoorts, Eenheid eenheid);
        List<Maandvangst> LeesMaandStatistieken(List<int> jaar, List<Haven> haven, Vissoort vissoort, Eenheid eenheid);
    }
}
