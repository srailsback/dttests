using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dttests.Models
{
    public class SegmentEdit
    {
        private dynamic x;

        // SR - need segment key
        public Guid SegmentKey { get; set; }
        public int ChangeId { get; set; }
        public string ChangeType { get; set; }
        public string FIPS { get; set; }
        public string ROUTE { get; set; }
        public int SEGMID { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
        public SegmentEdit() { }

        public SegmentEdit(dynamic x)
        {
            this.ChangeId = (int)x.ChangeId;
            this.SegmentKey = x.SegmmentKey;
            this.ChangeType = (string)x.ChangeType;
            this.FIPS = (string)x.FIPS;
            this.ROUTE = (string)x.ROUTE;
            this.FieldName = x.FieldName;
            this.SEGMID = x.SEGMID;
            this.OldValue = x.OldValue;
            this.NewValue = x.NewValue;
            this.Timestamp = x.Timestamp;
            this.UserName = x.UserName;
        }

    }

    public class SegmentEditAnnex : SegmentEdit
    {
        public Guid? OldSegmentKey { get; set; }
    }
}