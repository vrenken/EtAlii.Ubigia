namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting.Parsing;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;


    
    public partial class ScriptParser_Tests : IDisposable
    {
        private IScriptParser _parser;
        
        private static readonly char[] NormalCharacters = new []
        {
            'a', 'b', 'z', '1', '2', '_',
        };

        private static readonly char[] SpecialCharacters = new[]
        {
            '+', '-',
            ' ',
//            '$',
            ':', '@',
            '(',')',
            '.', ',',
        };

        private static readonly char[] SpecialCharacters2 = new[]
        {
            '+', '-',
            ' ',
//            '$',
            ':', '@',
            '(',')',
            '.', ',',
             'ä','ë','ö','ü','á','é','ó','ú','â','ê','ô','û'
        };

        public ScriptParser_Tests()
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse()
        {
            // Arrange.
            const string query = "$e1 <= /First/Second";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Newline_N()
        {
            // Arrange.
            const string query = "/First/1\n/Second/2\n/Third/3\n$var1 <= /Fourth/4\n/Fifth/5";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(5, script.Sequences.Count());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Newline_N_Invalid_Script()
        {
            // Arrange.
            const string query = "/First/1\n/Second/2\n/Third/3$var1 = /Fourth/4\n/Fifth/5";
            
            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Newline_RN()
        {
            // Arrange.
            const string query = "/First/1\r\n/Second/2\r\n/Third/3\r\n$var1 <= /Fourth/4\r\n/Fifth/5";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(5, script.Sequences.Count());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_VariableAssignment_With_Path_Error()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 = /Fourth/4\r\n/Fifth is bad/5\r\n/Sixth/6");

            // Assert.
            Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_VariableAssignment_With_Separator_Error()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 = /Fourth/4\r\n/Fifth//5\r\n/Sixth/6");

            // Assert.
            Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("#thislineissafe").Script;

            // Assert.
            Assert.Equal(1, script.Sequences.Count());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("#thislineissafe #and this line also").Script;

            // Assert.
            Assert.Equal(1, script.Sequences.Count());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_3()
        {
            // Arrange.
            var query = "/this/line/is/safe\r\n#and this line also";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(2, script.Sequences.Count());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(1).Parts.Skip(0).First());

        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_4()
        {
            // Arrange.
            const string query = "#this line is safe\r\n/and/this/line/also";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(2, script.Sequences.Count());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(0).First());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(1).Parts.Skip(1).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_5()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/this/line/is/safe #and this comment also").Script;

            // Assert.
            Assert.Equal(1, script.Sequences.Count());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(2).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_6()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/this/line/is/safe   #and this comment also").Script;

            // Assert.
            Assert.Equal(1, script.Sequences.Count());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(2).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_7()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/this/line/is/safe#and this comment also").Script;

            // Assert.
            Assert.Equal(1, script.Sequences.Count());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<Comment>(script.Sequences.ElementAt(0).Parts.Skip(2).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_With_Error_1()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("thislineisbad #and this should be ok");

            // Assert.
            Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_With_Error_2()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("thislineisbad");

            // Assert.
            Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_MultiLine_Comment_With_Error_1()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("#ThisLineIsOk\r\nButThisLineIsBad");

            // Assert.
            Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_MultiLine_Comment_With_Error_2()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("#ThisLineIsBad\r\nButThisLineIsOk");

            // Assert.
            Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_MultiLine_Comment()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("#ThisLineIsOk\r\n#AndThisLineAlso").Script;

            // Assert.
            Assert.Equal(2, script.Sequences.Count());
        }
    }
}