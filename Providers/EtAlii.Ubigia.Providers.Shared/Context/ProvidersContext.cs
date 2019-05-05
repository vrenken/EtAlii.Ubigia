namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public class ProvidersContext : IProvidersContext
    {
        public IGraphSLScriptContext SystemScriptContext { get; }

        public IManagementConnection ManagementConnection { get; }

        public ProviderConfiguration[] ProviderConfigurations { get; private set; }

        private Func<IDataConnection, IGraphSLScriptContext> _scriptContextFactory;

        public ProvidersContext(
            IGraphSLScriptContext systemScriptContext,
            IManagementConnection managementConnection)
        {
            SystemScriptContext = systemScriptContext;
            ManagementConnection = managementConnection;
        }

        public void Initialize(ProviderConfiguration[] providerConfigurations, Func<IDataConnection, IGraphSLScriptContext> scriptContextFactory)
        {
            if (ProviderConfigurations != null || _scriptContextFactory != null)
            {
                throw new InvalidOperationException("ProviderContext has already been initialized");
            }
            _scriptContextFactory = scriptContextFactory;
            ProviderConfigurations = providerConfigurations;
        }


        public IGraphSLScriptContext CreateScriptContext(IDataConnection connection)
        {
            return _scriptContextFactory(connection);
        }
    }
}