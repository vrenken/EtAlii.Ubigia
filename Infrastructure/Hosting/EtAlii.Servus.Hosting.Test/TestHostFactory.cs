namespace EtAlii.Servus.Hosting.Tests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Tests;
    using EtAlii.Servus.Storage;
    using EtAlii.Servus.Storage.InMemory;

    public class TestHostFactory : HostFactoryBase<TestHost>
    {
        public TestHostFactory(IHostConfiguration configuration) 
            : base(configuration)
        {
        }

        public override IHost Create(
            IInfrastructureFactory infrastructureFactory, 
            IStorageFactory storageFactory,
            IDiagnosticsConfiguration diagnostics)
        {
            var scaffoldings = new IScaffolding[]
            {
                new TestHostScaffolding(),
                new TestHostProfilingScaffolding(diagnostics),
                new TestHostLoggingScaffolding(diagnostics),
                new TestHostDebuggingScaffolding(diagnostics),
            };
            return base.Create(infrastructureFactory, storageFactory, diagnostics, scaffoldings);
        }

        public IHost Create(
            IInfrastructureConfiguration infrastructureConfiguration,
            IStorageConfiguration storageConfiguration,
            IDiagnosticsConfiguration diagnostics)
        {

            var infrastructureFactory = new TestInfrastructureFactory(infrastructureConfiguration);
            var storageFactory = new InMemoryStorageFactory(storageConfiguration);

            return this.Create(infrastructureFactory, storageFactory, diagnostics);
        }
    }
}
