﻿namespace EtAlii.Servus.Hosting
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Storage;
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Infrastructure.Hosting;

    public class PowerShellHost : IHost
    {
        public IHostConfiguration Configuration { get { return _configuration; } }
        private readonly IHostConfiguration _configuration;

        public IInfrastructure Infrastructure { get { return _infrastructure; } }
        private readonly IInfrastructure _infrastructure;

        public IStorage Storage { get { return _storage; } }
        private IStorage _storage;

        private readonly ILogger _logger;

        public PowerShellHost(
            IHostConfiguration configuration,
            IInfrastructure infrastructure,
            IStorage storage,
            ILogger logger)
        {
            _configuration = configuration;
            _infrastructure = infrastructure;
            _storage = storage;
            _logger = logger;
        }

        public void Start() 
        {
            try
            {
                _infrastructure.Start();
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
            _infrastructure.Stop();

            // End logging.
            //Logger.EndSession(); // Disabled because of performance loss.
        }
    }
}