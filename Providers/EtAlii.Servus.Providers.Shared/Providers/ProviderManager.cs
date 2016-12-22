namespace EtAlii.Servus.Provisioning
{
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Collections.Generic;

    public class ProviderManager : IProviderManager
    {
        private readonly IProvidersContext _context;
        private readonly ILogger _logger;
        private readonly ILogFactory _logFactory;
        private IProvider[] _providers;


        public ProviderManager(IProvidersContext context, 
            ILogger logger, 
            ILogFactory logFactory)
        {
            _context = context;
            _logger = logger;
            _logFactory = logFactory;
        }

        public void Start()
        {
            if (_providers != null)
            {
                throw new InvalidOperationException("Providers have already been started");
            }

            _logger.Info("Starting providers");

            var providers = new List<IProvider>();

            foreach (var configuration in _context.ProviderConfigurations)
            {
                IProvider provider = null;
                try
                {
                    var copiedProviderConfiguration = new ProviderConfiguration(configuration)
                        .Use(connection => _context.CreateDataContext(connection))
                        .Use(_context.ManagementConnection)
                        .Use(_context.SystemDataContext)
                        .Use(_logFactory);
                    provider = copiedProviderConfiguration.Factory.Create(copiedProviderConfiguration);
                }
                catch (Exception e)
                {
                    _logger.Critical("Unable to create provider using factory {0}", e, configuration.Factory.GetType());
                }

                if (provider != null)
                {
                    try
                    {
                        provider.Start();
                        providers.Add(provider);
                    }
                    catch (Exception e)
                    {
                        _logger.Critical("Unable to start provider {0}", e, provider.GetType());
                    }
                }
            }

            _providers = providers.ToArray();

            _logger.Info("Started providers");
        }

        public void Stop()
        {
            if (_providers == null)
            {
                throw new InvalidOperationException("Providers have not been started");
            }

            _logger.Info("Stopping providers");
            foreach (var provider in _providers)
            {
                try
                {
                    provider.Stop();
                }
                catch (Exception e)
                {
                    _logger.Critical("Unable to stop provider {0}", e, provider.GetType());
                }
            }

            _providers = null;
            _logger.Info("Stopped providers");
        }
    }
}