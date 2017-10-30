namespace EtAlii.xTechnology.Hosting
{
    using System.Linq;

    class ServiceManager : IServiceManager
    {
        private IHostService[] _services;

        public void Initialize(IHostService[] services)
        {
            _services = services;
        }

        public void Start()
        {
            foreach (var service in _services)
            {

                service.Start();
            }
        }

        public void Stop()
        {
            foreach (var service in _services.Reverse())
            {
                service.Stop();
            }
        }
    }
}