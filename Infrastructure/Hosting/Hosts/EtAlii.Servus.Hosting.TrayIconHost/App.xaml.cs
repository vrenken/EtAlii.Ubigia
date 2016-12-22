namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System.Configuration;
    using System.Windows;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Fabric;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Infrastructure.Logical;
    using EtAlii.Servus.Infrastructure.WebApi;
    using EtAlii.Servus.Infrastructure.WebApi.Portal.Admin;
    using EtAlii.Servus.Infrastructure.WebApi.Portal.User;
    using EtAlii.Servus.Storage;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            var name = "EtAlii";
            var category = "EtAlii.Servus.Infrastructure";
            var diagnostics = new DiagnosticsFactory().Create(true, false, true,
                () => new LogFactory(),
                () => new ProfilerFactory(),
                (factory) => factory.Create(name, category),
                (factory) => factory.Create(name, category));
            //var diagnostics = new DiagnosticsFactory().CreateDisabled(name, category);

            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Create a storage instance.
            var storageConfigurationSection = (IStorageConfigurationSection)exeConfiguration.GetSection("servus/storage");
            var storageConfiguration = storageConfigurationSection.ToStorageConfiguration();
            var storage = new StorageFactory().Create(storageConfiguration);

            // Fetch the Infrastructure configuration.
            var infrastructureConfigurationSection = (IInfrastructureConfigurationSection)exeConfiguration.GetSection("servus/infrastructure");
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
                .UseWebApi(diagnostics) // TODO: Web API usage should also be configured in the configuration section.
                .UseWebApiAdminPortal()
                .UseWebApiUserPortal()
                .UseSignalR()
                .Use(logicalContext);
            var infrastructure = new InfrastructureFactory().Create(infrastructureConfiguration);

            // Create a host instance.
            var hostConfigurationSection = (IHostConfigurationSection)exeConfiguration.GetSection("servus/host");
            var hostConfiguration = hostConfigurationSection.ToHostConfiguration()
                .Use(infrastructure)
                .Use(storage)
                .Use<TrayIconHost>();
            var host = new HostFactory().Create(hostConfiguration);

            // Start hosting both the infrastructure and the storage.
            host.Start();
        }
    }
}
