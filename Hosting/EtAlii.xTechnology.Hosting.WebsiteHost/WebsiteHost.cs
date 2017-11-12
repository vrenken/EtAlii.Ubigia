namespace EtAlii.xTechnology.Hosting
{
    public partial class WebsiteHost : HostBase, IHost
    {
        private readonly IServiceManager _serviceManager;

        public WebsiteHost(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public void Start()
        {
            State = HostState.Starting;

            _serviceManager.Start();

            State = HostState.Running;
        }

        public void Stop()
        {
            State = HostState.Stopping;

            _serviceManager.Stop();

            State = HostState.Stopped;
        }

        public void Shutdown()
        {
            Stop();

            State = HostState.Shutdown;
        }
    }
}
