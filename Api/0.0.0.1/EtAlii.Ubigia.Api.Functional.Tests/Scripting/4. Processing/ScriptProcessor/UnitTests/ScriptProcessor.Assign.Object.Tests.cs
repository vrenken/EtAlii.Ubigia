namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using EtAlii.xTechnology.Diagnostics;
    

    
    public class ScriptProcessorAssignObjectUnitTests : IDisposable
    {
        private IScriptParser _parser;
        private readonly IDiagnosticsConfiguration _diagnostics;

        public ScriptProcessorAssignObjectUnitTests()
        {
            _diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
        }
    }
}