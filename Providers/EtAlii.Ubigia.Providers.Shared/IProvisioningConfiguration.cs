namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface IProvisioningConfiguration : IConfiguration<ProvisioningConfiguration>
    {

        IProviderConfiguration[] ProviderConfigurations { get; }

        string Account { get; }
        string Password { get; }
        Uri Address { get; }

        IStorageTransportProvider CreateStorageTransportProvider();
        ITransportProvider CreateTransportProvider();

        IDataConnectionConfiguration CreateDataConnectionConfiguration();
        IManagementConnectionConfiguration CreateManagementConnectionConfiguration();
        IGraphSLScriptContext CreateScriptContext(IDataConnection connection, bool useCaching = true);
    }
}