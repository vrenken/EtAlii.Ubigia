namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.Ubigia.Api.Transport;

    public class ProvidersContext : IProvidersContext
    {
        public IDataContext SystemDataContext => _systemDataContext;
        private readonly IDataContext _systemDataContext;

        public IManagementConnection ManagementConnection => _managementConnection;
        private readonly IManagementConnection _managementConnection;

        public IProviderConfiguration[] ProviderConfigurations => _providerConfigurations;
        private IProviderConfiguration[] _providerConfigurations;
        private Func<IDataConnection, IDataContext> _dataContextFactory;

        public ProvidersContext(
            IDataContext systemDataContext,
            IManagementConnection managementConnection)
        {
            _systemDataContext = systemDataContext;
            _managementConnection = managementConnection;
        }

        public void Initialize(IProviderConfiguration[] providerConfigurations, Func<IDataConnection, IDataContext> dataContextFactory)
        {
            if (_providerConfigurations != null || _dataContextFactory != null)
            {
                throw new InvalidOperationException("ProviderContext has already been initialized");
            }
            _dataContextFactory = dataContextFactory;
            _providerConfigurations = providerConfigurations;
        }


        public IDataContext CreateDataContext(IDataConnection connection)
        {
            return _dataContextFactory(connection);
        }
    }
}