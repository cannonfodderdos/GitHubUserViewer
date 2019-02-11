using Autofac;
using Autofac.Integration.WebApi;
using Core.ApplicationServices;
using Core.Domain.Interfaces;
using Infrastructure.GitHubServiceV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace UserViewer
{
    public class IocConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Single instance so that HTTP Client doesn't exhaust available sockets
            builder.RegisterType<GitHubService>()
                .As<IGitHubService>()
                .SingleInstance();

            builder.RegisterType<UserService>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}