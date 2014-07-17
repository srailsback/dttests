using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dttests.Models
{
    public interface ISegmentEditsRepository
    {
        IQueryable<SegmentEdit> All(string fips = "");
    }

    public class SegmentEditsRepository : ISegmentEditsRepository
    {
        private string cacheKey = "segment_edits_store";

        public SegmentEditsRepository()
        {
            this.Cache();

        }

        private IQueryable<SegmentEdit> Cache()
        {
            var _table = new HUTFSegmentEdit();
            return HttpRuntime
                .Cache
                .GetOrStore<IQueryable<SegmentEdit>>
                (
                    cacheKey,
                    () => _table.All(columns: "*").Select(x => new SegmentEdit(x)).ToList().AsQueryable()
                );
        }

        public IQueryable<SegmentEdit> All(string fips = "")
        {
            if (!string.IsNullOrWhiteSpace(fips))
            {
                return this.Cache().Where(x => x.FIPS == fips);
            }
            return this.Cache();
        }
    }


}