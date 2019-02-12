using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Core.ApplicationServices;
using Core.Domain.Interfaces;
using Infrastructure.GitHubServiceV3;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using UserViewer.Logging;

namespace UserViewer
{
    public class IocConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            // Register autofac for both Web API and MVC
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register Logging
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new OutputLoggerProvider(LogLevel.Debug));

            var logger = loggerFactory.CreateLogger<Logging.OutputLogger>();

            builder.RegisterInstance<ILogger>(logger);

            // Register automapper module
            builder.RegisterModule(new Mapping.AutoMapperModule());

            // Single instance so that HTTP Client doesn't exhaust available sockets
            builder.RegisterType<GitHubService>()
                .As<IGitHubService>()
                .SingleInstance();

            builder.RegisterType<UserService>();

            // Set resolver for both Web API and MVC
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}