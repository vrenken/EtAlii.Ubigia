namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using System.Configuration;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Diagnostics;

    public class Program
    {
        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Starting Ubigia infrastructure...");

            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.Infrastructure");

            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Create a storage instance.
            var storageConfigurationSection = (IStorageConfigurationSection)exeConfiguration.GetSection("ubigia/storage");
            var storageConfiguration = storageConfigurationSection
                .ToStorageConfiguration();
            var storage = new StorageFactory().Create(storageConfiguration);

            // Fetch the Infrastructure configuration.
            var infrastructureConfigurationSection = (IInfrastructureConfigurationSection)exeConfiguration.GetSection("ubigia/infrastructure");
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
                .UseWebApiAdminApi()
                .UseWebApiAdminPortal()
                .UseWebApiUserApi()
                .UseWebApiUserPortal()
                .UseSignalRApi()
                .Use(logicalContext);
            var infrastructure = new InfrastructureFactory().Create(infrastructureConfiguration);

            // Create a host instance.
            var hostConfigurationSection = (IHostConfigurationSection)exeConfiguration.GetSection("ubigia/host");
            var hostConfiguration = hostConfigurationSection.ToHostConfiguration()
                .Use(infrastructure)
                .Use(storage)
                .Use<ConsoleHost>();
            var host = new HostFactory().Create(hostConfiguration);

            // Start hosting both the infrastructure and the storage.
            host.Start();

            Console.WriteLine("All OK. Ubigia is serving the storage specified below.");
            Console.WriteLine("Name: " + host.Infrastructure.Configuration.Name);
            Console.WriteLine("Address: " + host.Infrastructure.Configuration.Address);
            Console.WriteLine();
            Console.WriteLine("- Press any key to stop - ");
            Console.ReadKey();

            host.Stop();
        }
    }
}
