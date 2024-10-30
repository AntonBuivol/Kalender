using System.Web;
using System.Web.Optimization;

namespace EventManagerMvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.6.0.js"));

            // Подключаем Moment.js
            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment.js"));

            // Подключаем FullCalendar
            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                        "~/Scripts/fullcalendar/fullcalendar.js"));

            // Подключаем CSS FullCalendar
            bundles.Add(new StyleBundle("~/Content/fullcalendar").Include(
                        "~/Content/fullcalendar.css"));

            // Существующий код, например Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
