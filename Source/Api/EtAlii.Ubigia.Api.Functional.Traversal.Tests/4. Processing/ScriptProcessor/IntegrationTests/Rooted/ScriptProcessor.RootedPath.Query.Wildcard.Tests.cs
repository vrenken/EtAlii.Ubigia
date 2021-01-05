namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class ScriptProcessorRootedPathQueryWildcardIntegrationTests : IClassFixture<ScriptingUnitTestContext>, IAsyncLifetime
    {
        private IScriptParser _parser;
        private ILogicalContext _logicalContext;
        private readonly ScriptingUnitTestContext _testContext;

        public ScriptProcessorRootedPathQueryWildcardIntegrationTests(ScriptingUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_testContext.Diagnostics);
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
        public async Task ScriptProcessor_RootedPath_Wildcard_01()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Joe",
                "Person:+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "Person:Doe/Jo*";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_testContext.Diagnostics)
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
            Assert.Equal(3, result.Length);
            dynamic person1 = result.Skip(0).First();
            dynamic person2 = result.Skip(1).First();
            dynamic person3 = result.Skip(2).First();
            Assert.Equal("John", person1.ToString());
            Assert.Equal("Joe", person2.ToString());
            Assert.Equal("Johnny", person3.ToString());
        }
    }
}
