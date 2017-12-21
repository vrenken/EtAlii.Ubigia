namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using System;
    using EtAlii.xTechnology.Hosting.Owin;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User;
    using EtAlii.xTechnology.Diagnostics;
    using Functional;
    using Storage;
    using xTechnology.Hosting;

    public class HostConfigurationBuilder 
    {
        public IHostConfiguration Build(
            Func<string, object> getConfigurationSection,
            IApplicationManager applicationManager = null)
        {
            return Build(
                getConfigurationSection, 
                out IInfrastructure _,
                applicationManager);
        }

        public IHostConfiguration Build(
            Func<string, object> getConfigurationSection, 
            out IInfrastructure infrastructure,
            IApplicationManager applicationManager = null)
        {
            var name = "EtAlii";
            var category = "EtAlii.Ubigia.Infrastructure";
            //var diagnostics = new DiagnosticsFactory().Create(true, false, true,
            //    () => new LogFactory(),
            //    () => new ProfilerFactory(),
            //    (factory) => factory.Create(name, category),
            //    (factory) => factory.Create(name, category));
            var diagnostics = new DiagnosticsFactory().CreateDisabled(name, category);


            // Create a storage instance.
            var storageConfigurationSection = (IStorageConfigurationSection)getConfigurationSection("ubigia/storage");
            var storageConfiguration = storageConfigurationSection
                .ToStorageConfiguration();
            var storage = new StorageFactory().Create(storageConfiguration);

            // Fetch the Infrastructure configuration.
            var infrastructureConfigurationSection = (IInfrastructureConfigurationSection)getConfigurationSection("ubigia/infrastructure");
            var infrastructureConfiguration = infrastructureConfigurationSection.ToInfrastructureConfiguration();

            // Create fabric instance.
            var fabricConfiguration = new FabricContextConfiguration()
                .Use(storage);
            var fabric = new FabricContextFactory().Create(fabricConfiguration);

            // Create logical context instance.
            var logicalConfiguration = new LogicalContextConfiguration()
                .Use(fabric)
                .Use(infrastructureConfiguration.Name, infrastructureConfiguration.Address);
            var logicalContext = new LogicalContextFactory().Create(logicalConfiguration);

            // Create a Infrastructure instance.
            infrastructureConfiguration = infrastructureConfiguration
                .UseOwin(applicationManager)
                .UseWebApi(diagnostics) // TODO: Web API usage should also be configured in the configuration section.
                .UseWebApiAdminApi()
                .UseWebApiAdminPortal()
                .UseWebApiUserApi()
                .UseWebApiUserPortal()
                .UseSignalRApi()
                .Use(logicalContext);
            infrastructure = new InfrastructureFactory().Create(infrastructureConfiguration);

            var hostCommands = new HostCommandsFactory().Create(infrastructure);
            
            // Create a host instance.
            var hostConfigurationSection = (IHostConfigurationSection)getConfigurationSection("ubigia/host");
            var hostConfiguration = hostConfigurationSection.ToHostConfiguration()
                .UseInfrastructure(storage, infrastructure)
                .Use(hostCommands);

            return hostConfiguration;
        }
    }
}
