namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScriptScopeHelper_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptScopeHelper_GetAsVariable()
        {
            // Arrange.
            var scope = new ScriptScope();
            var scriptScopeHelper = new ScriptScopeHelper(scope);
            var pathComponent = new VariableComponent("ping");
            scope.Variables["ping"] = new ScopeVariable("pong", "source");
            
            // Act.
            var result = scriptScopeHelper.GetAsVariable(pathComponent);

            // Assert.
            Assert.AreEqual("pong", result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptScopeHelper_GetAsVariable_Null()
        {
            // Arrange.
            var scope = new ScriptScope();
            var scriptScopeHelper = new ScriptScopeHelper(scope);
            var pathComponent = new VariableComponent("ping");

            // Act.
            var result = scriptScopeHelper.GetAsVariable(pathComponent);

            // Assert.
            Assert.IsNull(result);
        }
    }
}