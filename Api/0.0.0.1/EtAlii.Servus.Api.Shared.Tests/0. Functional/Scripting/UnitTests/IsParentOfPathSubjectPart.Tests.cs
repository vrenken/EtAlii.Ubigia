namespace EtAlii.Servus.Api.Functional.Tests
{
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IsParentOfPathSubjectPart_Tests
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
        public void IsParentOfPathSubjectPart_ToString()
        {
            // Arrange.
            var part = new IsParentOfPathSubjectPart();

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.AreEqual("/", result);
        }
    }
}