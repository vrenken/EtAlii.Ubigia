namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public class ProvisioningConfiguration : IProvisioningConfiguration
    {
        public IProvisioningExtension[] Extensions { get; private set; }

        public IProviderConfiguration[] ProviderConfigurations { get; private set; }

        //public IStorage Storage { get { return _storage; } }
        //private IStorage _storage;

        public string Address { get; private set; }

        public string Account { get; private set; }

        public string Password { get; private set; }

        private Action<IManagementConnectionConfiguration>[] _managementConnectionConfigurationFactoryExtensions;
        private Action<IDataConnectionConfiguration>[] _dataConnectionConfigurationFactoryExtensions;
        private Action<IDataContextConfiguration>[] _dataContextConfigurationFactoryExtensions;

        private Func<ITransportProvider> _transportProviderFactory;
        private Func<IStorageTransportProvider> _storageTransportProviderFactory;

        public ProvisioningConfiguration()
        {
            Extensions = new IProvisioningExtension[0];
            ProviderConfigurations = new IProviderConfiguration[0];
            _dataConnectionConfigurationFactoryExtensions = new Action<IDataConnectionConfiguration>[0];
            _managementConnectionConfigurationFactoryExtensions = new Action<IManagementConnectionConfiguration>[0];
            _dataContextConfigurationFactoryExtensions = new Action<IDataContextConfiguration>[0];
        }

        public IStorageTransportProvider CreateStorageTransportProvider()
        {
            return _storageTransportProviderFactory();
        }

        public ITransportProvider CreateTransportProvider()
        {
            return _transportProviderFactory();
        }

        public IProvisioningConfiguration Use(IProvisioningExtension[] extensions)
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

        public IProvisioningConfiguration Use(string address, string account, string password)
        {
            if (String.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException(nameof(address));
            }
            if (String.IsNullOrWhiteSpace(account))
            {
                throw new ArgumentException(nameof(account));
            }
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(nameof(password));
            }

            Address = address;
            Account = account;
            Password = password;
            return this;
        }

        public IProvisioningConfiguration Use(IProviderConfiguration[] providerConfigurations)
        {
            if (providerConfigurations == null)
            {
                throw new ArgumentException(nameof(providerConfigurations));
            }
            ProviderConfigurations = providerConfigurations;

            return this;
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

        public IDataContext CreateDataContext(IDataConnection connection, bool useCaching = true)
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

            var configuration = new DataContextConfiguration()
                .Use(logicalContext);
            foreach (var extension in _dataContextConfigurationFactoryExtensions)
            {
                extension(configuration);
            }

            return new DataContextFactory().Create(configuration);
        }

        public IProvisioningConfiguration Use(Action<IDataConnectionConfiguration> dataConnectionConfigurationFactoryExtension)
        {
            if (dataConnectionConfigurationFactoryExtension == null)
            {
                throw new ArgumentException(nameof(dataConnectionConfigurationFactoryExtension));
            }

            _dataConnectionConfigurationFactoryExtensions = new[] { dataConnectionConfigurationFactoryExtension }
                .Concat(_dataConnectionConfigurationFactoryExtensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IProvisioningConfiguration Use(Action<IManagementConnectionConfiguration> managementConnectionConfigurationFactoryExtension)
        {
            if (managementConnectionConfigurationFactoryExtension == null)
            {
                throw new ArgumentException(nameof(managementConnectionConfigurationFactoryExtension));
            }

            _managementConnectionConfigurationFactoryExtensions = new[] { managementConnectionConfigurationFactoryExtension }
                .Concat(_managementConnectionConfigurationFactoryExtensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IProvisioningConfiguration Use(Action<IDataContextConfiguration> dataContextConfigurationFactoryExtension)
        {
            if (dataContextConfigurationFactoryExtension == null)
            {
                throw new ArgumentException(nameof(dataContextConfigurationFactoryExtension));
            }

            _dataContextConfigurationFactoryExtensions = new[] { dataContextConfigurationFactoryExtension }
                .Concat(_dataContextConfigurationFactoryExtensions)
                .Distinct()
                .ToArray();
            return this;
        }
        public IProvisioningConfiguration Use(Func<ITransportProvider> transportProviderFactory)
        {
            _transportProviderFactory = transportProviderFactory ?? throw new ArgumentException(nameof(transportProviderFactory));
            return this;
        }

        public IProvisioningConfiguration Use(Func<IStorageTransportProvider> storageTransportProviderFactory)
        {
            _storageTransportProviderFactory = storageTransportProviderFactory ?? throw new ArgumentException(nameof(storageTransportProviderFactory));
            return this;
        }

    }
}
