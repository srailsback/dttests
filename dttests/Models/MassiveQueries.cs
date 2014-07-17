
namespace dttests.Models
{
    public static class MassiveHelper
    {
        public static string connectionString = "ConnectionString";
        public static string GetTableName(string appKey)
        {
            return System.Configuration.ConfigurationManager.AppSettings[appKey];
        }
    }

    internal class HUTFSegment : DynamicModel
    {
        public HUTFSegment() : base(MassiveHelper.connectionString, MassiveHelper.GetTableName("GRDMS_CICOOFF_TABLE"), "SegmentKey") { }
    }

    internal class HUTFSegmentEdit : DynamicModel
    {
        public HUTFSegmentEdit() : base(MassiveHelper.connectionString, MassiveHelper.GetTableName("Segment_Edits_Table"), "ChangeId") { }
    }
}