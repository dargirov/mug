namespace MugStore.Web
{
    using System.Data.Entity;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using MugStore.Data;
    using MugStore.Data.Migrations;
    using MugStore.Web.App_Start;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            AutofacConfig.RegisterAutofac();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
