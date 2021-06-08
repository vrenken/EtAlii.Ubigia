namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorRootAssignTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly IDiagnosticsConfiguration _diagnostics;
        private readonly TraversalUnitTestContext _testContext;

        public ScriptProcessorRootAssignTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _diagnostics = DiagnosticsConfiguration.Default;
            _parser = new TestScriptParserFactory().Create();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);

            const string query = "root:time <= EtAlii.Ubigia.Roots.Time";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots
                .GetAll()
                .SingleOrDefaultAsync(r => r.Name == "time")
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("time", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root_And_Using_Short_RootType()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);

            const string query = "root:time <= Time";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots.GetAll().SingleOrDefaultAsync(r => r.Name == "time").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("time", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root_Under_Other_Name()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);

            const string query = "root:specialtime <= EtAlii.Ubigia.Roots.Time";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots.GetAll().SingleOrDefaultAsync(r => r.Name == "specialtime").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("specialtime", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root_Under_Other_Name_And_Using_Short_RootType()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);

            const string query = "root:specialtime <= Time";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots.GetAll().SingleOrDefaultAsync(r => r.Name == "specialtime").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("specialtime", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Object_Root_Under_Other_Name()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);

            const string query = "root:projects <= EtAlii.Ubigia.Roots.Object";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots.GetAll().SingleOrDefaultAsync(r => r.Name == "projects").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("projects", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Object_Root_Under_Other_Name_And_Using_Short_RootType()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);

            const string query = "root:projects <= Object";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots.GetAll().SingleOrDefaultAsync(r => r.Name == "projects").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("projects", root.Name);
        }
    }
}
