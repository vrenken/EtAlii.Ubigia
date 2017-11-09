using System.Web.Configuration;
using EtAlii.xTechnology.Hosting;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.Ubigia.Infrastructure.Hosting.Owin;
    using global::Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder application)
        {
            var applicationManager = new WebsiteApplicationManager(application);

            var configuration = new HostConfigurationBuilder().Build(sectionName => WebConfigurationManager.GetWebApplicationSection(sectionName), applicationManager);

            var host = new HostFactory<WebsiteHost>().Create(configuration);

            // Start hosting both the infrastructure and the storage.
            host.Start();

        }
    }
}
