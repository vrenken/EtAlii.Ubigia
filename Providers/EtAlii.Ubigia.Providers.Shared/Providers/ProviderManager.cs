namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProviderManager : IProviderManager
    {
        private readonly IProvidersContext _context;
        private IProvider[] _providers;
        //private readonly ILogger _logger
        //private readonly ILogFactory _logFactory

        public string Status { get; private set; }

        public ProviderManager(IProvidersContext context//,
            //ILogger logger,
            //ILogFactory logFactory
            )
        {
            _context = context;
            //_logger = logger
            //_logFactory = logFactory
        }

        public void Start()
        {
            if (_providers != null)
            {
                throw new InvalidOperationException("Providers have already been started");
            }

            var sb = new StringBuilder();

            sb.AppendLine("Starting providers");

            var providers = new List<IProvider>();

            foreach (var configuration in _context.ProviderConfigurations)
            {
                IProvider provider = null;
                try
                {
                    var copiedProviderConfiguration = new ProviderConfiguration(configuration)
                        .Use(connection => _context.CreateScriptContext(connection))
                        .Use(_context.ManagementConnection)
                        .Use(_context.SystemScriptContext);
                        //.Use(_logFactory)
                    provider = copiedProviderConfiguration.Factory.Create(copiedProviderConfiguration);
                }
                catch (Exception)// e)
                {
                    sb.AppendLine($"Unable to create provider using factory {configuration.Factory.GetType()}");
                    //_logger.Critical("Unable to create provider using factory [0]", e, configuration.Factory.GetType())
                }

                if (provider != null)
                {
                    try
                    {
                        provider.Start();
                        providers.Add(provider);
                    }
                    catch (Exception)// e)
                    {
                        sb.AppendLine($"Unable to start provider {provider.GetType()}");
                        //_logger.Critical("Unable to start provider [0]", e, provider.GetType())
                    }
                }
            }

            _providers = providers.ToArray();

            sb.AppendLine("Started providers");
            //_logger.Info("Started providers")

            Status = sb.ToString();
        }

        public void Stop()
        {
            if (_providers == null)
            {
                throw new InvalidOperationException("Providers have not been started");
            }

            var sb = new StringBuilder();

            sb.AppendLine("Stopping providers");
            //_logger.Info("Stopping providers")

            foreach (var provider in _providers)
            {
                try
                {
                    provider.Stop();
                }
                catch (Exception)// e)
                {
                    sb.AppendLine($"Unable to stop provider {provider.GetType()}");
                    //_logger.Critical("Unable to stop provider [0]", e, provider.GetType())
                }
            }

            _providers = null;
            sb.AppendLine($"Stopped providers");
            //_logger.Info("Stopped providers")

            Status = sb.ToString();
        }
    }
}