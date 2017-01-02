namespace EtAlii.Servus.Api.Fabric.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport.Tests;
    using EtAlii.Servus.Storage.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    [TestClass]
    public class FabricContext_Properties_Tests
    {
        private IFabricContext _fabric;
        private static ITransportTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(async () =>
            {
                _testContext = new TransportTestContextFactory().Create();
                await _testContext.Start();
            });
            task.Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var task = Task.Run(async () =>
            {
                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var task = Task.Run(async () =>
            {
                var connection = await _testContext.CreateDataConnection(true);
                var fabricContextConfiguration = new FabricContextConfiguration()
                    .Use(connection);
                _fabric = new FabricContextFactory().Create(fabricContextConfiguration);
            });
            task.Wait();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var task = Task.Run(() =>
            {
                _fabric.Dispose();
                _fabric = null;
            });
            task.Wait();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Properties_Store()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var properties = TestProperties.Create();

            // Act.
            await _fabric.Properties.Store(entry.Id, properties);

            // Assert.
            Assert.IsTrue(properties.Stored); 
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Properties_Retrieve()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            var entry = await _fabric.Entries.Get(root.Identifier, scope);
            var properties = TestProperties.CreateComplete();
            await _fabric.Properties.Store(entry.Id, properties);

            // Act.
            var retrievedProperties = await _fabric.Properties.Retrieve(entry.Id, scope);

            // Assert.
            AssertData.AreEqual(properties, retrievedProperties);
            Assert.AreEqual(true, retrievedProperties.Stored);
            Assert.AreEqual(true, properties.Stored);
        }
    }
}
