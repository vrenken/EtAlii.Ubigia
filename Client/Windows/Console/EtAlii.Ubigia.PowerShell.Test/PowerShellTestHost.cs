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
            State = HostState.Starting;
            try
            {
                _serviceManager.Start();
                State = HostState.Running;
            }
            catch (Exception e)
            {
                State = HostState.Error;
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
            State = HostState.Stopping;

            _serviceManager.Stop();

            // End logging.
            //Logger.EndSession(); // Disabled because of performance loss.

            State = HostState.Stopped;
        }
        public void Shutdown()
        {
            Stop();

            State = HostState.Shutdown;
        }
    }
}