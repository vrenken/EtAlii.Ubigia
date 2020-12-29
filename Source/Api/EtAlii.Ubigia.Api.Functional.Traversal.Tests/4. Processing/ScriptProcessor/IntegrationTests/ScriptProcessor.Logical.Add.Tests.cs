namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorLogicalAddTests : IClassFixture<ScriptingUnitTestContext>, IAsyncLifetime
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private readonly ScriptingUnitTestContext _testContext;

        public ScriptProcessorLogicalAddTests(ScriptingUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            _diagnostics = DiagnosticsConfiguration.Default;
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            _parser = new TestScriptParserFactory().Create(scriptParserConfiguration);
            _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
        }

        public Task DisposeAsync()
        {
            _parser = null;
            _logicalContext.Dispose();
            _logicalContext = null;
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Location_Add()
        {
            // Arrange.
            var locationPath = await _testContext.LogicalTestContext.AddContinentCountryRegionCityLocation(_logicalContext).ConfigureAwait(false);
            var selectQuery = $"<= {locationPath}";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(selectScript);
            dynamic locationEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(locationEntry);
            Assert.NotEqual(Identifier.Empty, ((INode)locationEntry).Id);
            Assert.Equal(selectQuery.Split(new[] { '/' }).Last(), locationEntry.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Get_Time()
        {
            // Arrange.
            var locationPath = await _testContext.LogicalTestContext.AddContinentCountryRegionCityLocation(_logicalContext).ConfigureAwait(false);
            var selectQuery = $"<= {locationPath}";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(selectScript);
            dynamic locationEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(locationEntry);
            Assert.NotEqual(Identifier.Empty, ((INode)locationEntry).Id);
            Assert.Equal(selectQuery.Split(new[] { '/' }).Last(), locationEntry.ToString());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Time_Add_With_Variable()
        {
            // Arrange.
            var locationPath = await _testContext.LogicalTestContext.AddContinentCountryRegionCityLocation(_logicalContext).ConfigureAwait(false);
            var selectQuery = $"<= {locationPath}";
            var selectQueryParts = selectQuery.Split(new[] {'/'});
            var variable = selectQueryParts[3];
            selectQueryParts[3] = "$variable";
            selectQuery = string.Join("/", selectQueryParts);
            selectQuery = $"$variable <= \"{variable}\"\r\n{selectQuery}";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(selectScript);
            dynamic locationEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(locationEntry);
            Assert.NotEqual(Identifier.Empty, ((INode)locationEntry).Id);
            Assert.Equal(selectQuery.Split(new[] { '/' }).Last(), locationEntry.ToString());
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_1()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.FirstOrDefaultAsync();

            // Act.
            var parseResult = _parser.Parse("/Person/LastName += SurName");
            var addScript = parseResult.Script;
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic afterResult = await lastSequence.Output.FirstOrDefaultAsync();

            // Assert.
            Assert.False(parseResult.Errors.Any(), parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_2()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var addScript = _parser.Parse("/Person/LastName += \"SurName\"").Script;
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_Empty_Item_2()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();
            var addScript = _parser.Parse("/Person/LastName += \"\"").Script;

            // Act.
            var act = processor.Process(addScript);

            // Assert.
            Assert.Null(beforeResult);
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_3()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var addScript = _parser.Parse("/Person/LastName += SurName").Script;
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_4()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var addScript = _parser.Parse("/Person/LastName += \"SurName\"").Script;
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_With_Variable_1()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var parseResult = _parser.Parse("/Person/LastName += $var");
            var addScript = parseResult.Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.False(parseResult.Errors.Any(), parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_With_Variable_2()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();
            var parseResult = _parser.Parse("$var <= \"SurName\"\r\n/Person/LastName += $var");
            var addScript = parseResult.Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");

            // Act.
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.False(parseResult.Errors.Any(), parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Add_With_Variable_3()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastNameOriginal","SurName").ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();
            var parseResult = _parser.Parse("$var <= /Person/LastNameOriginal/SurName\r\n/Person/LastName += $var");
            var addScript = parseResult.Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");

            // Act.
            lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.False(parseResult.Errors.Any(), parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((INode)afterResult).Id);
        }
    }
}
