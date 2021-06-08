namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath()
        {
            // Arrange.
            const string query = "First:Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Query()
        {
            // Arrange.
            const string query = "First:Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsType<RootedPathSubject>(script.Sequences.First().Parts.Skip(1).First());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Typed_Word()
        {
            // Arrange.
            const string query = "First:Second/[Word]/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.DumpAsString());
            Assert.IsType<RootedPathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.Equal("First", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Root);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(0));
            Assert.IsType<TypedPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(2));
            Assert.Equal("WORD", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(2).Cast<TypedPathSubjectPart>().First().Type);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(4));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Typed_word()
        {
            // Arrange.
            const string query = "First:Second/[word]/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.DumpAsString());
            Assert.IsType<RootedPathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.Equal("First", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Root);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(0));
            Assert.IsType<TypedPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(2));
            Assert.Equal("WORD", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(2).Cast<TypedPathSubjectPart>().First().Type);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(4));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Typed_WORD()
        {
            // Arrange.
            const string query = "First:Second/[WORD]/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.DumpAsString());
            Assert.IsType<RootedPathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.Equal("First", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Root);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(0));
            Assert.IsType<TypedPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(2));
            Assert.Equal("WORD", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(2).Cast<TypedPathSubjectPart>().First().Type);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(4));
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Typed_NUMBER()
        {
            // Arrange.
            const string query = "First:Second/[NUMBER]/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.DumpAsString());
            Assert.IsType<RootedPathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.Equal("First", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Root);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(0));
            Assert.IsType<TypedPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(2));
            Assert.Equal("NUMBER", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(2).Cast<TypedPathSubjectPart>().First().Type);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(4));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Regex_Time_Double_Quoted_01()
        {
            // Arrange.
            const string query = @"time:['\d4-\d2-\d2 \d2:\d2']";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsType<RootedPathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.Equal("time", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Root);
            Assert.IsType<RegexPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(0));
            Assert.Equal(@"\d4-\d2-\d2 \d2:\d2", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(0).Cast<RegexPathSubjectPart>().First().Regex);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Regex_Time_Double_Quoted_02()
        {
            // Arrange.
            const string query = @"time:['\d4-\d2-\d2 \d2:\d2']/Person/['\w']/['\w']";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsType<RootedPathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.Equal("time", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Root);
            Assert.IsType<RegexPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(0));
            Assert.Equal(@"\d4-\d2-\d2 \d2:\d2", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(0).Cast<RegexPathSubjectPart>().First().Regex);
            Assert.IsType<RegexPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(4));
            Assert.Equal(@"\w", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(4).Cast<RegexPathSubjectPart>().First().Regex);
            Assert.IsType<RegexPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(6));
            Assert.Equal(@"\w", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(6).Cast<RegexPathSubjectPart>().First().Regex);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Regex_Time_Single_Quoted_01()
        {
            // Arrange.
            const string query = "time:[\"\\d4-\\d2-\\d2 \\d2:\\d2\"]";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsType<RootedPathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.Equal("time", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Root);
            Assert.IsType<RegexPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(0));
            Assert.Equal(@"\d4-\d2-\d2 \d2:\d2", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(0).Cast<RegexPathSubjectPart>().First().Regex);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Regex_Time_Single_Quoted_02()
        {
            // Arrange.
            const string query = "time:[\"\\d4-\\d2-\\d2 \\d2:\\d2\"]/Person/['\\w']/['\\w']";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsType<RootedPathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.Equal("time", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Root);
            Assert.IsType<RegexPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(0));
            Assert.Equal(@"\d4-\d2-\d2 \d2:\d2", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(0).Cast<RegexPathSubjectPart>().First().Regex);
            Assert.IsType<RegexPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(4));
            Assert.Equal(@"\w", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(4).Cast<RegexPathSubjectPart>().First().Regex);
            Assert.IsType<RegexPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.ElementAt(6));
            Assert.Equal(@"\w", script.Sequences.First().Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(6).Cast<RegexPathSubjectPart>().First().Regex);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Typed_BaadFood()
        {
            // Arrange.
            const string query = "/First/Second/[BaadFood]/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            Assert.Null(result.Script);
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Query_And_VariableAssignment_01()
        {
            // Arrange.
            const string query = "First:Second/Third/Fourth\r\n$var1 <= /Fourth/4\r\n/Fifth/5\r\n/Sixth/6";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsType<RootedPathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<VariableSubject>(script.Sequences.ElementAt(1).Parts.ElementAt(0));
            Assert.IsType<AssignOperator>(script.Sequences.ElementAt(1).Parts.ElementAt(1));
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(1).Parts.ElementAt(2));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_RootedPath_With_Query_And_VariableAssignment_02()
        {
            // Arrange.
            const string query = "First:Second/Third/Fourth\r\n$var1 <= Fourth:4\r\n/Fifth/5\r\n/Sixth/6";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsType<RootedPathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<VariableSubject>(script.Sequences.ElementAt(1).Parts.ElementAt(0));
            Assert.IsType<AssignOperator>(script.Sequences.ElementAt(1).Parts.ElementAt(1));
            Assert.IsType<RootedPathSubject>(script.Sequences.ElementAt(1).Parts.ElementAt(2));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Query_RootedPath_With_Separator_Error()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("First:Second//Third/Fourth");

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var subject = sequence.Parts.Skip(1).Cast<RootedPathSubject>().First();

            Assert.IsType<ConstantPathSubjectPart>(subject.Parts.ElementAt(0));
            Assert.IsType<AllParentsPathSubjectPart>(subject.Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(subject.Parts.ElementAt(2));
            Assert.IsType<ParentPathSubjectPart>(subject.Parts.ElementAt(3));
            Assert.IsType<ConstantPathSubjectPart>(subject.Parts.ElementAt(4));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Unquoted_RootedPath_With_Normal_Characters()
        {
            // Arrange.
            var count = 0;

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = $"ThirdIs{c}Cool";
                var query = $"First:Second/{symbol}/Fourth";
                var script = _parser.Parse(query).Script;
                count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in _normalCharacters)
            {
                act(character);
            }
            Assert.NotEqual(0, count);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Quoted_RootedPath_With_Normal_Characters()
        {
            // Arrange.
            var count = 0;

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = $"\"ThirdIs{c}Cool\"";
                var query = $"First:Second/{symbol}/Fourth";
                var script = _parser.Parse(query).Script;
                count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in _normalCharacters)
            {
                act(character);
            }
            Assert.NotEqual(0, count);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Quoted_RootedPath_With_Special_Characters()
        {
            // Arrange.
            var count = 0;

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = $"\"ThirdIs{c}Cool\"";
                var query = $"First:Second/{symbol}/Fourth";
                var script = _parser.Parse(query).Script;
                count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in _specialCharacters)
            {
                act(character);
            }
            act('/'); // Let's also check the separator charachter.
            Assert.NotEqual(0, count);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Unquoted_RootedPath_With_Special_Characters()
        {
            // Arrange.

            // Act.
            var act = new Func<char, ScriptParseResult>(c =>
            {
                var symbol = $"ThirdIsNot{c}Cool";
                var query = $"First:Second/{symbol}/Fourth";
                return _parser.Parse(query);
            });

            // Assert.
            foreach (var character in _specialCharacters)
            {
                var result = act(character);
                Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Unquoted_RootedPath_With_Quoted_Special_Characters()
        {
            // Arrange.

            // Act.
            var act = new Func<char, ScriptParseResult>(c =>
            {
                var symbol = $"\"ThirdIsNot{c}Cool\"";
                var query = $"First:Second/{symbol}/Fourth";
                return _parser.Parse(query);
            });

            // Assert.
            foreach (var character in _specialCharacters2)
            {
                var result = act(character);
                Assert.DoesNotContain(result.Errors, e => e.Exception is ScriptParserException);
            }
        }


    }
}
