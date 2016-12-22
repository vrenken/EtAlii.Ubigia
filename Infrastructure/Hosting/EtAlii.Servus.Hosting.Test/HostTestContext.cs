namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Fabric;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Infrastructure.Logical;
    using EtAlii.Servus.Infrastructure.Tests;
    using EtAlii.Servus.Infrastructure.WebApi.Portal.Admin;
    using EtAlii.Servus.Infrastructure.WebApi.Portal.User;
    using EtAlii.Servus.Storage;
    using EtAlii.Servus.Storage.InMemory;
    using EtAlii.xTechnology.Diagnostics;
    public sealed class HostTestContext : HostTestContext<TestHost>, IHostTestContext
    {
        public void Start()
        {
            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Servus.Infrastructure");
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
                .UseTestInfrastructure(diagnostics)
                .UseWebApiAdminPortal()
                .UseWebApiUserPortal()
                .Use(diagnostics)
                .Use(logicalContext);
            var infrastructure = new InfrastructureFactory().Create(infrastructureConfiguration);

            // Create a host instance.
            var hostConfiguration = new HostConfiguration()
                .UseTestHost(diagnostics)
                .Use(infrastructure)
                .Use(storage);
            var host = new HostFactory().Create(hostConfiguration);

            // Start hosting both the infrastructure and the storage.
            Start(host);
        }
    }
}
