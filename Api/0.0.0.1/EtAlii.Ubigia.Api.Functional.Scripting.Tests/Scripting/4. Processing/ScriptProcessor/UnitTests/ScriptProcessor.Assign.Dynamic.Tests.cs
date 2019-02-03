namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    
    public class ScriptProcessorAssignDynamicUnitTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptProcessorAssignDynamicUnitTests()
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                _parser = null;
            }
        }

        ~ScriptProcessorAssignDynamicUnitTests()
        {
            Dispose(false);
        }

    }
}