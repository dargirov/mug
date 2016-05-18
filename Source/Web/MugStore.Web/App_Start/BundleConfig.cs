using System;
using System.Web;
using System.Web.Optimization;

namespace MugStore.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);
            

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/css/main.css", "~/Content/css/touchcarousel.css"));
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/babylon").Include("~/Scripts/libs/babylon.2.3.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/libs/jquery-2.2.3.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/touchcarousel").Include("~/Scripts/libs/touchcarousel-1.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/Scripts/scene.js", "~/Scripts/mug.js", "~/Scripts/cart.js"));
            bundles.Add(new ScriptBundle("~/bundles/main").Include("~/Scripts/main.js"));
        }
    }
}
