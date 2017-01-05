namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi;
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
