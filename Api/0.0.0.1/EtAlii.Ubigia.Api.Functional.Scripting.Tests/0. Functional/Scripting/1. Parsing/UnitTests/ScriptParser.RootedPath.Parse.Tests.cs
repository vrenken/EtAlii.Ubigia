﻿namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Tests;
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
            Assert.Equal(1, script.Sequences.Count());
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
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
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
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
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
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
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
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
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
            var script = result.Script;
            Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
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
            Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Unquoted_RootedPath_With_Normal_Characters()
        {
            // Arrange.

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = $"ThirdIs{c}Cool";
                var query = $"First:Second/{symbol}/Fourth";
                var script = _parser.Parse(query).Script;
                var count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in NormalCharacters)
            {
                act(character);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Quoted_RootedPath_With_Normal_Characters()
        {
            // Arrange.

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = $"\"ThirdIs{c}Cool\"";
                var query = $"First:Second/{symbol}/Fourth";
                var script = _parser.Parse(query).Script;
                var count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in NormalCharacters)
            {
                act(character);
            }
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Quoted_RootedPath_With_Special_Characters()
        {
            // Arrange.

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = $"\"ThirdIs{c}Cool\"";
                var query = $"First:Second/{symbol}/Fourth";
                var script = _parser.Parse(query).Script;
                var count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in SpecialCharacters)
            {
                act(character);
            }
            act('/'); // Let's also check the separator charachter.
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
            foreach (var character in SpecialCharacters)
            {
                var result = act(character);
                Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
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
            foreach (var character in SpecialCharacters2)
            {
                var result = act(character);
                Assert.False(result.Errors.Any(e => e.Exception is ScriptParserException));
            }
        }


    }
}