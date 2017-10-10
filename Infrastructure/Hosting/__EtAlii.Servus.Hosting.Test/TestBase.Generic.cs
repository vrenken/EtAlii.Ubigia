namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.Tests;
    using EtAlii.Servus.Storage;
    using EtAlii.Servus.Storage.InMemory;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class TestBase : EtAlii.Servus.Infrastructure.Hosting.Tests.TestBase<TestHost>
    {
        protected static TestHost CreateHost(
            IHostingConfiguration hostingConfiguration,
            IInfrastructureConfiguration infrastructureConfiguration,
            IStorageConfiguration storageConfiguration,
            IDiagnosticsConfiguration diagnostics)
        {
            return (TestHost)new TestHostFactory().Create<TestInfrastructureFactory, InMemoryStorageFactory>(hostingConfiguration, infrastructureConfiguration, storageConfiguration, diagnostics);
        }

    }
}
