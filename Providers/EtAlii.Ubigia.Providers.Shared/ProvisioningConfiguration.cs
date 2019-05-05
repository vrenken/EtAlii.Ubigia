namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public class ProvisioningConfiguration : Configuration<ProvisioningConfiguration>, IProvisioningConfiguration, IEditableProvisioningConfiguration
    {
        IProviderConfiguration[] IEditableProvisioningConfiguration.ProviderConfigurations { get => ProviderConfigurations; set => ProviderConfigurations = value; }
        public IProviderConfiguration[] ProviderConfigurations { get; private set; }

        Uri IEditableProvisioningConfiguration.Address { get => Address; set => Address = value; }
        public Uri Address { get; private set; }

        string IEditableProvisioningConfiguration.Account { get => Account; set => Account = value; }
        public string Account { get; private set; }

        string IEditableProvisioningConfiguration.Password { get => Password; set => Password = value; }
        public string Password { get; private set; }

        Action<IManagementConnectionConfiguration>[] IEditableProvisioningConfiguration.ManagementConnectionConfigurationFactoryExtensions { get => _managementConnectionConfigurationFactoryExtensions; set => _managementConnectionConfigurationFactoryExtensions = value; }
        private Action<IManagementConnectionConfiguration>[] _managementConnectionConfigurationFactoryExtensions;

        Action<IDataConnectionConfiguration>[] IEditableProvisioningConfiguration.DataConnectionConfigurationFactoryExtensions { get => _dataConnectionConfigurationFactoryExtensions; set => _dataConnectionConfigurationFactoryExtensions = value; }
        private Action<IDataConnectionConfiguration>[] _dataConnectionConfigurationFactoryExtensions;
        
        Action<IGraphSLScriptContextConfiguration>[] IEditableProvisioningConfiguration.ScriptContextConfigurationFactoryExtensions { get => _scriptContextConfigurationFactoryExtensions; set => _scriptContextConfigurationFactoryExtensions = value; }
        private Action<IGraphSLScriptContextConfiguration>[] _scriptContextConfigurationFactoryExtensions;

        Func<ITransportProvider> IEditableProvisioningConfiguration.TransportProviderFactory { get => _transportProviderFactory; set => _transportProviderFactory = value; }
        private Func<ITransportProvider> _transportProviderFactory;

        Func<IStorageTransportProvider> IEditableProvisioningConfiguration.StorageTransportProviderFactory { get => _storageTransportProviderFactory; set => _storageTransportProviderFactory = value; }
        private Func<IStorageTransportProvider> _storageTransportProviderFactory;

        public ProvisioningConfiguration()
        {
            ProviderConfigurations = new IProviderConfiguration[0];
            _dataConnectionConfigurationFactoryExtensions = new Action<IDataConnectionConfiguration>[0];
            _managementConnectionConfigurationFactoryExtensions = new Action<IManagementConnectionConfiguration>[0];
            _scriptContextConfigurationFactoryExtensions = new Action<IGraphSLScriptContextConfiguration>[0];
        }

        public IStorageTransportProvider CreateStorageTransportProvider()
        {
            return _storageTransportProviderFactory();
        }

        public ITransportProvider CreateTransportProvider()
        {
            return _transportProviderFactory();
        }

        public IDataConnectionConfiguration CreateDataConnectionConfiguration()
        {
            var configuration = new DataConnectionConfiguration();
            foreach (var extension in _dataConnectionConfigurationFactoryExtensions)
            {
                extension(configuration);
            }
            return configuration;
        }

        public IManagementConnectionConfiguration CreateManagementConnectionConfiguration()
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
            var fabricContextConfiguration = new FabricContextConfiguration()
                .UseTraversalCaching(useCaching)
                .Use(connection);
            var fabricContext = new FabricContextFactory().Create(fabricContextConfiguration);

            var logicalContextConfiguration = new LogicalContextConfiguration()
                .UseCaching(useCaching)
                //.Use(_diagnostics)
                .Use(fabricContext);
            var logicalContext = new LogicalContextFactory().Create(logicalContextConfiguration);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(logicalContext);
            foreach (var extension in _scriptContextConfigurationFactoryExtensions)
            {
                extension(configuration);
            }

            return new GraphSLScriptContextFactory().Create(configuration);
        }
    }
}
