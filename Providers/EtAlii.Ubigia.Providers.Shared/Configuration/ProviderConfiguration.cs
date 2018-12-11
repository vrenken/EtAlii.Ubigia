namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.Logging;

    public class ProviderConfiguration : IProviderConfiguration
    {
        public IGraphSLScriptContext SystemScriptContext { get; private set; }

        public IManagementConnection ManagementConnection { get; private set; }

        public IProviderExtension[] Extensions { get; private set; }

        public IProviderFactory Factory { get; private set; }

        public ILogFactory LogFactory { get; private set; }

        private Func<IDataConnection, IGraphSLScriptContext> _scriptContextFactory;

        public ProviderConfiguration()
        {
            Extensions = new IProviderExtension[0];
        }

        public ProviderConfiguration(IProviderConfiguration configuration)
            :this()
        {
            Factory = configuration.Factory;
            Extensions = configuration.Extensions;
            LogFactory = configuration.LogFactory;
        }

        public IProviderConfiguration Use(IProviderExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IProviderConfiguration Use(IProviderFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentException(nameof(factory));
            }

            Factory = factory;

            return this;
        }

        public IProviderConfiguration Use(IManagementConnection managementConnection)
        {
            if (managementConnection == null)
            {
                throw new ArgumentException(nameof(managementConnection));
            }

            ManagementConnection = managementConnection;

            return this;
        }

        public IProviderConfiguration Use(IGraphSLScriptContext systemScriptContext)
        {
            if (systemScriptContext == null)
            {
                throw new ArgumentException(nameof(systemScriptContext));
            }

            SystemScriptContext = systemScriptContext;

            return this;
        }

        public IProviderConfiguration Use(ILogFactory logFactory)
        {
            if (logFactory == null)
            {
                throw new ArgumentException(nameof(logFactory));
            }

            LogFactory = logFactory;

            return this;
        }

        public IProviderConfiguration Use(Func<IDataConnection, IGraphSLScriptContext> scriptContextFactory)
        {
            if (scriptContextFactory == null)
            {
                throw new ArgumentException(nameof(scriptContextFactory));
            }

            _scriptContextFactory = scriptContextFactory;

            return this;
        }

        public IGraphSLScriptContext CreateScriptContext(IDataConnection connection)
        {
            return _scriptContextFactory(connection);
        }
    }
}