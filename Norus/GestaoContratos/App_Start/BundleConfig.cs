using System.Web;
using System.Web.Optimization;

namespace GestaoContratos
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            /////////////////////////////////////////////////////////////
            
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/node_modules/bootstrap/dist/css/bootstrap.min.css",
                      "~/node_modules/pnotify/dist/PNotifyBrightTheme.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/node_modules/jquery/dist/jquery.min.js",
                "~/node_modules/jquery-mask-plugin/dist/jquery.mask.min.js",
                "~/node_modules/popper.js/dist/umd/popper.min.js",
                "~/node_modules/bootstrap/dist/js/bootstrap.min.js",
                "~/node_modules/pnotify/dist/iife/Pnotify.js",
                "~/node_modules/pnotify/dist/iife/PnotifyButtons.js",
                "~/node_modules/pnotify/dist/iife/PnotifyAnimate.js",
                "~/node_modules/pnotify/dist/iife/PnotifyMobile.js",
                "~/node_modules/moment/min/moment-with-locales.js",
                "~/node_modules/bs-custom-file-input/dist/bs-custom-file-input.js",
                "~/Scripts/site/funcoes.js"));

            bundles.Add(new ScriptBundle("~/bundles/contratoCadastro").Include(
                "~/Scripts/site/contratoCadastro.js"));


            bundles.Add(new ScriptBundle("~/bundles/contratoLista").Include(
                "~/Scripts/site/contratoLista.js",
                "~/Scripts/lib/twbs-pagination/jquery.twbsPagination.js"));
        }
    }
}
