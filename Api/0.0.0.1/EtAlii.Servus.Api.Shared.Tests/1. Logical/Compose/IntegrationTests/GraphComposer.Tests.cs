namespace EtAlii.Servus.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Fabric.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class GraphComposer_IntegrationTests
    {
        private IDiagnosticsConfiguration _diagnostics;
        private static IFabricTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(async () =>
            {
                _testContext = new FabricTestContextFactory().Create();
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
            var task = Task.Run(() =>
            {
                _diagnostics = TestDiagnostics.Create();
            });
            task.Wait();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var task = Task.Run(() =>
            {
                _diagnostics = null;
            });
            task.Wait();
        }

        [TestMethod]
        public async Task GraphComposer_Create()
        {
            // Arrange.
            var fabric = await _testContext.CreateFabricContext(true);
            var traverserFactory = new GraphPathTraverserFactory();

            // Act.
            var composer = new GraphComposerFactory(traverserFactory).Create(fabric);

            // Assert.
            Assert.IsNotNull(composer);
        }
    }
}