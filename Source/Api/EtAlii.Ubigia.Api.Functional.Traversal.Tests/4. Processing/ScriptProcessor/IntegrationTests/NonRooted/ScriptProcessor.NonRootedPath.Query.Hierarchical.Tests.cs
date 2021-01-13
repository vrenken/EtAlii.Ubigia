namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorNonRootedPathQueryHierarchicalIntegrationTests : IClassFixture<TraversalUnitTestContext>, IAsyncLifetime
    {
        private readonly TraversalUnitTestContext _testContext;
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;

        public ScriptProcessorNonRootedPathQueryHierarchicalIntegrationTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            _diagnostics = DiagnosticsConfiguration.Default;
            _parser = new TestScriptParserFactory().Create();
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
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale:true, Birthdate:1980-02-25, Weight:76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/John\\";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new TestScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_Bool()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale: true, Birthdate:1980-02-25, Weight:76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.IsMale=true\\";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new TestScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();
            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_DateTime()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale: true, Birthdate: 1980-02-25, Weight: 76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.Birthdate=1980-02-25\\";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new TestScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_Float()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale: true, Birthdate: 1980-02-25, Weight: 76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.Weight=76.23\\";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new TestScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }
    }
}
