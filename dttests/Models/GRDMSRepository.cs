using System.Linq;
using System.Web;

namespace dttests.Models
{
    public interface IGRDMSRepository
    {
        IQueryable<Segment> All(string fips = "");
    }

    public class GRDMSRepository : IGRDMSRepository
    {
        private string cacheKey = "grdms_segments_store";

        public GRDMSRepository()
        {
            this.Cache();
        }

        private IQueryable<Segment> Cache()
        {
            var _table = new HUTFSegment();
            return HttpRuntime
                .Cache
                .GetOrStore<IQueryable<Segment>>
                (
                    cacheKey,
                    () => _table.All(columns: "*").Select(x => new Segment(x)).ToList().AsQueryable()
                );
        }

        public IQueryable<Segment> All(string fips = "")
        {
            if (!string.IsNullOrWhiteSpace(fips))
            {
                return Cache().Where(x => x.FIPS == fips);
            }
            return Cache();
        }
    }
}