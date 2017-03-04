namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Logging;

    public class ProviderConfiguration : IProviderConfiguration
    {
        public IDataContext SystemDataContext => _systemDataContext;
        private IDataContext _systemDataContext;

        public IManagementConnection ManagementConnection => _managementConnection;
        private IManagementConnection _managementConnection;

        public IProviderExtension[] Extensions => _extensions;
        private IProviderExtension[] _extensions;
        public IProviderFactory Factory => _factory;
        private IProviderFactory _factory;

        public ILogFactory LogFactory => _logFactory;
        private ILogFactory _logFactory;
        private Func<IDataConnection, IDataContext> _dataContextFactory;

        public ProviderConfiguration()
        {
            _extensions = new IProviderExtension[0];
        }

        public ProviderConfiguration(IProviderConfiguration configuration)
            :this()
        {
            _factory = configuration.Factory;
            _extensions = configuration.Extensions;
            _logFactory = configuration.LogFactory;
        }

        public IProviderConfiguration Use(IProviderExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            _extensions = extensions
                .Concat(_extensions)
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

            _factory = factory;

            return this;
        }

        public IProviderConfiguration Use(IManagementConnection managementConnection)
        {
            if (managementConnection == null)
            {
                throw new ArgumentException(nameof(managementConnection));
            }

            _managementConnection = managementConnection;

            return this;
        }

        public IProviderConfiguration Use(IDataContext systemDataContext)
        {
            if (systemDataContext == null)
            {
                throw new ArgumentException(nameof(systemDataContext));
            }

            _systemDataContext = systemDataContext;

            return this;
        }

        public IProviderConfiguration Use(ILogFactory logFactory)
        {
            if (logFactory == null)
            {
                throw new ArgumentException(nameof(logFactory));
            }

            _logFactory = logFactory;

            return this;
        }

        public IProviderConfiguration Use(Func<IDataConnection, IDataContext> dataContextFactory)
        {
            if (dataContextFactory == null)
            {
                throw new ArgumentException(nameof(dataContextFactory));
            }

            _dataContextFactory = dataContextFactory;

            return this;
        }

        public IDataContext CreateDataContext(IDataConnection connection)
        {
            return _dataContextFactory(connection);
        }
    }
}