namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;

    [TestClass]
    public class IsParentOfPathSubjectPart_Tests
    {
        private IScriptParser _parser;

        [TestInitialize]
        public void Initialize()
        {
            var diagnostics = ApiTestHelper.CreateDiagnostics();
            _parser = new ScriptParserFactory().Create(diagnostics);
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