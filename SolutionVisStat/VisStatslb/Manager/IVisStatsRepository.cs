using VisStatslb.MODEL;

namespace VisStatslb.Manager
{
    public interface IVisStatsRepository
    {
        bool HeeftVissoort(Vissoort vissoort);
        void schrijfVissoort(Vissoort vissoort);
    }
}