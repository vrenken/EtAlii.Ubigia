namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class ScriptProcessor_Function_Rename
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private static ILogicalTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(async () =>
            {
                _testContext = new LogicalTestContextFactory().Create();
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
        public void Initialize()
        {
            var task = Task.Run(() =>
            {
                _diagnostics = TestDiagnostics.Create();
                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_diagnostics);
                _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            });
            task.Wait();
        }

        [TestCleanup]
        public void Cleanup()
        {
            var task = Task.Run(() =>
            {
                _parser = null;
            });
            task.Wait();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Rename_Path_01()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            const string query = "/Contacts += Doe/John\r\n$path <= /Contacts/Doe/John\r\nrename($path, 'Jane')";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.IsInstanceOfType(result, typeof(object[]));
            Assert.IsInstanceOfType(result[0], typeof(IReadOnlyEntry));
            Assert.AreEqual("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Rename_Path_02()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            const string query = "/Contacts += Doe/John\r\n$path <= /Contacts/Doe/John\r\nrename(/Contacts/Doe/John, 'Jane')";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.IsInstanceOfType(result, typeof(object[]));
            Assert.IsInstanceOfType(result[0], typeof(IReadOnlyEntry));
            Assert.AreEqual("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Rename_Path_03()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            const string query = "/Contacts += Doe/John\r\n$jane <= 'Jane'\r\n$path <= /Contacts/Doe/John\r\nrename(/Contacts/Doe/John, $jane)";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.IsInstanceOfType(result, typeof(object[]));
            Assert.IsInstanceOfType(result[0], typeof(IReadOnlyEntry));
            Assert.AreEqual("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Rename_Path_04()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            const string query = "/Contacts += Doe/John\r\n$jane <= 'Jane'\r\n$path <= /Contacts/Doe/John\r\nrename($path, $jane)";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.IsInstanceOfType(result, typeof(object[]));
            Assert.IsInstanceOfType(result[0], typeof(IReadOnlyEntry));
            Assert.AreEqual("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
        }

    }
}