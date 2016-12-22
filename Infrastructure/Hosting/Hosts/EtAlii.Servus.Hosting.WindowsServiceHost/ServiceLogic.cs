namespace EtAlii.Servus.Infrastructure.Hosting.WindowsServiceHost
{
    using System;
    using System.Collections.Generic;
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

    public class ServiceLogic : IServiceLogic
    {
        private const string _nameFormat = "SIS${0}"; // Servus Infrastructure Service
        private const string _displayNameFormat = "Servus Infrastructure Service ({0})";
        private const string _descriptionFormat = "Provides applications access to the Servus storage '{0}'";
        

        // SERVINFSERV$SQLEXPRESS
        public string Name { get { return _name; } }
        private readonly string _name;

        public string DisplayName { get { return _displayName; } }
        private readonly string _displayName;

        public string Description { get { return _description; } }
        private readonly string _description;
        
        private readonly IHost _host;

        public ServiceLogic()
        {
            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Servus.Infrastructure"); // TODO: Diagnostics should be moved to each of the configuration sections.

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
                .Use<WindowsServiceHost>();
            _host = new HostFactory().Create(hostConfiguration);

            var infrastructureName = _host.Infrastructure.Configuration.Name;
            _name = String.Format(_nameFormat, infrastructureName).Replace(" ","_");
            _displayName = String.Format(_displayNameFormat, infrastructureName);
            _description = String.Format(_descriptionFormat, infrastructureName);
        }

        public void Start(IEnumerable<string> args)
        {
            Console.WriteLine("Starting Servus infrastructure...");

            _host.Start();

            Console.WriteLine("All OK. Servus is serving the storage specified below.");
            Console.WriteLine("Name: " + _host.Infrastructure.Configuration.Name);
            Console.WriteLine("Address: " + _host.Infrastructure.Configuration.Address);
        }

        public void Stop()
        {
            _host.Stop();
        }
    }
}
