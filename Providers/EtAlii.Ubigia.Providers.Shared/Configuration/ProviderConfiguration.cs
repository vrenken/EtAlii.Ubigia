namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.Logging;

    public class ProviderConfiguration : Configuration, IProviderConfiguration, IEditableProviderConfiguration
    {
        IGraphSLScriptContext IEditableProviderConfiguration.SystemScriptContext { get => SystemScriptContext; set => SystemScriptContext = value; }
        public IGraphSLScriptContext SystemScriptContext { get; private set; }

        IManagementConnection IEditableProviderConfiguration.ManagementConnection { get => ManagementConnection; set => ManagementConnection = value; }
        public IManagementConnection ManagementConnection { get; private set; }

        IProviderFactory IEditableProviderConfiguration.Factory { get => Factory; set => Factory = value; }
        public IProviderFactory Factory { get; private set; }

        ILogFactory IEditableProviderConfiguration.LogFactory { get => LogFactory; set => LogFactory = value; }
        public ILogFactory LogFactory { get; private set; }

        Func<IDataConnection, IGraphSLScriptContext> IEditableProviderConfiguration.ScriptContextFactory { get; set; }

        public ProviderConfiguration()
        {
        }

        public ProviderConfiguration(ProviderConfiguration configuration)
            :this()
        {
            Factory = configuration.Factory;
            LogFactory = configuration.LogFactory;
            this.Use(configuration.Extensions);
        }
        
        public IGraphSLScriptContext CreateScriptContext(IDataConnection connection)
        {
            return ((IEditableProviderConfiguration)this).ScriptContextFactory(connection);
        }

    }
}