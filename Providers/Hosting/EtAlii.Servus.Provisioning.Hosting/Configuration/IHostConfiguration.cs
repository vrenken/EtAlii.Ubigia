namespace EtAlii.Servus.Provisioning.Hosting
{
    using System;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;

    public interface IHostConfiguration 
    {
        IHostExtension[] Extensions { get; }

        IProviderConfiguration[] ProviderConfigurations { get; }
        IHostConfiguration  Use(string address, string account, string password);

        IHostConfiguration Use(IHostExtension[] extensions);

        IHostConfiguration Use(IProviderConfiguration[] providerConfigurations);

        string Account { get; }
        string Password { get; }
        string Address { get; }

        IDataConnectionConfiguration CreateDataConnectionConfiguration();
        IManagementConnectionConfiguration CreateManagementConnectionConfiguration();
        IDataContext CreateDataContext(IDataConnection connection, bool useCaching = true);

        IHostConfiguration Use(Action<IManagementConnectionConfiguration> managementConnectionConfigurationFactoryExtension);
        IHostConfiguration Use(Action<IDataConnectionConfiguration> dataConnectionConfigurationFactoryExtension);
        IHostConfiguration Use(Action<IDataContextConfiguration> dataContextConfigurationFactoryExtension);
    }
}