//namespace EtAlii.Ubigia.Provisioning.Hosting
//[
//    using System
//    using EtAlii.xTechnology.Hosting

//    public static class HostConfigurationUseProvisioningExtension
//    [
//        public static IHostConfiguration UseProvisioning(this IHostConfiguration configuration, IProvisioning provisioning)
//        [
//            configuration.Use(new ProvisioningHostExtension(provisioning))

//            var services = new Type[]
//            [
//                typeof(IProvisioningService),
//            ]
//            configuration.Use(services)

//            return configuration
//        ]
//        //IHostConfiguration Use(IProviderConfiguration[] providerConfigurations)
//        //IHostConfiguration Use(string address, string account, string password)

//        //IHostConfiguration Use(Action<IManagementConnectionConfiguration> managementConnectionConfigurationFactoryExtension)
//        //IHostConfiguration Use(Action<IDataConnectionConfiguration> dataConnectionConfigurationFactoryExtension)
//        //IHostConfiguration Use(Action<IDataContextConfiguration> dataContextConfigurationFactoryExtension)

//    ]
//]