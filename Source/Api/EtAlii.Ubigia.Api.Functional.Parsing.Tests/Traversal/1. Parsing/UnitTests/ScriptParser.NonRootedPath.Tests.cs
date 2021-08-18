// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public partial class ScriptParserNonRootedPathTests : IClassFixture<FunctionalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;

        private static readonly char[] _normalCharacters =
        {
            'a', 'b', 'z', '1', '2', '_',
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

        public ScriptParserNonRootedPathTests(FunctionalUnitTestContext testContext)
        {
            _parser = testContext.CreateScriptParser();
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "$e1 <= /First/Second";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            Assert.NotNull(result.Script);
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Newline_N()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/1\n/Second/2\n/Third/3\n$var1 <= /Fourth/4\n/Fifth/5";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(5, script.Sequences.Count());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Newline_N_Invalid_Script()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/1\n/Second/2\n/Third/3$var1 = /Fourth/4\n/Fifth/5";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            Assert.Null(result.Script);
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Newline_RN()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/1\r\n/Second/2\r\n/Third/3\r\n$var1 <= /Fourth/4\r\n/Fifth/5";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(5, script.Sequences.Count());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_VariableAssignment_With_Path_Error()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var result = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 = /Fourth/4\r\n/Fifth is bad/5\r\n/Sixth/6", scope);

            // Assert.
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_VariableAssignment_With_Separator_Error()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var result = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 = /Fourth/4\r\n/Fifth//5\r\n/Sixth/6", scope);

            // Assert.
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Comment_1()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("--thislineissafe", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Comment_2()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("--thislineissafe --and this line also", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Comment_3()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var query = "/this/line/is/safe\r\n--and this line also";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(2, script.Sequences.Count());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(1).Parts.Skip(0).First());

        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Comment_4()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "--this line is safe\r\n/and/this/line/also";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(2, script.Sequences.Count());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(0).First());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(1).Parts.Skip(1).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Comment_5()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/this/line/is/safe --and this comment also", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(2).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Comment_6()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/this/line/is/safe   --and this comment also", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(2).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Comment_7()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/this/line/is/safe--and this comment also", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(2).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Comment_With_Error_1()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var result = _parser.Parse("thislineisbad --and this should be ok", scope);

            // Assert.
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Comment_With_Error_2()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var result = _parser.Parse("thislineisbad", scope);

            // Assert.
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_MultiLine_Comment_With_Error_1()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var result = _parser.Parse("--ThisLineIsOk\r\nButThisLineIsBad", scope);

            // Assert.
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_MultiLine_Comment_With_Error_2()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var result = _parser.Parse("--ThisLineIsBad\r\nButThisLineIsOk", scope);

            // Assert.
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_MultiLine_Comment()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("--ThisLineIsOk\r\n--AndThisLineAlso", scope).Script;

            // Assert.
            Assert.Equal(2, script.Sequences.Count());
        }
    }
}
