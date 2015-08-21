using System;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Owin.Diagnostics;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin;
using Owin;
using OWIN.Windsor.DependencyResolverScopeMiddleware;

[assembly: OwinStartup("SampleLibraryStartup", typeof(SampleLibrary.Startup))]

namespace SampleLibrary
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = BootstrapContainer();

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = false,
                FileSystem = new PhysicalFileSystem("."),
                EnableDefaultFiles = true,
            };

            app.UseWindsorDependencyResolverScope(config, container)
                .UseWebApi(config)
                .UseFileServer(options)
                .UseErrorPage(ErrorPageOptions.ShowAll);
        }

        private static IWindsorContainer BootstrapContainer()
        {
            var container = new WindsorContainer();
            container.Register(
                Component.For<Func<string>>().Instance(() => "injected"),
                Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleScoped()
                );

            return container;
        }
    }
}
