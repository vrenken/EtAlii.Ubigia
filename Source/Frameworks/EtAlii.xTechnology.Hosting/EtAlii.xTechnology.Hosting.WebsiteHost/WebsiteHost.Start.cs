

//[assembly: OwinStartup(typeof(Startup))]

namespace EtAlii.xTechnology.Hosting.WebsiteHost.NetCore
{
    public partial class WebsiteHost
    {
        public static void Start(IHostConfiguration configuration)
        {
            // To use in webapplication

            var host = new HostFactory<WebsiteHost>().Create(configuration);

            // Start hosting both the infrastructure and the storage.
            host.Start();

        }
    }
}
