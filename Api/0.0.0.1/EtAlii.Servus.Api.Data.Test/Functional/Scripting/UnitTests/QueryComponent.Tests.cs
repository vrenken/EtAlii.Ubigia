namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class QueryComponent_Tests
    {
        private IScriptParser _parser;

        [TestInitialize]
        public void Initialize()
        {
            _parser = ScriptParserFactory.Create();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void QueryComponent_ToString()
        {
            // Arrange.
            var component = new QueryComponent("Property", "Value");

            // Act.
            var result = component.ToString();

            // Assert.
            Assert.AreEqual("Property:Value", result);
        }
    }
}