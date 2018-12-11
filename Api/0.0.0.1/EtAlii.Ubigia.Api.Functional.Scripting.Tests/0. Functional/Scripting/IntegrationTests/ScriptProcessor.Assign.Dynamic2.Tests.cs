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
    public class ScriptProcessor_Assign_Dynamic2_IntegrationTests
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
        public async Task ScriptProcessor_Assign_Dynamic_Should_Update_01()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= $first";
            var assignQuery2 = "/Contacts/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            dynamic firstVariable = new { ObjectType = "Family" };
            dynamic secondVariable = new { ObjectType = "Person" };
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Person", result2.ObjectType);
            Assert.AreNotEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Not_Update_01()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= $first";
            var assignQuery2 = "/Contacts/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            dynamic firstVariable = new {ObjectType = "Family"};
            dynamic secondVariable = new {ObjectType = "Family"};
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Family", result2.ObjectType);
            Assert.AreEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Update_02()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= $first";
            var assignQuery2 = "/Contacts/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            dynamic firstVariable = new {ObjectType = "Family", Code = "ABC"};
            dynamic secondVariable = new {ObjectType = "Person", Code = "ABC"};
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));

            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Person", result2.ObjectType);
            Assert.AreEqual("ABC", result1.Code);
            Assert.AreEqual("ABC", result2.Code);
            Assert.AreNotEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Update_03()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= $first";
            var assignQuery2 = "/Contacts/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            dynamic firstVariable = new {ObjectType = "Family"};
            dynamic secondVariable = new {ObjectType = "Person", Code = "ABC"};
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));

            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Person", result2.ObjectType);
            Assert.AreEqual("ABC", result2.Code);
            Assert.AreNotEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Not_Update_02()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= $first";
            var assignQuery2 = "/Contacts/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            dynamic firstVariable = new {ObjectType = "Family", Code = "ABC"};
            dynamic secondVariable = new {ObjectType = "Family", Code = "ABC"};
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));

            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Family", result2.ObjectType);
            Assert.AreEqual("ABC", result1.Code);
            Assert.AreEqual("ABC", result2.Code);
            Assert.AreEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Not_Update_03()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= $first";
            var assignQuery2 = "/Contacts/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            dynamic firstVariable = new { ObjectType = "Family", Code = "ABC" };
            dynamic secondVariable = new { ObjectType = "Family" };
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Family", result2.ObjectType);
            Assert.AreEqual("ABC", result1.Code);
            Assert.AreEqual("ABC", result2.Code);
            Assert.AreEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Not_Update_04()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= $first";
            var assignQuery2 = "/Contacts/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            dynamic firstVariable = new { ObjectType = (string)null, Code = (string)null };
            dynamic secondVariable = new { ObjectType = (string)null, Code = (string)null };
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            //Assert.AreEqual(null, result1.ObjectType);
            //Assert.AreEqual(null, result2.ObjectType);
            //Assert.AreEqual(null, result1.Code);
            //Assert.AreEqual(null, result2.Code);
            Assert.AreEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Not_Update_05()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= $first";
            var assignQuery2 = "/Contacts/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            dynamic firstVariable = new { ObjectType = "TEST", Code = (string)null };
            dynamic secondVariable = new { ObjectType = "TEST", Code = (string)null };
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("TEST", result1.ObjectType);
            Assert.AreEqual("TEST", result2.ObjectType);
            //Assert.AreEqual(null, result1.Code);
            //Assert.AreEqual(null, result2.Code);
            Assert.AreEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

    }
}