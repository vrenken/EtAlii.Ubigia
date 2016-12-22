namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System;
    using System.Configuration;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Fabric;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Infrastructure.Logical;
    using EtAlii.Servus.Infrastructure.WebApi;
    using EtAlii.Servus.Infrastructure.WebApi.Portal.Admin;
    using EtAlii.Servus.Infrastructure.WebApi.Portal.User;
    using EtAlii.Servus.Storage;
    using EtAlii.xTechnology.Diagnostics;

    public class Program
    {
        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Starting Servus infrastructure...");

            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Servus.Infrastructure");

            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Create a storage instance.
            var storageConfigurationSection = (IStorageConfigurationSection)exeConfiguration.GetSection("servus/storage");
            var storageConfiguration = storageConfigurationSection
                .ToStorageConfiguration();
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
                .Use<ConsoleHost>();
            var host = new HostFactory().Create(hostConfiguration);

            // Start hosting both the infrastructure and the storage.
            host.Start();

            Console.WriteLine("All OK. Servus is serving the storage specified below.");
            Console.WriteLine("Name: " + host.Infrastructure.Configuration.Name);
            Console.WriteLine("Address: " + host.Infrastructure.Configuration.Address);
            Console.WriteLine();
            Console.WriteLine("- Press any key to stop - ");
            Console.ReadKey();

            host.Stop();
        }
    }
}
