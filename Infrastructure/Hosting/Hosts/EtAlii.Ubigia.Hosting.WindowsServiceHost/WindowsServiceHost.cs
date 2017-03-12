﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.WindowsServiceHost
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Storage;

    public class WindowsServiceHost : HostBase
    {
        //private readonly ILogger _logger;

        public WindowsServiceHost(
            IInfrastructure infrastructure,
            IHostConfiguration configuration,
            IStorage storage)
            : base(configuration, infrastructure, storage)
        {
            //_logger = logger;
        }

        public override void Start()
        {
            Task.Delay(500).ContinueWith((o) =>
            {
                try
                {
                    Infrastructure.Start();
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

        public override void Stop()
        {
            Infrastructure.Stop();
        }

    }
}
