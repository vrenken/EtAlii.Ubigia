namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Ubigia.Api.Tests.TestAssembly;

    [TestClass]
    public class ScriptProcessor_Assign_Dynamic_IntegrationTests
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
        public async Task ScriptProcessor_Assign_Dynamic_To_Path()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time+=/2014",
                "/Time/2014+=/09",
                "/Time/2014/09+=/06",
                "/Time/2014/09/06+=/16",
                "<= /Time/2014/09/06/16+=/33"
            };

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= $data");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence  = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(12, selectResult.IntValue);
            //Assert.AreNotEqual(addResult.Id, updateResult.Id);
            //Assert.AreNotEqual(updateResult.Id, selectResult.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Empty()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time+=/2014",
                "/Time/2014+=/09",
                "/Time/2014/09+=/06",
                "/Time/2014/09/06+=/16",
                "<= /Time/2014/09/06/16+=/33"
            };

            dynamic data = new
            {
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= $data");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time += /2014",
                "/Time/2014 += /09",
                "/Time/2014/09 += /06",
                "/Time/2014/09/06 += /16",
                "<= /Time/2014/09/06/16 += /33"
            };

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= $data");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(12, selectResult.IntValue);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced_With_DateTime_Local()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time += /2014",
                "/Time/2014 += /09",
                "/Time/2014/09 += /06",
                "/Time/2014/09/06 += /16",
                "<= /Time/2014/09/06/16 += /33"
            };

            var dateTime = new DateTime(2016, 04, 10, 21, 21, 04, DateTimeKind.Local);

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
                DateTime = dateTime,
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= $data");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(12, selectResult.IntValue);
            Assert.AreEqual(dateTime, selectResult.DateTime);
            Assert.AreEqual(dateTime.Kind, selectResult.DateTime.Kind, "Kind");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced_With_DateTime_Utc()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time += /2014",
                "/Time/2014 += /09",
                "/Time/2014/09 += /06",
                "/Time/2014/09/06 += /16",
                "<= /Time/2014/09/06/16 += /33"
            };

            var dateTime = new DateTime(2016, 04, 10, 21, 21, 04, DateTimeKind.Utc);

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
                DateTime = dateTime,
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= $data");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(12, selectResult.IntValue);
            Assert.AreEqual(dateTime, selectResult.DateTime);
            Assert.AreEqual(dateTime.Kind, selectResult.DateTime.Kind, "Kind");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced_With_DateTime_Unspecified()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time += /2014",
                "/Time/2014 += /09",
                "/Time/2014/09 += /06",
                "/Time/2014/09/06 += /16",
                "<= /Time/2014/09/06/16 += /33"
            };

            var dateTime = new DateTime(2016, 04, 10, 21, 21, 04, DateTimeKind.Unspecified);

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
                DateTime = dateTime,
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= $data");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(12, selectResult.IntValue);
            Assert.AreEqual(dateTime, selectResult.DateTime);
            Assert.AreEqual(dateTime.Kind, selectResult.DateTime.Kind, "Kind");
        }
    }
}