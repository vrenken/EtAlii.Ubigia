namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public class ProvisioningConfiguration : Configuration, IProvisioningConfiguration, IEditableProvisioningConfiguration
    {
        ProviderConfiguration[] IEditableProvisioningConfiguration.ProviderConfigurations { get => ProviderConfigurations; set => ProviderConfigurations = value; }
        public ProviderConfiguration[] ProviderConfigurations { get; private set; }

        Uri IEditableProvisioningConfiguration.Address { get => Address; set => Address = value; }
        public Uri Address { get; private set; }

        string IEditableProvisioningConfiguration.Account { get => Account; set => Account = value; }
        public string Account { get; private set; }

        string IEditableProvisioningConfiguration.Password { get => Password; set => Password = value; }
        public string Password { get; private set; }

        Action<ManagementConnectionConfiguration>[] IEditableProvisioningConfiguration.ManagementConnectionConfigurationFactoryExtensions { get; set; }

        Action<DataConnectionConfiguration>[] IEditableProvisioningConfiguration.DataConnectionConfigurationFactoryExtensions { get; set; }
        
        Action<GraphSLScriptContextConfiguration>[] IEditableProvisioningConfiguration.ScriptContextConfigurationFactoryExtensions { get; set; }

        Func<ITransportProvider> IEditableProvisioningConfiguration.TransportProviderFactory { get; set; }

        Func<IStorageTransportProvider> IEditableProvisioningConfiguration.StorageTransportProviderFactory { get; set; }

        public ProvisioningConfiguration()
        {
            ProviderConfigurations = Array.Empty<ProviderConfiguration>();
            ((IEditableProvisioningConfiguration) this).DataConnectionConfigurationFactoryExtensions  = Array.Empty<Action<DataConnectionConfiguration>>();
            ((IEditableProvisioningConfiguration) this).ManagementConnectionConfigurationFactoryExtensions = Array.Empty<Action<ManagementConnectionConfiguration>>();
            ((IEditableProvisioningConfiguration) this).ScriptContextConfigurationFactoryExtensions = Array.Empty<Action<GraphSLScriptContextConfiguration>>();
        }

        public IStorageTransportProvider CreateStorageTransportProvider()
        {
            return ((IEditableProvisioningConfiguration) this).StorageTransportProviderFactory();
        }

        public ITransportProvider CreateTransportProvider()
        {
            return ((IEditableProvisioningConfiguration) this).TransportProviderFactory();
        }

        public DataConnectionConfiguration CreateDataConnectionConfiguration()
        {
            var configuration = new DataConnectionConfiguration();
            foreach (var extension in ((IEditableProvisioningConfiguration) this).DataConnectionConfigurationFactoryExtensions)
            {
                extension(configuration);
            }
            return configuration;
        }

        public ManagementConnectionConfiguration CreateManagementConnectionConfiguration()
        {
            var configuration = new ManagementConnectionConfiguration();
            foreach (var extension in ((IEditableProvisioningConfiguration) this).ManagementConnectionConfigurationFactoryExtensions)
            {
                extension(configuration);
            }
            return configuration;
        }

        public IGraphSLScriptContext CreateScriptContext(IDataConnection connection, bool useCaching = true)
        {
            var configuration = new GraphSLScriptContextConfiguration()
                .UseCaching(useCaching)
                //.Use(_diagnostics)
                .UseTraversalCaching(useCaching)
                .Use(connection);
            foreach (var extension in ((IEditableProvisioningConfiguration) this).ScriptContextConfigurationFactoryExtensions) 
            {
                extension(configuration);
            }

            return new GraphSLScriptContextFactory().Create(configuration);
        }
    }
}
