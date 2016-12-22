using EtAlii.Servus.Infrastructure.Hosting;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Infrastructure.WebApi;
    using global::Owin;

    public class WebsiteApplicationManager : IApplicationManager
    {
        private readonly IAppBuilder _applicationBuilder;

        public WebsiteApplicationManager(IAppBuilder applicationBuilder)
        {
            _applicationBuilder = applicationBuilder;
        }

        public void Start(IComponentManager[] componentManagers)
        {
            foreach (var componentManager in componentManagers)
            {
                componentManager.Start(_applicationBuilder);
            }
        }

        public void Stop(IComponentManager[] componentManagers)
        {
            foreach (var componentManager in componentManagers)
            {
                componentManager.Stop();
            }
        }
    }
}
