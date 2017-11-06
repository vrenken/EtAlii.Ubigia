namespace EtAlii.Ubigia.Hosting
{
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    public class PowerShellTestHost : HostBase, IHost
    {
        public IHostConfiguration Configuration { get; }

        public IInfrastructure Infrastructure { get; }

        private readonly IServiceManager _serviceManager;
        private readonly ILogger _logger;

        public PowerShellTestHost(
            IServiceManager serviceManager,
            IHostConfiguration configuration,
            IInfrastructure infrastructure,
            ILogger logger)
        {
            Configuration = configuration;
            Infrastructure = infrastructure;
            _serviceManager = serviceManager;
            _logger = logger;
        }

        public void Start()
        {
            Status = HostStatus.Starting;
            try
            {
                _serviceManager.Start();
                Status = HostStatus.Running;
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
            Status = HostStatus.Stopping;

            _serviceManager.Stop();

            // End logging.
            //Logger.EndSession(); // Disabled because of performance loss.

            Status = HostStatus.Stopped;
        }
        public void Shutdown()
        {
            Stop();

            Status = HostStatus.Shutdown;
        }
    }
}