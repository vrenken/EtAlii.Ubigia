namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class ScriptParser_Function_Rename_Tests
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
        public void ScriptParser_Rename()
        {
            // Arrange.
            var scriptText = new[]
            {
                "$v0 <= /Documents/Files+=/Images",
                "$v1 <= rename($v0, 'Photos')",
                "/Documents/Files",
            };

            // Act.
            var script = _parser.Parse(scriptText).Script;

            // Assert.
            //Assert.IsNotNull(script);
            //Assert.IsTrue(script.Sequences.Count() == 1);
        }
    }
}