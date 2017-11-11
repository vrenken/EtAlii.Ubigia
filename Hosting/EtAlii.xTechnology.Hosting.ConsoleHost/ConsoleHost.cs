﻿namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Threading.Tasks;

    public partial class ConsoleHost : HostBase, IHost
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHostConfiguration _configuration;

        protected ConsoleHost(
            IHostConfiguration configuration,
            IServiceManager serviceManager)
        {
            _configuration = configuration;
            _serviceManager = serviceManager;
        }


        public void Start()
        {
            State = HostState.Starting;

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
