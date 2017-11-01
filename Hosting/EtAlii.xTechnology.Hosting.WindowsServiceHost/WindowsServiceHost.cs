namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;

    public class WindowsServiceHost : IHost
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHostConfiguration _configuration;

        protected WindowsServiceHost(
            IHostConfiguration configuration,
            IServiceManager serviceManager)
        {
            _configuration = configuration;
            _serviceManager = serviceManager;
        }

        public void Start()
        {
            Task.Delay(500).ContinueWith((o) =>
            {
                try
                {
                    _serviceManager.Start();
                }
                catch (Exception)
                {
                    //_logger.Critical("Fatal exception in infrastructure hosting", e);
                    //_logger.Info("Restarting infrastructure hosting");
                    Task.Delay(2000);
                    Stop();
                    Task.Delay(1000);
                    Start();
                }
            });
        }

        public void Stop()
        {
            _serviceManager.Stop();
        }

    }
}
