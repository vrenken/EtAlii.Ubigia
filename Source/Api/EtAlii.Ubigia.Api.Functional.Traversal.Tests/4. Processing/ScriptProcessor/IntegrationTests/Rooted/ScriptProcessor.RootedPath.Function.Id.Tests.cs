
namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorRootedPathFunctionIdTests : IClassFixture<TraversalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;
        private readonly IDiagnosticsConfiguration _diagnostics;
        private readonly TraversalUnitTestContext _testContext;

        public ScriptProcessorRootedPathFunctionIdTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _diagnostics = DiagnosticsConfiguration.Default;
            _parser = new TestScriptParserFactory().Create();
        }
        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Assign()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "<= id() <= Person:";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<Identifier>(result.Single());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Variable_Path_01()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "Person: += Doe/John\r\n$path <= Person:Doe/John\r\nid($path)";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<Identifier>(result.Single());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Variable_Path_02()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "$path <= Person:\r\nid($path)";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<Identifier>(result.Single());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Variable_Path_Variable()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "$path <= Person:\r\n$id <= id($path)\r\n$id";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<Identifier>(result.Single());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Path_01()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "id(Person:)";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<Identifier>(result.Single());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Path_02()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "$var1 <= Person: += Doe/Jane\r\n$var2 <= id(Person:Doe/Jane)";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics, scope);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Empty(result);  // << Question: should the $var2 assignment create output or not? Nope it should not. only assign (<=), function or path results should.
            Assert.IsAssignableFrom<INode>(await scope.Variables["var1"].Value.SingleAsync());
            Assert.IsType<Identifier>(await scope.Variables["var2"].Value.SingleAsync());
            Assert.Equal((await scope.Variables["var1"].Value.Cast<INode>().SingleAsync()).Id, await scope.Variables["var2"].Value.Cast<Identifier>().SingleAsync());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Path_03()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "<= Id(Person:Doe/*)";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var personsAfter = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(personsAfter);
            Assert.Equal(3, personsAfter.Length);
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Path_04()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "$var1 <= Person:Doe/*\r\n<= Id($var1)";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var personsAfter = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(personsAfter);
            Assert.Equal(3, personsAfter.Length);
        }

    }
}
