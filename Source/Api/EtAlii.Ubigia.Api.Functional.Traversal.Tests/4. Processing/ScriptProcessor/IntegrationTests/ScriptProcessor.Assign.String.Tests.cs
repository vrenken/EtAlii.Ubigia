namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class ScriptProcessorAssignStringIntegrationTests : IClassFixture<TraversalUnitTestContext>, IAsyncLifetime
    {
        private readonly TraversalUnitTestContext _testContext;
        //private IScriptParser _parser
        private ILogicalContext _logicalContext;

        public ScriptProcessorAssignStringIntegrationTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            //_parser = new TestScriptParserFactory[].Create[]
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
