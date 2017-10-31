namespace EtAlii.xTechnology.Hosting
{
    public class WebsiteHost : IHost
    {
        private readonly IServiceManager _serviceManager;

        public WebsiteHost(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public void Start()
        {
            _serviceManager.Start();
        }

        public void Stop()
        {
            _serviceManager.Stop();
        }
    }
}
