namespace EtAlii.Ubigia.Api.Functional.Scripting.GraphSL.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorRootUnassignTests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private readonly LogicalUnitTestContext _testContext;

        public ScriptProcessorRootUnassignTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
            _diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }
        public void Dispose()
        {
            _parser = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Unassign_Time_Root()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);

            const string query = "root:time <= ";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            const string arrangeQuery = "root:time <= Object";
            var arrangeScript = _parser.Parse(arrangeQuery).Script;
            var lastSequence = await processor.Process(arrangeScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Unassign_Time_Root_Under_Other_Name()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);

            const string query = "root:specialtime <= ";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            const string arrangeQuery = "root:specialtime <= Object";
            var arrangeScript = _parser.Parse(arrangeQuery).Script;
            var lastSequence = await processor.Process(arrangeScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Unassign_Object_Root()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);

            const string query = "root:projects <= ";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            const string arrangeQuery = "root:projects <= Object";
            var arrangeScript = _parser.Parse(arrangeQuery).Script;
            var lastSequence = await processor.Process(arrangeScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}