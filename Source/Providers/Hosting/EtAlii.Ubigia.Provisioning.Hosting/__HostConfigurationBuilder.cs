//namespace EtAlii.Ubigia.Provisioning.Hosting
//[
//    using System
//    using EtAlii.Ubigia.Api.Transport.Management.SignalR
//    using EtAlii.Ubigia.Api.Transport.SignalR
//    using EtAlii.xTechnology.Diagnostics
//    using EtAlii.xTechnology.Hosting

//    public class HostConfigurationBuilder 
//    [
//        public IHostConfiguration Build(Func<string, object> getConfigurationSection)
//        [
//            var name = "EtAlii"
//            var category = "EtAlii.Ubigia.Provisioning"
//            //var diagnostics = new DiagnosticsFactory().Create(true, false, true,
//            //    () => new LogFactory(),
//            //    () => new ProfilerFactory(),
//            //    (factory) => factory.Create(name, category),
//            //    (factory) => factory.Create(name, category))
//            var diagnostics = new DiagnosticsFactory().CreateDisabled(name, category)

//            // Let's first fetch the provider configurations.
//            var providerConfigurationsSection = (IProviderConfigurationsSection)getConfigurationSection("ubigia/providers")
//            var providerConfigurations = providerConfigurationsSection
//                .ToProviderConfigurations()
            
//            // Create a provisioning instance.
//            var provisioningConfigurationSection = (IProvisioningConfigurationSection)getConfigurationSection("ubigia/provisioning")
//            var provisioningConfiguration = provisioningConfigurationSection
//                .ToProvisioningConfiguration()
//                .Use(() => SignalRTransportProvider.Create())
//                .Use(() => SignalRStorageTransportProvider.Create())

//            var provisioning = new ProvisioningFactory().Create(provisioningConfiguration)
//                //.Use(providerConfigurations)
//                //.UseProvisioning(providerConfigurations)
//                //.Use(diagnostics)

//            var hostCommands = new HostCommandsFactory().Create()

//            // Create a host instance.
//            var hostConfigurationSection = (IHostConfigurationSection)getConfigurationSection("ubigia/host")
//            var hostConfiguration = hostConfigurationSection.ToHostConfiguration()
//                .UseProvisioning(provisioning)
//                .Use(hostCommands)

//            return hostConfiguration
//        ]
//    ]
//]