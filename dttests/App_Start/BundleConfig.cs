using System.Web;
using System.Web.Optimization;

namespace dttests
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/content/datatables").Include(
                "~/Scripts/Vendor/DataTables-1.10.0/media/css/jquery.dataTables.css"
            ));


            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/Vendor/DataTables-1.10.0/media/js/jquery.dataTables.js",
                "~/Scripts/Vendor/ICanHaz/ICanHaz.js"      
            ));

        }
    }
}