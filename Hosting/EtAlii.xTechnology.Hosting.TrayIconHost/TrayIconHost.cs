namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;

    public partial class TrayIconHost : HostBase, ITrayIconHost
    {
        public ITaskbarIcon TaskbarIcon { get; }

        //private readonly ILogger _logger;

        private readonly IServiceManager _serviceManager;
        private readonly IHostConfiguration _configuration;

        protected TrayIconHost(
            IHostConfiguration configuration,
            IServiceManager serviceManager,
            ITaskbarIcon taskbarIcon)
        {
            _configuration = configuration;
            _serviceManager = serviceManager;
            TaskbarIcon = taskbarIcon;
            //_logger = logger;
        }


        public void Start()
        {
            State = HostState.Starting;

            TaskbarIcon.Dispatcher.Invoke(() => TaskbarIcon.Visibility = Visibility.Visible);

            Task.Delay(500).ContinueWith((o) =>
            {
                try
                {
                    _serviceManager.Start();

                    State = HostState.Running;
                }
                catch (Exception)
                {
                    State = HostState.Error;

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
