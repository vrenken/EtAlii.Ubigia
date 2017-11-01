namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;

    public class ServiceLogic : IServiceLogic
    {
        private const string _nameFormat = "SIS${0}"; // Ubigia Infrastructure Service
        private const string _displayNameFormat = "Ubigia Infrastructure Service ({0})";
        private const string _descriptionFormat = "Provides applications access to the Ubigia storage '{0}'";
        

        // SERVINFSERV$SQLEXPRESS
        public string Name { get; }

        public string DisplayName { get; }

        public string Description { get; }

        private readonly IHost _host;

        public ServiceLogic()
        {
            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.Infrastructure"); // TODO: Diagnostics should be moved to each of the configuration sections.

            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Create a storage instance.
            var storageConfigurationSection = (IStorageConfigurationSection)exeConfiguration.GetSection("ubigia/storage");
            var storageConfiguration = storageConfigurationSection.ToStorageConfiguration();
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
                .UseInfrastructure(storage, infrastructure);
            _host = new HostFactory<WindowsServiceHost>().Create(hostConfiguration);

            var infrastructureName = infrastructure.Configuration.Name;
            Name = String.Format(_nameFormat, infrastructureName).Replace(" ","_");
            DisplayName = String.Format(_displayNameFormat, infrastructureName);
            Description = String.Format(_descriptionFormat, infrastructureName);
        }

        public void Start(IEnumerable<string> args)
        {
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
        }
    }
}
