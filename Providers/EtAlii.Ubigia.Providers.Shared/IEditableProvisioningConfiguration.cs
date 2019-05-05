namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface IEditableProvisioningConfiguration
    {
        IProviderConfiguration[] ProviderConfigurations { get; set; }

        Uri Address { get; set; }

        string Account { get; set; }

        string Password { get; set; }

        Action<IManagementConnectionConfiguration>[] ManagementConnectionConfigurationFactoryExtensions { get; set; }
        Action<IDataConnectionConfiguration>[] DataConnectionConfigurationFactoryExtensions { get; set; }
        Action<IGraphSLScriptContextConfiguration>[] ScriptContextConfigurationFactoryExtensions { get; set; }
        
        Func<ITransportProvider> TransportProviderFactory { get; set; }
        Func<IStorageTransportProvider> StorageTransportProviderFactory { get; set; }
    }
}