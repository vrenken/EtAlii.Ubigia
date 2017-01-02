namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    [TestClass]
    public class ScriptProcessor_Query_Temporal_IntegrationTests
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
        public async Task ScriptProcessor_Temporal_Downdate_01()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts/Doe/John <= { Nickname: 'Joe' }",
                "/Contacts/Doe/John <= { Nickname: 'John' }",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John{";

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
            dynamic person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(person);
            Assert.AreEqual("Joe", person.Nickname);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Temporal_Downdate_02()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts/Doe/John <= { Nickname: 'Joe' }",
                "/Contacts/Doe/John <= { Nickname: 'Johnny' }",
                "/Contacts/Doe/John <= { Nickname: 'John' }",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John{{";

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
            dynamic person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(person);
            Assert.AreEqual("Joe", person.Nickname);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Temporal_Downdate_03()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts/Doe/John <= { Nickname: 'Joe' }",
                "/Contacts/Doe/John <= { Nickname: 'Johnny' }",
                "/Contacts/Doe/John <= { Nickname: 'John' }",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John{";

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
            dynamic person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(person);
            Assert.AreEqual("Johnny", person.Nickname);
        }
    }
}