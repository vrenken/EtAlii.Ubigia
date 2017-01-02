namespace EtAlii.Servus.Api.Functional.Tests
{
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScriptProcessor_Assign_Dynamic_UnitTests
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;

        [TestInitialize]
        public void Initialize()
        {
            _diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

    }
}