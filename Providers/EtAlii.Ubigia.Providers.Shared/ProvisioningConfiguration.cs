namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
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

        Action<ManagementConnectionConfiguration>[] IEditableProvisioningConfiguration.ManagementConnectionConfigurationFactoryExtensions { get => _managementConnectionConfigurationFactoryExtensions; set => _managementConnectionConfigurationFactoryExtensions = value; }
        private Action<ManagementConnectionConfiguration>[] _managementConnectionConfigurationFactoryExtensions;

        Action<DataConnectionConfiguration>[] IEditableProvisioningConfiguration.DataConnectionConfigurationFactoryExtensions { get => _dataConnectionConfigurationFactoryExtensions; set => _dataConnectionConfigurationFactoryExtensions = value; }
        private Action<DataConnectionConfiguration>[] _dataConnectionConfigurationFactoryExtensions;
        
        Action<GraphSLScriptContextConfiguration>[] IEditableProvisioningConfiguration.ScriptContextConfigurationFactoryExtensions { get => _scriptContextConfigurationFactoryExtensions; set => _scriptContextConfigurationFactoryExtensions = value; }
        private Action<GraphSLScriptContextConfiguration>[] _scriptContextConfigurationFactoryExtensions;

        Func<ITransportProvider> IEditableProvisioningConfiguration.TransportProviderFactory { get => _transportProviderFactory; set => _transportProviderFactory = value; }
        private Func<ITransportProvider> _transportProviderFactory;

        Func<IStorageTransportProvider> IEditableProvisioningConfiguration.StorageTransportProviderFactory { get => _storageTransportProviderFactory; set => _storageTransportProviderFactory = value; }
        private Func<IStorageTransportProvider> _storageTransportProviderFactory;

        public ProvisioningConfiguration()
        {
            ProviderConfigurations = Array.Empty<ProviderConfiguration>();
            _dataConnectionConfigurationFactoryExtensions = Array.Empty<Action<DataConnectionConfiguration>>();
            _managementConnectionConfigurationFactoryExtensions = Array.Empty<Action<ManagementConnectionConfiguration>>();
            _scriptContextConfigurationFactoryExtensions = Array.Empty<Action<GraphSLScriptContextConfiguration>>();
        }

        public IStorageTransportProvider CreateStorageTransportProvider()
        {
            return _storageTransportProviderFactory();
        }

        public ITransportProvider CreateTransportProvider()
        {
            return _transportProviderFactory();
        }

        public DataConnectionConfiguration CreateDataConnectionConfiguration()
        {
            var configuration = new DataConnectionConfiguration();
            foreach (var extension in _dataConnectionConfigurationFactoryExtensions)
            {
                extension(configuration);
            }
            return configuration;
        }

        public ManagementConnectionConfiguration CreateManagementConnectionConfiguration()
        {
            var configuration = new ManagementConnectionConfiguration();
            foreach (var extension in _managementConnectionConfigurationFactoryExtensions)
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
            foreach (var extension in _scriptContextConfigurationFactoryExtensions) 
            {
                extension(configuration);
            }

            return new GraphSLScriptContextFactory().Create(configuration);
        }
    }
}
