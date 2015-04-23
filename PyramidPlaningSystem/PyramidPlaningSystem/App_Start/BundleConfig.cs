using System.Web;
using System.Web.Optimization;

namespace PyramidPlaningSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/angular-material/angular-material.css",
                      "~/Content/site.css",
                      "~/Content/ExternalLoginProvider.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                   "~/Scripts/angular.js",
                   "~/Scripts/angular-material.js",
                   "~/Scripts/angular-animate.js",
                   "~/Scripts/angular-aria.js",
                   "~/Scripts/angular-route.js",
                   "~/Scripts/angular-messages.js",
                   "~/Scripts/angular-ui/ui-bootstrap.js",
                   "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                   "~/Scripts/angular-tagger.js",
                   "~/Angular/Shared/app.js",
                //"~/Angular/Controller/mainLayoutController.js",
                //"~/Angular/Controller/addToDoController.js",
                //"~/Angular/Controller/listAllToDosController.js",
                //"~/Angular/Controller/editToDoController.js",
                   "~/Angular/ColorTheming/colorTheming.js",
                   "~/Angular/Route/Route.js",
                   "~/Angular/Services/ToDoFactory.js"
                   ));

            //bundles.Add(new ScriptBundle("~/bundles/custom").Include(
            //       "~/Angular/Controller/app.js").Include(
            //       "~/Angular/Controller/mainLayoutController.js").Include(
            //       "~/Angular/ColorTheming/colorTheming.js").Include(
            //       "~/Angular/Route/Route.js").Include(
            //       "~/Angular/Services/ToDoFactory.js"));


            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
