using System.Web.Configuration;
using EtAlii.Ubigia.Infrastructure.Hosting;
using EtAlii.Ubigia.Storage;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User;
    using EtAlii.xTechnology.Diagnostics;
    using global::Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder application)
        {
            var applicationManager = new WebsiteApplicationManager(application);

            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.Infrastructure"); // TODO: Diagnostics should be moved to each of the configuration sections.

            // Create a storage instance.
            var storageConfigurationSection = (IStorageConfigurationSection)WebConfigurationManager.GetWebApplicationSection("ubigia/storage");
            var storageConfiguration = storageConfigurationSection.ToStorageConfiguration();
            var storage = new StorageFactory().Create(storageConfiguration);

            // Fetch the Infrastructure configuration.
            var infrastructureConfigurationSection = (IInfrastructureConfigurationSection)WebConfigurationManager.GetWebApplicationSection("ubigia/infrastructure");
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
                .UseWebApi(diagnostics, applicationManager) // TODO: Web API usage should also be configured in the configuration section.
                .UseWebApiAdminApi()
                .UseWebApiAdminPortal()
                .UseWebApiUserApi()
                .UseWebApiUserPortal()
                .UseSignalR()
                .Use(logicalContext);
            var infrastructure = new InfrastructureFactory().Create(infrastructureConfiguration);

            // Create a host instance.
            var hostConfigurationSection = (IHostConfigurationSection)WebConfigurationManager.GetWebApplicationSection("ubigia/host");
            var hostConfiguration = hostConfigurationSection.ToHostConfiguration()
                .Use(infrastructure)
                .Use(storage)
                .Use<WebsiteHost>();
            var host = new HostFactory().Create(hostConfiguration);

            // Start hosting both the infrastructure and the storage.
            host.Start();

        }
    }
}
