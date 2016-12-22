namespace EtAlii.Servus.Provisioning
{
    using System;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;

    public interface IProvidersContext
    {
        IDataContext SystemDataContext { get; }
        IManagementConnection ManagementConnection { get; }
        IProviderConfiguration[] ProviderConfigurations { get; }

        void Initialize(IProviderConfiguration[] providerConfigurations, Func<IDataConnection, IDataContext> dataContextFactory);

        IDataContext CreateDataContext(IDataConnection connection);
    }
}