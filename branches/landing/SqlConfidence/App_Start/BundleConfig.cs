using System.Web;
using System.Web.Optimization;

namespace SqlConfidence
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/lib/jquery-{version}.js",
                        "~/Scripts/site.js",
                        "~/Scripts/lib/ckeditor/ckeditor.js",
                        "~/Scripts/lib/codemirror.js",
                        "~/Scripts/lib/sql.js",
                        "~/Scripts/lib/sql-hint.js",
                        "~/Scripts/lib/show-hint.js",
                        "~/Scripts/lib/foundation.js"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                    "~/Content/foundation.css",
                    "~/Content/codemirror.css",
                    "~/Content/show-hint.css"));
        }
    }
}