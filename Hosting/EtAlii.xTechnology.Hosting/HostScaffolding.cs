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
            container.Register<IHost, THost>();
            container.RegisterInitializer<IHost>(host =>
            {
                host.Initialize(_configuration.Commands);
                foreach (var command in _configuration.Commands)
                {
                    command.Initialize(host);
                }
            });
            container.Register<IServiceManager, ServiceManager>();
            container.RegisterInitializer<IServiceManager>(serviceManager =>
            {
                var services = _configuration.Services
                .Select(service => (IHostService)container.GetInstance(service))
                .ToArray();
                serviceManager.Initialize(services);
            });

            container.Register<IHostConfiguration>(() => _configuration);
        }
    }
}
