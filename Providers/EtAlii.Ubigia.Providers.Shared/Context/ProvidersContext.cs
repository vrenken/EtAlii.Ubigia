namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public class ProvidersContext : IProvidersContext
    {
        public IDataContext SystemDataContext { get; }

        public IManagementConnection ManagementConnection { get; }

        public IProviderConfiguration[] ProviderConfigurations { get; private set; }

        private Func<IDataConnection, IDataContext> _dataContextFactory;

        public ProvidersContext(
            IDataContext systemDataContext,
            IManagementConnection managementConnection)
        {
            SystemDataContext = systemDataContext;
            ManagementConnection = managementConnection;
        }

        public void Initialize(IProviderConfiguration[] providerConfigurations, Func<IDataConnection, IDataContext> dataContextFactory)
        {
            if (ProviderConfigurations != null || _dataContextFactory != null)
            {
                throw new InvalidOperationException("ProviderContext has already been initialized");
            }
            _dataContextFactory = dataContextFactory;
            ProviderConfigurations = providerConfigurations;
        }


        public IDataContext CreateDataContext(IDataConnection connection)
        {
            return _dataContextFactory(connection);
        }
    }
}