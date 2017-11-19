//namespace EtAlii.Ubigia.Provisioning.Hosting
//{
//    using System;
//    using EtAlii.Ubigia.Api.Functional;
//    using EtAlii.Ubigia.Api.Transport;
//    using EtAlii.Ubigia.Api.Transport.Management;

//    public interface IHostConfiguration 
//    {
//        IHostExtension[] Extensions { get; }

//        IProviderConfiguration[] ProviderConfigurations { get; }
//        IHostConfiguration  Use(string address, string account, string password);

//        IHostConfiguration Use(IHostExtension[] extensions);

//        IHostConfiguration Use(IProviderConfiguration[] providerConfigurations);

//        string Account { get; }
//        string Password { get; }
//        string Address { get; }

//        IDataConnectionConfiguration CreateDataConnectionConfiguration();
//        IManagementConnectionConfiguration CreateManagementConnectionConfiguration();
//        IDataContext CreateDataContext(IDataConnection connection, bool useCaching = true);

//        IHostConfiguration Use(Action<IManagementConnectionConfiguration> managementConnectionConfigurationFactoryExtension);
//        IHostConfiguration Use(Action<IDataConnectionConfiguration> dataConnectionConfigurationFactoryExtension);
//        IHostConfiguration Use(Action<IDataContextConfiguration> dataContextConfigurationFactoryExtension);
//    }
//}