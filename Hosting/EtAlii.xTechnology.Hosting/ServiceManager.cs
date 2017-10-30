namespace EtAlii.xTechnology.Hosting
{
    using System.Linq;

    class ServiceManager : IServiceManager
    {
        private readonly IHostConfiguration _configuration;

        public ServiceManager(IHostConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Start()
        {
            foreach (var service in _configuration.Services)
            {
                service.Start();
            }
        }

        public void Stop()
        {
            foreach (var service in _configuration.Services.Reverse())
            {
                service.Stop();
            }
        }
    }
}