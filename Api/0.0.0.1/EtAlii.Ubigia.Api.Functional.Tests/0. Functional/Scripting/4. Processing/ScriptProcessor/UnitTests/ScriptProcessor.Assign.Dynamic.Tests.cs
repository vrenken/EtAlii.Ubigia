namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    

    
    public class ScriptProcessor_Assign_Dynamic_UnitTests : IDisposable
    {
        private IScriptParser _parser;
        private readonly IDiagnosticsConfiguration _diagnostics;

        public ScriptProcessor_Assign_Dynamic_UnitTests()
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