namespace EtAlii.Ubigia.Api.Functional.Scripting.GraphSL.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;

    public class ScriptProcessorAssignDynamicUnitTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptProcessorAssignDynamicUnitTests()
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
        }
    }
}