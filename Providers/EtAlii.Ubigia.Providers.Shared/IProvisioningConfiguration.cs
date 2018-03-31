namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface IProvisioningConfiguration
    {
        IProvisioningExtension[] Extensions { get; }

        IProviderConfiguration[] ProviderConfigurations { get; }
        IProvisioningConfiguration Use(Uri address, string account, string password);

        IProvisioningConfiguration Use(IProvisioningExtension[] extensions);

        IProvisioningConfiguration Use(IProviderConfiguration[] providerConfigurations);

        string Account { get; }
        string Password { get; }
        Uri Address { get; }

        IStorageTransportProvider CreateStorageTransportProvider();
        ITransportProvider CreateTransportProvider();

        IDataConnectionConfiguration CreateDataConnectionConfiguration();
        IManagementConnectionConfiguration CreateManagementConnectionConfiguration();
        IDataContext CreateDataContext(IDataConnection connection, bool useCaching = true);

        IProvisioningConfiguration Use(Action<IManagementConnectionConfiguration> managementConnectionConfigurationFactoryExtension);
        IProvisioningConfiguration Use(Action<IDataConnectionConfiguration> dataConnectionConfigurationFactoryExtension);
        IProvisioningConfiguration Use(Action<IDataContextConfiguration> dataContextConfigurationFactoryExtension);
        IProvisioningConfiguration Use(Func<IStorageTransportProvider> storageTransportProviderFactory);
        IProvisioningConfiguration Use(Func<ITransportProvider> transportProviderFactory);

    }
}