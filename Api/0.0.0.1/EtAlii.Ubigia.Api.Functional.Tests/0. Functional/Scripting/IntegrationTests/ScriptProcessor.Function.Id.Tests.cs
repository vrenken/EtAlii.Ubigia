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

    [TestClass]
    public partial class ScriptProcessor_Function_Id
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
        public async Task ScriptProcessor_Function_Id_Assign()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            const string query = "<= id() <= /Contacts";
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
            Assert.IsInstanceOfType(result.Single(), typeof(Identifier));
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Variable_Path_01()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            const string query = "/Contacts += Doe/John\r\n$path <= /Contacts/Doe/John\r\nid($path)";
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
            Assert.IsInstanceOfType(result.Single(), typeof(Identifier));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Variable_Path_02()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            const string query = "$path <= /Contacts\r\nid($path)";
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
            Assert.IsInstanceOfType(result.Single(), typeof(Identifier));
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Variable_Path_Variable()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            const string query = "$path <= /Contacts\r\n$id <= id($path)\r\n$id";
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
            Assert.IsInstanceOfType(result.Single(), typeof(Identifier));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Path_01()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            const string query = "id(/Contacts)";
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
            Assert.IsInstanceOfType(result.Single(), typeof(Identifier));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Path_02()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            const string query = "$var1 <= /Contacts += /Doe/Jane\r\n$var2 <= id(/Contacts/Doe/Jane)";
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
            Assert.AreEqual(0, result.Length);  // << Question: should the $var2 assignment create output or not? Nope it should not. only assign (<=), function or path results should.
            Assert.IsInstanceOfType(await scope.Variables["var1"].Value.SingleAsync(), typeof(INode));
            Assert.IsInstanceOfType(await scope.Variables["var2"].Value.SingleAsync(), typeof(Identifier));
            Assert.AreEqual((await scope.Variables["var1"].Value.Cast<INode>().SingleAsync()).Id, await scope.Variables["var2"].Value.Cast<Identifier>().SingleAsync());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Path_03()
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
            var selectQuery = "<= Id(/Contacts/Doe/*)";

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
            Assert.AreEqual(3, personsAfter.Count());
        }
        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Path_04()
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
            var selectQuery = "$var1 <= /Contacts/Doe/*\r\n<= Id($var1)";

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
            Assert.AreEqual(3, personsAfter.Count());
        }

    }
}