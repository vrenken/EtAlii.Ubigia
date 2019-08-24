namespace EtAlii.Ubigia.Api.Functional.Scripting.GraphSL.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorNonRootedPathAssignAnonymous2IntegrationTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;

        private readonly LogicalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathAssignAnonymous2IntegrationTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            _diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
        }

        public Task DisposeAsync()
        {
            _parser = null;
            _logicalContext.Dispose();
            _logicalContext = null;
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Assign_Anonymous_Should_Update_01()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables.Add("first", new ScopeVariable(new { ObjectType = "Family" }, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(new { ObjectType = "Person" }, "Variable"));
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
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
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Person", result2.ObjectType);
            Assert.NotEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Assign_Anonymous_Should_Not_Update_01()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables.Add("first", new ScopeVariable(new { ObjectType = "Family" }, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(new { ObjectType = "Family" }, "Variable"));
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
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
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Family", result2.ObjectType);
            Assert.Equal(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Assign_Anonymous_Should_Update_02()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables.Add("first", new ScopeVariable(new { ObjectType = "Family", Code = "ABC" }, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(new { ObjectType = "Person", Code = "ABC" }, "Variable"));

            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
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
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Person", result2.ObjectType);
            Assert.Equal("ABC", result1.Code);
            Assert.Equal("ABC", result2.Code);
            Assert.NotEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Assign_Anonymous_Should_Update_03()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables.Add("first", new ScopeVariable(new { ObjectType = "Family" }, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(new { ObjectType = "Person", Code = "ABC" }, "Variable"));

            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
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
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Person", result2.ObjectType);
            Assert.Equal("ABC", result2.Code);
            Assert.NotEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Assign_Anonymous_Should_Not_Update_02()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables.Add("first", new ScopeVariable(new { ObjectType = "Family", Code = "ABC" }, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(new { ObjectType = "Family", Code = "ABC" }, "Variable"));

            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
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
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Family", result2.ObjectType);
            Assert.Equal("ABC", result1.Code);
            Assert.Equal("ABC", result2.Code);
            Assert.Equal(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Assign_Anonymous_Should_Not_Update_03()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables.Add("first", new ScopeVariable(new { ObjectType = "Family", Code = "ABC" }, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(new { ObjectType = "Family", }, "Variable"));
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
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
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Family", result2.ObjectType);
            Assert.Equal("ABC", result1.Code);
            Assert.Equal("ABC", result2.Code);
            Assert.Equal(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Assign_Anonymous_Should_Not_Update_04()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables.Add("first", new ScopeVariable(new { ObjectType = (string)null, Code = (string)null }, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(new { ObjectType = (string)null, Code = (string)null }, "Variable"));
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
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
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            //Assert.Equal(null, result1.ObjectType)
            //Assert.Equal(null, result2.ObjectType)
            //Assert.Equal(null, result1.Code)
            //Assert.Equal(null, result2.Code)
            Assert.Equal(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Assign_Anonymous_Should_Not_Update_05()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables.Add("first", new ScopeVariable(new { ObjectType = "TEST", Code = (string)null }, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(new { ObjectType = "TEST", Code = (string)null }, "Variable"));
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
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
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("TEST", result1.ObjectType);
            Assert.Equal("TEST", result2.ObjectType);
            //Assert.Equal(null, result1.Code)
            //Assert.Equal(null, result2.Code)
            Assert.Equal(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }
    }
}