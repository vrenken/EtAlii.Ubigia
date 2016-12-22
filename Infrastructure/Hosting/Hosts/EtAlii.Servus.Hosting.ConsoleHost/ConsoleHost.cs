namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Storage;

    public class ConsoleHost : HostBase
    {
        //private readonly ILogger _logger;

        public ConsoleHost(
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
