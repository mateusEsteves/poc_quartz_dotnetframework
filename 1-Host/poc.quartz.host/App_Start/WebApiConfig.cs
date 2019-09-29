using poc.quartz.jobs.scheduler;
using System.Web.Http;

namespace poc.quartz.host
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            new InicializadorScheduler().InicializarScheduler().GetAwaiter().GetResult();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
