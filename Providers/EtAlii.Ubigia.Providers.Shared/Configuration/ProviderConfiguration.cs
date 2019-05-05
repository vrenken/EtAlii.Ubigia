namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.Logging;

    public class ProviderConfiguration : Configuration<ProviderConfiguration>, IProviderConfiguration, IEditableProviderConfiguration
    {
        IGraphSLScriptContext IEditableProviderConfiguration.SystemScriptContext { get => SystemScriptContext; set => SystemScriptContext = value; }
        public IGraphSLScriptContext SystemScriptContext { get; private set; }

        IManagementConnection IEditableProviderConfiguration.ManagementConnection { get => ManagementConnection; set => ManagementConnection = value; }
        public IManagementConnection ManagementConnection { get; private set; }

        IProviderFactory IEditableProviderConfiguration.Factory { get => Factory; set => Factory = value; }
        public IProviderFactory Factory { get; private set; }

        ILogFactory IEditableProviderConfiguration.LogFactory { get => LogFactory; set => LogFactory = value; }
        public ILogFactory LogFactory { get; private set; }

        Func<IDataConnection, IGraphSLScriptContext> IEditableProviderConfiguration.ScriptContextFactory { get => _scriptContextFactory; set => _scriptContextFactory = value; }
        private Func<IDataConnection, IGraphSLScriptContext> _scriptContextFactory;

        public ProviderConfiguration()
        {
        }

        public ProviderConfiguration(ProviderConfiguration configuration)
            :this()
        {
            Factory = configuration.Factory;
            Use(configuration.Extensions);
            LogFactory = configuration.LogFactory;
        }
        
        public IGraphSLScriptContext CreateScriptContext(IDataConnection connection)
        {
            return _scriptContextFactory(connection);
        }

    }
}