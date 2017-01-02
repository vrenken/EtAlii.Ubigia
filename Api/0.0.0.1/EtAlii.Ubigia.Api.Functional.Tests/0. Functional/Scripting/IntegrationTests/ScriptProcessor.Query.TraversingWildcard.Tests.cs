namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Ubigia.Api.Tests.TestAssembly;

    [TestClass]
    public class ScriptProcessor_Query_TraversingWildcard_IntegrationTests
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
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
        public void TestInitialize()
        {
            var task = Task.Run(async () =>
            {
                _diagnostics = TestDiagnostics.Create();
                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_diagnostics);
                _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
                _logicalContext = await _testContext.CreateLogicalContext(true);
            });
            task.Wait();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var task = Task.Run(() =>
            {
                _parser = null;
                _logicalContext.Dispose();
                _logicalContext = null;
            });
            task.Wait();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_TraversingWildcard_01()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Joe",
                "/Contacts+=Doe/Johnny",
                "/Contacts+=Dane/John",
                "/Contacts+=Dane/Jane",
                "/Contacts+=Dane/Joe",
                "/Contacts+=Dane/Johnny",
                "/Contacts+=Dee/The/John",
                "/Contacts+=Dee/The/Jane",
                "/Contacts+=Dee/The/Joe",
                "/Contacts+=Dee/The/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/*3*/John";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            dynamic person1 = result.Skip(0).First();
            dynamic person2 = result.Skip(1).First();
            dynamic person3 = result.Skip(2).First();
            Assert.AreEqual("John", person1.ToString());
            Assert.AreEqual("John", person2.ToString());
            Assert.AreEqual("John", person3.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_TraversingWildcard_02()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Joe",
                "/Contacts+=Doe/Johnny",
                "/Contacts+=Dane/John",
                "/Contacts+=Dane/Jane",
                "/Contacts+=Dane/Joe",
                "/Contacts+=Dane/Johnny",
                "/Contacts+=Dee/The/John",
                "/Contacts+=Dee/The/Jane",
                "/Contacts+=Dee/The/Joe",
                "/Contacts+=Dee/The/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/*2*/Jo*";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(9, result.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_TraversingWildcard_03()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Joe",
                "/Contacts+=Doe/Johnny",
                "/Contacts+=Dane/John",
                "/Contacts+=Dane/Jane",
                "/Contacts+=Dane/Joe",
                "/Contacts+=Dane/Johnny",
                "/Contacts+=Dee/The/John",
                "/Contacts+=Dee/The/Jane",
                "/Contacts+=Dee/The/Joe",
                "/Contacts+=Dee/The/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/*2*";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(12, result.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_TraversingWildcard_04()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Joe",
                "/Contacts+=Doe/Johnny",
                "/Contacts+=Dane/John",
                "/Contacts+=Dane/Jane",
                "/Contacts+=Dane/Joe",
                "/Contacts+=Dane/Johnny",
                "/Contacts+=Dee/The/John",
                "/Contacts+=Dee/The/Jane",
                "/Contacts+=Dee/The/Joe",
                "/Contacts+=Dee/The/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/*3*";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(16, result.Count());
        }
    }
}