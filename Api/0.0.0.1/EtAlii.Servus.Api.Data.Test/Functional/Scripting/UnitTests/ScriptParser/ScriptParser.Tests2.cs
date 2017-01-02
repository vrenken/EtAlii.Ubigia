namespace EtAlii.Servus.Api.Data.UnitTests
{
    using System;
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public partial class ScriptParser_Tests2
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
        public void ScriptParser_Parse_Single_Line()
        {
            // Arrange.
            var text = "'SingleLine'";

            // Act.
            var script = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 1);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines_RN()
        {
            // Arrange.
            var text = "'First'\r\n'Second'";

            // Act.
            var script = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 2);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines_N()
        {
            // Arrange.
            var text = "'First'\n'Second'";

            // Act.
            var script = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 2);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Additonal_Newline()
        {
            // Arrange.
            var text = "'First'\n'Second'\r\n\r\n'Third'";

            // Act.
            var script = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 3);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Trailing_Newline()
        {
            // Arrange.
            var text = "'First'\n'Second'\r\n'Third'\r\n";

            // Act.
            var script = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 3);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Trailing_Newline_And_Variable()
        {
            // Arrange.
            var text = "'First'\n$second\r\n'Third'\r\n";

            // Act.
            var script = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 3);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Leading_Newline()
        {
            // Arrange.
            var text = "\r\n'First'\n'Second'\r\n'Third'";

            // Act.
            var script = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 3);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Leading_Newline_And_Variable()
        {
            // Arrange.
            var text = "\r\n'First'\n$second\r\n'Third'";

            // Act.
            var script = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 3);
        }
    }
}