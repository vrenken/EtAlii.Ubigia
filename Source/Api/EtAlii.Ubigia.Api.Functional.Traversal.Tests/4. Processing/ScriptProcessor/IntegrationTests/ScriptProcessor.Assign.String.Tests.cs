namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class ScriptProcessorAssignStringIntegrationTests : IClassFixture<ScriptingUnitTestContext>, IAsyncLifetime
    {
        private readonly ScriptingUnitTestContext _testContext;
        //private IScriptParser _parser
        private ILogicalContext _logicalContext;

        public ScriptProcessorAssignStringIntegrationTests(ScriptingUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            //var diagnostics = TestDiagnostics.Create();
            //  var scriptParserConfiguration = new ScriptParserConfiguration()
            //      .UseFunctionalDiagnostics(diagnostics)
            //_parser = new ScriptParserFactory().Create(scriptParserConfiguration)
            _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
        }

        public Task DisposeAsync()
        {
            //_parser = null
            _logicalContext.Dispose();
            _logicalContext = null;
            return Task.CompletedTask;
        }
    }
}
