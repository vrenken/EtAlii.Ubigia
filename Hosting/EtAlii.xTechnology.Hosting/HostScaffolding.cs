namespace EtAlii.xTechnology.Hosting
{
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;
    public class HostScaffolding<THost> : IScaffolding
        where THost : class, IHost
    {
        private readonly IHostConfiguration _configuration;

        public HostScaffolding(IHostConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IHostConfiguration>(() => _configuration);

            container.Register<IHost, THost>();
            container.RegisterInitializer<IHost>(host =>
            {
                foreach (var command in _configuration.Commands)
                {
                    command.Initialize(host);
                }
                var serviceManager = container.GetInstance<IServiceManager>();
                var services = _configuration.Services
                    .Select(service => (IHostService)container.GetInstance(service))
                    .ToArray();

                var status = services
                    .Select(service => service.Status)
                    .ToArray();

                host.Initialize(_configuration.Commands, status);
                serviceManager.Initialize(services);
                
            });
            container.Register<IServiceManager, ServiceManager>();
        }
    }
}
