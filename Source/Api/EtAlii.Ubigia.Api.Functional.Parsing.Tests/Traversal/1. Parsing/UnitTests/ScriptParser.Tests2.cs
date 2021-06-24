// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public class ScriptParserTests2 : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserTests2()
        {
            _parser = new TestScriptParserFactory().Create();
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Single_Line_01()
        {
            // Arrange.
            var text = "'SingleLine'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(parseResult.Errors);
            Assert.Null(script);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Single_Line_02()
        {
            // Arrange.
            var text = "/'SingleLine'";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.NotNull(script);
            Assert.True(script.Sequences.Count() == 1);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines_RN_01()
        {
            // Arrange.
            var text = "'First'\r\n'Second'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(parseResult.Errors);
            Assert.Null(script);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines_RN_02()
        {
            // Arrange.
            var text = "/'First'\r\n/'Second'";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.NotNull(script);
            Assert.True(script.Sequences.Count() == 2);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines_N_01()
        {
            // Arrange.
            var text = "'First'\n'Second'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(parseResult.Errors);
            Assert.Null(script);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines_N_02()
        {
            // Arrange.
            var text = "/'First'\n/'Second'";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.NotNull(script);
            Assert.True(script.Sequences.Count() == 2);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Additional_Newline()
        {
            // Arrange.
            var text = "/'First'\n/'Second'\r\n\r\n/'Third'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.NotNull(script);
            Assert.Equal(3, script.Sequences.Count());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Trailing_Newline_01()
        {
            // Arrange.
            var text = "'First'\n'Second'\r\n'Third'\r\n";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(parseResult.Errors);
            Assert.Null(script);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Trailing_Newline_02()
        {
            // Arrange.
            var text = "/'First'\n/'Second'\r\n/'Third'\r\n";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.NotNull(script);
            Assert.True(script.Sequences.Count() == 3);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Trailing_Newline_And_Variable_01()
        {
            // Arrange.
            var text = "'First'\n$second\r\n'Third'\r\n";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(parseResult.Errors);
            Assert.Null(script);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Trailing_Newline_And_Variable_02()
        {
            // Arrange.
            var text = "/'First'\n$second\r\n/'Third'\r\n";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.NotNull(script);
            Assert.True(script.Sequences.Count() == 3);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Leading_Newline_01()
        {
            // Arrange.
            var text = "\r\n'First'\n'Second'\r\n'Third'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(parseResult.Errors);
            Assert.Null(script);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Leading_Newline_02()
        {
            // Arrange.
            var text = "\r\n/'First'\n/'Second'\r\n/'Third'";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.NotNull(script);
            Assert.True(script.Sequences.Count() == 3);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Leading_Newline_And_Variable_01()
        {
            // Arrange.
            var text = "\r\n'First'\n$second\r\n'Third'";

            // Act.
            var parseResult = _parser.Parse(text);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(parseResult.Errors);
            Assert.Null(script);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Multiple_Lines__With_Leading_Newline_And_Variable_02()
        {
            // Arrange.
            var text = "\r\n/'First'\n$second\r\n/'Third'";

            // Act.
            var script = _parser.Parse(text).Script;

            // Assert.
            Assert.NotNull(script);
            Assert.True(script.Sequences.Count() == 3);
        }
    }
}
