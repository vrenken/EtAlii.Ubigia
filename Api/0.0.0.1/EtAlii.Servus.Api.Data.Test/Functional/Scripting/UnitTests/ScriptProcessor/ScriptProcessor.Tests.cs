namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;

    [TestClass]
    public class ScriptProcessor_Tests2
    {
        private IScriptProcessor _processor;
        private IScriptParser _parser;

        [TestInitialize]
        public void Initialize()
        {
            var diagnostics = ApiTestHelper.CreateDiagnostics();
            _parser = new ScriptParserFactory().Create(diagnostics);
            _processor = new ScriptProcessorFactory().Create(diagnostics);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
            _processor = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Assign_String_To_Variable()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"");
            var scope = new ScriptScope();

            // Act.
            _processor.Process(script, scope, null);

            // Assert.
            Assert.AreEqual("Time", scope.Variables["var1"].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Assign_String_To_Variable_Via_Variable()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1");
            var scope = new ScriptScope();

            // Act.
            _processor.Process(script, scope, null);

            // Assert.
            Assert.AreEqual("Time", scope.Variables["var2"].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Replace()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <= \"Location\"");
            var scope = new ScriptScope();

            // Act.
            _processor.Process(script, scope, null);

            // Assert.
            Assert.AreEqual("Time", scope.Variables["var2"].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Clear()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <=");
            var scope = new ScriptScope();

            // Act.
            _processor.Process(script, scope, null);

            // Assert.
            Assert.AreEqual("Time", scope.Variables["var2"].Value);
            Assert.IsFalse(scope.Variables.ContainsKey("var1"));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Empty_String()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <= \"\"");
            var scope = new ScriptScope();

            // Act.
            _processor.Process(script, scope, null);

            // Assert.
            Assert.AreEqual("Time", scope.Variables["var2"].Value);
            Assert.AreEqual("", scope.Variables["var1"].Value);
        }

    }
}