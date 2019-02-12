using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Tracing;
using UserViewer.Handlers;

namespace UserViewer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Resolve Logger from IoC/DI Configuration for use in GlobalExceptionLogging
            var resolver = config.DependencyResolver;
            ILogger logger = (ILogger)resolver.GetService(typeof(ILogger));

            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Services.Replace(typeof(IExceptionLogger), new GlobalExceptionLogger(logger));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
