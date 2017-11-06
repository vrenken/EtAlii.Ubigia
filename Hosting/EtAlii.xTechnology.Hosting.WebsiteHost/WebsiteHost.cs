namespace EtAlii.xTechnology.Hosting
{
    public class WebsiteHost : HostBase, IHost
    {
        private readonly IServiceManager _serviceManager;

        public WebsiteHost(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public void Start()
        {
            Status = HostStatus.Starting;

            _serviceManager.Start();

            Status = HostStatus.Running;
        }

        public void Stop()
        {
            Status = HostStatus.Stopping;

            _serviceManager.Stop();

            Status = HostStatus.Stopped;
        }

        public void Shutdown()
        {
            Stop();

            Status = HostStatus.Shutdown;
        }
    }
}
