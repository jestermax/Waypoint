using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

using Domain.Configuration;

namespace Presentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            if (!WebSecurity.Initialized)
            {
                //WebSecurity.InitializeDatabaseConnection(
                //    AppConfiguration.ConnectionStringName,
                //    AppConfiguration.UserTableName,
                //    AppConfiguration.UserIdColumn,
                //    AppConfiguration.UserNameColumn,
                //    false);
            }

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
