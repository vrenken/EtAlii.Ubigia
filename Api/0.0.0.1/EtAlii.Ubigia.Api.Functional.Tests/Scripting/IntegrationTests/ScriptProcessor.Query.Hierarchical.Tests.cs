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
    public class ScriptProcessor_Query_Hierarchical_IntegrationTests
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
        public async Task ScriptProcessor_Query_Hierarchical_Parent_Child_Parent()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Does",
                "/Contacts/Does+=John",
                "/Contacts/Does/John <= { IsMale:true, Birthdate:1980-02-25, Weight:76.23 }"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Does/John\\";

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
            Assert.AreEqual(1, result.Count());
            dynamic first = result.First();

            Assert.AreEqual("Does", first.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Query_Hierarchical_Parent_Child_Parent_With_Condition_Bool()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Does",
                "/Contacts/Does+=John",
                "/Contacts/Does/John <= { IsMale: true, Birthdate:1980-02-25, Weight:76.23 }"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Does/.IsMale=true\\";

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
            Assert.AreEqual(1, result.Count());
            dynamic first = result.First();

            Assert.AreEqual("Does", first.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Query_Hierarchical_Parent_Child_Parent_With_Condition_DateTime()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Does",
                "/Contacts/Does+=John",
                "/Contacts/Does/John <= { IsMale: true, Birthdate: 1980-02-25, Weight: 76.23 }"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Does/.Birthdate=1980-02-25\\";

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
            Assert.AreEqual(1, result.Count());
            dynamic first = result.First();

            Assert.AreEqual("Does", first.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Query_Hierarchical_Parent_Child_Parent_With_Condition_Float()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Does",
                "/Contacts/Does+=John",
                "/Contacts/Does/John <= { IsMale: true, Birthdate: 1980-02-25, Weight: 76.23 }"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Does/.Weight=76.23\\";

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
            Assert.AreEqual(1, result.Count());
            dynamic first = result.First();

            Assert.AreEqual("Does", first.ToString());
        }
    }
}