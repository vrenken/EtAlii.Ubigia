namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    [TestClass]
    public class ScriptProcessor_Get_Tests
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
        public async Task ScriptProcessor_Get_GetItem()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);

            const string query = "/Time";
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
            Assert.IsNotNull(script);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Get_GetItemByVariable_1()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var queries = new[]
            {
                "$var1 <= /Time",
                "$var1"
            };

            var script = _parser.Parse(queries).Script;
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
            Assert.IsInstanceOfType(result.Single(), typeof(DynamicNode));
            Assert.AreEqual("Time", result.Cast<INode>().Single().Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Get_GetItemByVariable_2()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var queries = new[]
            {
                "$var1 <= /\"Time\"",
                "$var1"
            };

            var script = _parser.Parse(queries).Script;
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
            Assert.IsInstanceOfType(result.Single(), typeof(DynamicNode));
            Assert.AreEqual("Time", result.Cast<INode>().Single().Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Get_GetItemByVariables_1()
        {
            // Arrange.
            var now = DateTime.Now;
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var queries = new[]
            {
                "/Time+=/{0:yyyy}",
                "$var1 <= Time",
                "$var2 <= {0:yyyy}",
                "/$var1/$var2"
            };

            var query = String.Format(String.Join("\r\n", queries), now);
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
            Assert.IsInstanceOfType(result, typeof(IEnumerable<object>));
            Assert.AreEqual(string.Format("{0:yyyy}", now), result.Cast<INode>().Single().Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Get_GetItemByVariables_2()
        {
            // Arrange.
            var now = DateTime.Now;
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var queries = new[]
            {
                "/Time+=/{0:yyyy}",
                "$var1 <= \"Time\"",
                "$var2 <= \"{0:yyyy}\"",
                "/$var1/$var2"
            };

            var query = String.Format(String.Join("\r\n", queries), now);
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
            Assert.IsInstanceOfType(result, typeof(IEnumerable<object>));
            Assert.AreEqual(string.Format("{0:yyyy}", now), result.Cast<INode>().Single().Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Get_GetItemByVariables_Spaced()
        {
            // Arrange.
            var now = DateTime.Now;
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var queries = new[]
            {
                "/Time += /{0:yyyy}",
                "$var1 <= \"Time\"",
                "$var2 <= \"{0:yyyy}\"",
                "/$var1/$var2"
            };

            var query = String.Format(String.Join("\r\n", queries), now);
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
            Assert.IsInstanceOfType(result, typeof(IEnumerable<object>));
            Assert.AreEqual(string.Format("{0:yyyy}", now), result.Cast<INode>().Single().Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Get_GetItemByCompositeVariable()
        {
            // Arrange.
            var now = DateTime.Now;
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var queries = new[]
            {
                "/Time+=/{0:yyyy}",
                "$var1 <= \"Time/{0:yyyy}\"",
                "/$var1"
            };

            var query = String.Format(String.Join("\r\n", queries), now);
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
            Assert.IsInstanceOfType(result, typeof(IEnumerable<object>));
            Assert.AreEqual(String.Format("{0:yyyy}", now), result.Cast<INode>().Single().Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Get_GetItemByCompositeVariable_Spaced()
        {
            // Arrange.
            var now = DateTime.Now;
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var queries = new[]
            {
                "/Time += /{0:yyyy}",
                "$var1 <= \"Time/{0:yyyy}\"",
                "/$var1"
            };

            var query = String.Format(String.Join("\r\n", queries), now);
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
            Assert.IsInstanceOfType(result, typeof(IEnumerable<object>));
            Assert.AreEqual(String.Format("{0:yyyy}", now), result.Cast<INode>().Single().Type);
        }
    }
}