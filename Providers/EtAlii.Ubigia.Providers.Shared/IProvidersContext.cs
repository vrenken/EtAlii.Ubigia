namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface IProvidersContext
    {
        IDataContext SystemDataContext { get; }
        IManagementConnection ManagementConnection { get; }
        IProviderConfiguration[] ProviderConfigurations { get; }

        void Initialize(IProviderConfiguration[] providerConfigurations, Func<IDataConnection, IDataContext> dataContextFactory);

        IDataContext CreateDataContext(IDataConnection connection);
    }
}