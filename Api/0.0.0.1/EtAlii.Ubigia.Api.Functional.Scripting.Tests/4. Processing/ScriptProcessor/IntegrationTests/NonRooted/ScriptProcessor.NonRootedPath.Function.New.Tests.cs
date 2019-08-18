namespace EtAlii.Ubigia.Api.Functional.Scripting.GraphSL.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorNonRootedPathFunctionNewTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private readonly LogicalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathFunctionNewTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public Task InitializeAsync()
        {
            _diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            _parser = null;
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Function_New_Path_01()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            const string query = "/Person += Doe/John/Visits\r\n/Person/Doe/John/Visits += new()";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.NotNull(result[0]);
            Assert.StartsWith("Undefined_", result[0].ToString());

        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Function_New_Path_02()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            const string query = "/Person += Doe/John/Visits\r\n/Person/Doe/John/Visits += new('Vacation')";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<object[]>(result);
            Assert.NotNull(result[0]);
            Assert.Equal("Vacation", result[0].ToString());
        }
        
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Function_New_Path_03()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            const string query = "/Person += Doe/John/Visits\r\n/Person/Doe/John/Visits += new(\"Vacation\")";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.NotNull(result[0]);
            Assert.Equal("Vacation", result[0].ToString());
        }

    }
}