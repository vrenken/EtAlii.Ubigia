namespace EtAlii.Servus.Api.Functional.Tests
{
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class ScriptParser_Function_File_Tests
    {
        private IScriptParser _parser;

        [TestInitialize]
        public void Initialize() 
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_File_Add()
        {
            // Arrange.
            //var text = "'SingleLine'";

            // Act.
            //var script = _parser.Parse(text).Script;

            // Assert.
            //Assert.IsNotNull(script);
            //Assert.IsTrue(script.Sequences.Count() == 1);
        }
    }
}