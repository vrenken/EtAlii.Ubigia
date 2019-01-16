
namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using Xunit;

    public class ScriptProcessorAssignStringIntegrationTests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;
        private ILogicalContext _logicalContext;

        public ScriptProcessorAssignStringIntegrationTests(LogicalUnitTestContext testContext)
        {
            var task = Task.Run(async () =>
            {
                var diagnostics = TestDiagnostics.Create();
                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(diagnostics);
                _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
                _logicalContext = await testContext.LogicalTestContext.CreateLogicalContext(true);
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(() =>
            {
                _parser = null;
                _logicalContext.Dispose();
                _logicalContext = null;
            });
            task.Wait();
        }
    }
}