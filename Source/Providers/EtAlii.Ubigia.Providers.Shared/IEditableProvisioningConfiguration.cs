namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface IEditableProvisioningConfiguration
    {
        ProviderConfiguration[] ProviderConfigurations { get; set; }

        Uri Address { get; set; }

        string Account { get; set; }

        string Password { get; set; }

        Action<ManagementConnectionConfiguration>[] ManagementConnectionConfigurationFactoryExtensions { get; set; }
        Action<DataConnectionConfiguration>[] DataConnectionConfigurationFactoryExtensions { get; set; }
        Action<GraphSLScriptContextConfiguration>[] ScriptContextConfigurationFactoryExtensions { get; set; }
        
        Func<ITransportProvider> TransportProviderFactory { get; set; }
        Func<IStorageTransportProvider> StorageTransportProviderFactory { get; set; }
    }
}