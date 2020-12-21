namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;

    public class ScriptProcessorAssignObjectUnitTests : IDisposable
    {
        //private IScriptParser _parser

        //public ScriptProcessorAssignObjectUnitTests()
        //[
            //var diagnostics = TestDiagnostics.Create()
            //var scriptParserConfiguration = new ScriptParserConfiguration()
            //    .Use(diagnostics)
            //_parser = new ScriptParserFactory().Create(scriptParserConfiguration)
        //]

        public void Dispose()
        {
            //_parser = null;
            GC.SuppressFinalize(this);
        }
    }
}
