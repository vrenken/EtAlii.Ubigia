namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Ubigia.Api.Tests.TestAssembly;

    [TestClass]
    public class ScriptProcessor_Logical_Add_Tests
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
        public async Task ScriptProcessor_Logical_Time_Add()
        {
            // Arrange.
            var timePath = await _testContext.AddYearMonthDayHourMinute(_logicalContext);
            var selectQuery = String.Format("<= {0}", timePath);
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(selectScript);
            dynamic minuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(minuteEntry);
            Assert.AreNotEqual(Identifier.Empty, ((INode)minuteEntry).Id);
            Assert.AreEqual(selectQuery.Split(new char[] { '/' }).Last(), minuteEntry.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Get_Time()
        {
            // Arrange.
            var timePath = await _testContext.AddYearMonthDayHourMinute(_logicalContext);
            var selectQuery = String.Format("<= {0}", timePath);
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(selectScript);
            dynamic minuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(minuteEntry);
            Assert.AreNotEqual(Identifier.Empty, ((INode)minuteEntry).Id);
            Assert.AreEqual(selectQuery.Split(new char[] { '/' }).Last(), minuteEntry.ToString());
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Time_Add_With_Variable()
        {
            // Arrange.
            var timePath = await _testContext.AddYearMonthDayHourMinute(_logicalContext);
            var selectQuery = String.Format("<= {0}", timePath);
            var selectQueryParts = selectQuery.Split(new char[] {'/'}); 
            var variable = selectQueryParts[3];
            selectQueryParts[3] = "$variable";
            selectQuery = String.Join("/", selectQueryParts);
            selectQuery = String.Format("$variable <= \"{0}\"\r\n", variable) + selectQuery; 
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(selectScript);
            dynamic minuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(minuteEntry);
            Assert.AreNotEqual(Identifier.Empty, ((INode)minuteEntry).Id);
            Assert.AreEqual(selectQuery.Split(new char[] { '/' }).Last(), minuteEntry.ToString());
        }



        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_1()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Contacts");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            var path = await _testContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName");
            var selectQuery = String.Format("<= /Contacts/LastName/");
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.FirstOrDefaultAsync();

            // Act.
            var parseResult = _parser.Parse("/Contacts/LastName += SurName");
            var addScript = parseResult.Script;
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic afterResult = await lastSequence.Output.FirstOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addScript, parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsNull(beforeResult);
            Assert.IsNotNull(afterResult);
            Assert.AreNotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_2()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Contacts");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            var path = await _testContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName");
            var selectQuery = String.Format("<= /Contacts/LastName/");
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var addScript = _parser.Parse("/Contacts/LastName += \"SurName\"").Script;
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNull(beforeResult);
            Assert.IsNotNull(afterResult);
            Assert.AreNotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_Empty_Item_2()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Contacts");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            var path = await _testContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName");
            var selectQuery = String.Format("<= /Contacts/LastName/");
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();
            var addScript = _parser.Parse("/Contacts/LastName += \"\"").Script;

            // Act.
            var act = processor.Process(addScript);

            // Assert.
            await ExceptionAssert.ThrowsObservable<ScriptProcessingException, SequenceProcessingResult>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_3()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Contacts");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            var path = await _testContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName");
            var selectQuery = String.Format("<= /Contacts/LastName/");
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var addScript = _parser.Parse("/Contacts/LastName += /SurName").Script;
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNull(beforeResult);
            Assert.IsNotNull(afterResult);
            Assert.AreNotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_4()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Contacts");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            var path = await _testContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName");
            var selectQuery = String.Format("<= /Contacts/LastName/");
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var addScript = _parser.Parse("/Contacts/LastName += /\"SurName\"").Script;
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNull(beforeResult);
            Assert.IsNotNull(afterResult);
            Assert.AreNotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_With_Variable_1()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Contacts");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            var path = await _testContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName");
            var selectQuery = String.Format("<= /Contacts/LastName/");
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var parseResult = _parser.Parse("/Contacts/LastName += $var");
            var addScript = parseResult.Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addScript, parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsNull(beforeResult);
            Assert.IsNotNull(afterResult);
            Assert.AreNotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_With_Variable_2()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Contacts");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            var path = await _testContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName");
            var selectQuery = String.Format("<= /Contacts/LastName/");
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();
            var parseResult = _parser.Parse("$var <= \"SurName\"\r\n/Contacts/LastName += $var");
            var addScript = parseResult.Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");

            // Act.
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addScript, parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsNull(beforeResult);
            Assert.IsNotNull(afterResult);
            Assert.AreNotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_With_Variable_3()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Contacts");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            await _testContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastNameOriginal","SurName");
            var path = await _testContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName");
            var selectQuery = String.Format("<= /Contacts/LastName/");
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();
            var parseResult = _parser.Parse("$var <= /Contacts/LastNameOriginal/SurName\r\n/Contacts/LastName += $var");
            var addScript = parseResult.Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");

            // Act.
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addScript, parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsNull(beforeResult);
            Assert.IsNotNull(afterResult);
            Assert.AreNotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }
    }
}