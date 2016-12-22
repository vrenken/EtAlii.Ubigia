namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;
    using EtAlii.Servus.Infrastructure.Functional;
    using Microsoft.Owin.Hosting;

    public class DefaultApplicationManager : IApplicationManager
    {
        private readonly IInfrastructureConfiguration _configuration;
        private IDisposable _host;

        public DefaultApplicationManager(IInfrastructureConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Start(IComponentManager[] componentManagers)
        {
            _host = WebApp.Start(_configuration.Address, appBuilder =>
            {
                foreach (var componentManager in componentManagers)
                {
                    componentManager.Start(appBuilder);
                }   
            });
        }

        public void Stop(IComponentManager[] componentManagers)
        {
            foreach (var componentManager in componentManagers)
            {
                componentManager.Stop();
            }

            if (_host != null)
            {
                _host.Dispose();
                _host = null;
            }
        }
    }
}