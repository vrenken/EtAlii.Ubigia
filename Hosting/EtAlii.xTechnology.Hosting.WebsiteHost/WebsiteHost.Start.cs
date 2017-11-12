using System.Web.Configuration;
using EtAlii.xTechnology.Hosting;
using Microsoft.Owin;

//[assembly: OwinStartup(typeof(Startup))]

namespace EtAlii.xTechnology.Hosting
{
    using global::Owin;

    public partial class WebsiteHost
    {
        public static void Start(IHostConfiguration configuration)
        {
            // To use in webapplication
            //var applicationManager = new ExistingApplicationManager(application);
            //var configuration = new HostConfigurationBuilder()
            //    .Build(sectionName => WebConfigurationManager.GetWebApplicationSection(sectionName), applicationManager)
            //    .UseWebsiteHost();
            //WebsiteHost.Start(configuration);

            var host = new HostFactory<WebsiteHost>().Create(configuration);

            // Start hosting both the infrastructure and the storage.
            host.Start();

        }
    }
}
