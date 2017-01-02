// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class ScriptParser_Tests2
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
        public void ScriptParser_Parse_Single_Line_01()
        {
            // Arrange.
            var text = "'SingleLine'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
            Assert.IsNull(script);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Single_Line_02()
        {
            // Arrange.
            var text = "/'SingleLine'";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 1);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines_RN_01()
        {
            // Arrange.
            var text = "'First'\r\n'Second'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
            Assert.IsNull(script);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines_RN_02()
        {
            // Arrange.
            var text = "/'First'\r\n/'Second'";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 2);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines_N_01()
        {
            // Arrange.
            var text = "'First'\n'Second'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
            Assert.IsNull(script);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines_N_02()
        {
            // Arrange.
            var text = "/'First'\n/'Second'";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 2);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Additonal_Newline()
        {
            // Arrange.
            var text = "/'First'\n/'Second'\r\n\r\n/'Third'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 3);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Trailing_Newline_01()
        {
            // Arrange.
            var text = "'First'\n'Second'\r\n'Third'\r\n";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
            Assert.IsNull(script);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Trailing_Newline_02()
        {
            // Arrange.
            var text = "/'First'\n/'Second'\r\n/'Third'\r\n";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 3);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Trailing_Newline_And_Variable_01()
        {
            // Arrange.
            var text = "'First'\n$second\r\n'Third'\r\n";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
            Assert.IsNull(script);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Trailing_Newline_And_Variable_02()
        {
            // Arrange.
            var text = "/'First'\n$second\r\n/'Third'\r\n";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 3);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Leading_Newline_01()
        {
            // Arrange.
            var text = "\r\n'First'\n'Second'\r\n'Third'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
            Assert.IsNull(script);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Leading_Newline_02()
        {
            // Arrange.
            var text = "\r\n/'First'\n/'Second'\r\n/'Third'";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 3);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Leading_Newline_And_Variable_01()
        {
            // Arrange.
            var text = "\r\n'First'\n$second\r\n'Third'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
            Assert.IsNull(script);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Leading_Newline_And_Variable_02()
        {
            // Arrange.
            var text = "\r\n/'First'\n$second\r\n/'Third'";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Sequences.Count() == 3);
        }
    }
}