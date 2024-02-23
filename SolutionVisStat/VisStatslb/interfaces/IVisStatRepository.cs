using VisStatsBL.MODEL;

namespace VisStatsBL.interfaces
{
    public interface IVisStatRepository {
        bool HeeftVissoort(Vissoort vissoort);
        void SchrijfVissoort(Vissoort vissoort);
        public void SchrijfHaven(Haven haven);
        public bool HeeftHaven(Haven haven);
    }
}
