namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.Tests;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User;
    using EtAlii.Ubigia.Storage;
    using EtAlii.Ubigia.Storage.InMemory;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;

    public sealed class HostTestContext : HostTestContext<TestHost>, IHostTestContext
    {
        public void Start()
        {
            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.Infrastructure");
            //diagnostics.EnableLogging = true;

            // Create a Storage instance.
            var storageConfiguration = TestStorageConfiguration.Create()
                .UseInMemoryStorage();
            var storage = new StorageFactory().Create(storageConfiguration);

            // Fetch the Infrastructure configuration.
            var infrastructureConfiguration = TestInfrastructureConfiguration.Create();

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
                .UseOwin<TestInfrastructure>()
                .UseSignalRTestApi(diagnostics)
                .UseWebApiAdminApi()
                .UseWebApiAdminPortal()
                .UseWebApiUserApi()
                .UseWebApiUserPortal()
                .Use(diagnostics)
                .Use(logicalContext);
            var infrastructure = new InfrastructureFactory().Create(infrastructureConfiguration);

            // Create a host instance.
            var hostConfiguration = new HostConfiguration()
                .UseTestHost(diagnostics)
				.Use(new InfrastructureHostExtension(storage, infrastructure));
                //.UseInfrastructure(storage, infrastructure);
	        
			var host = new HostFactory<TestHost>().Create(hostConfiguration);

            // Start hosting both the infrastructure and the storage.
            Start(host, infrastructure);
        }
    }
}
