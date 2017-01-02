namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScriptProcessor_Function_Count_Tests
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private static ILogicalTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(() =>
            {
            });
            task.Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var task = Task.Run(() =>
            {
            });
            task.Wait();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var task = Task.Run(async () =>
            {
                _testContext = new LogicalTestContextFactory().Create();
                await _testContext.Start();

                _diagnostics = TestDiagnostics.Create();
                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_diagnostics);
                _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            });
            task.Wait();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var task = Task.Run(async () =>
            {
                _parser = null;

                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Count_01()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "<= Count() <= /Contacts/Doe/*";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var personsAfter = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(personsAfter);
            Assert.AreEqual(1, personsAfter.Count());
            Assert.AreEqual(3, personsAfter.Single());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Count_02()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "<= Count(/Contacts/Doe/*)";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var personsAfter = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(personsAfter);
            Assert.AreEqual(1, personsAfter.Count());
            Assert.AreEqual(3, personsAfter.Single());
        }
        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Count_03()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "$var1 <= /Contacts/Doe/*\r\n<= Count($var1)";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var personsAfter = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(personsAfter);
            Assert.AreEqual(1, personsAfter.Count());
            Assert.AreEqual(3, personsAfter.Single());
        }
    }
}