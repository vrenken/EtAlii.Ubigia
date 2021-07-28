// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Xunit;

    public partial class ScriptParserRootedPathTests : IClassFixture<TraversalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;

        private static readonly char[] _normalCharacters =
        {
            'a', 'b', 'z', '1', '2', '_'
        };

        private static readonly char[] _specialCharacters =
        {
            '+', '-',
            ' ',
//            '$',
            ':', '@',
            '(',')',
            '.', ',',
        };

        private static readonly char[] _specialCharacters2 =
        {
            '+', '-',
            ' ',
//            '$',
            ':', '@',
            '(',')',
            '.', ',',
             'ä','ë','ö','ü','á','é','ó','ú','â','ê','ô','û'
        };

        public ScriptParserRootedPathTests(TraversalUnitTestContext testContext)
        {
            _parser = new TestScriptParserFactory().Create(testContext.ClientConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse()
        {
            // Arrange.
            const string query = "$e1 <= First:Second";

            // Act.
            var result = _parser.Parse(query);
            var script = result.Script;

            // Assert.
            Assert.NotNull(script);
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_Newline_N_01()
        {
            // Arrange.
            const string query = "First:1\nSecond:2\nThird:3\n$var1 <= Fourth:4\nFifth:5";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(5, script.Sequences.Count());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_Newline_N_02()
        {
            // Arrange.
            const string query = "/First/1\nSecond:2\n/Third/3\n$var1 <= Fourth:4\n/Fifth/5";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(5, script.Sequences.Count());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_Newline_N_03()
        {
            // Arrange.
            const string query = "First:1\n/Second/2\nThird:3\n$var1 <= /Fourth/4\nFifth:5";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(5, script.Sequences.Count());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_Newline_N_Invalid_Script()
        {
            // Arrange.
            const string query = "First:1\nSecond:2\nThird:3$var1 = Fourth:4\nFifth:5";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            Assert.Null(result.Script);
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_Newline_RN()
        {
            // Arrange.
            const string query = "First:1\r\nSecond:2\r\nThird:3\r\n$var1 <= Fourth:4\r\nFifth:5";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(5, script.Sequences.Count());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_VariableAssignment_With_Path_Error()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("First:Second/Third/Fourth\r\n$var1 = Fourth:4\r\nFifth is bad:5\r\nSixth:6");

            // Assert.
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_VariableAssignment_With_Separator_Error()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("First:Second/Third/Fourth\r\n$var1 = Fourth:4\r\nFifth:/5\r\nSixth:6");

            // Assert.
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_Comment_3()
        {
            // Arrange.
            var query = "this:line/is/safe\r\n--and this line also";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(2, script.Sequences.Count());
            Assert.IsType<RootedPathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(1).Parts.Skip(0).First());

        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_Comment_4()
        {
            // Arrange.
            const string query = "--this line is safe\r\nand:this/line/also";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(2, script.Sequences.Count());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(0).First());
            Assert.IsType<RootedPathSubject>(script.Sequences.ElementAt(1).Parts.Skip(1).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_Comment_5()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("this:line/is/safe --and this comment also").Script;

            // Assert.
            Assert.Single(script.Sequences);
            Assert.IsType<RootedPathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(2).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_Comment_6()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("this:line/is/safe   --and this comment also").Script;

            // Assert.
            Assert.Single(script.Sequences);
            Assert.IsType<RootedPathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(2).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_Parse_Comment_7()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("this:line/is/safe--and this comment also").Script;

            // Assert.
            Assert.Single(script.Sequences);
            Assert.IsType<RootedPathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(2).First());
        }
    }
}
