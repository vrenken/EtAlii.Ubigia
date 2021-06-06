namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorRootedPathFunctionCountTests : IAsyncLifetime
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new LogicalTestContextFactory().Create();
            await _testContext.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);

            _diagnostics = DiagnosticsConfiguration.Default;
            _parser = new TestScriptParserFactory().Create();
        }

        public async Task DisposeAsync()
        {
            _parser = null;

            await _testContext.Stop().ConfigureAwait(false);
            _testContext = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Count_01()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "<= Count() <= Person:Doe/*";

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
            Assert.Single(personsAfter);
            Assert.Equal(3, personsAfter.Single());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Count_02()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "<= Count(Person:Doe/*)";

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
            Assert.Single(personsAfter);
            Assert.Equal(3, personsAfter.Single());
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Count_03()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "$var1 <= Person:Doe/*\r\n<= Count($var1)";

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
            Assert.Single(personsAfter);
            Assert.Equal(3, personsAfter.Single());
        }
    }
}
