namespace EtAlii.Ubigia.Hosting
{
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Hosting;

    public class PowerShellHost : IHost
    {
        public IHostConfiguration Configuration { get; }

        public IInfrastructure Infrastructure { get; }

        public IStorage Storage { get; }

        private readonly ILogger _logger;

        public PowerShellHost(
            IHostConfiguration configuration,
            IInfrastructure infrastructure,
            IStorage storage,
            ILogger logger)
        {
            Configuration = configuration;
            Infrastructure = infrastructure;
            Storage = storage;
            _logger = logger;
        }

        public void Start() 
        {
            try
            {
                Infrastructure.Start();
            }
            catch (Exception e)
            {
                _logger.Critical("Fatal exception in infrastructure hosting", e);
                _logger.Info("Restarting infrastructure hosting");
                Task.Delay(2000);
                Stop();
                Task.Delay(1000);
                Start();
            }
        }
        public void Stop() 
        {
            Infrastructure.Stop();

            // End logging.
            //Logger.EndSession(); // Disabled because of performance loss.
        }
    }
}