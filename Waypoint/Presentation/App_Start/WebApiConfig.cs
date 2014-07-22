using System.Web.Http;

using Domain.Configuration;

namespace Presentation
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(AppConfiguration.ApiRouteName, "api/{controller}/{id}", new
            {
                id = RouteParameter.Optional
            });
        }
    }
}
