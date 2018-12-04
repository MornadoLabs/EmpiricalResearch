using System.Web;
using System.Web.Optimization;

namespace Lab7
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Vendors/JQuery/Scripts/jquery-{version}.js",
                        "~/Vendors/JQuery/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Vendors/Bootstrap/Scripts/bootstrap.js",
                      "~/Vendors/Bootstrap/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                      "~/Vendors/chart.js/Scripts/Chart.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                      "~/Scripts/Custom/main.js"));

            // Styles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Vendors/Bootstrap/Styles/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/custom.css"));
        }
    }
}
