// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserNonRootedPathTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path_With_Query()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.First().Parts.Skip(1).First());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path_With_Typed_Word()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/[Word]/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.DumpAsString());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(3));
            Assert.IsType<TypedPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(5));
            Assert.Equal("WORD", script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<TypedPathSubjectPart>().First().Type);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(7));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path_With_Typed_word()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/[word]/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.DumpAsString());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(3));
            Assert.IsType<TypedPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(5));
            Assert.Equal("WORD", script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<TypedPathSubjectPart>().First().Type);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(7));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path_With_Typed_WORD()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/[WORD]/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.DumpAsString());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(3));
            Assert.IsType<TypedPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(5));
            Assert.Equal("WORD", script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<TypedPathSubjectPart>().First().Type);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(7));
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path_With_Typed_NUMBER()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/[NUMBER]/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.DumpAsString());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.First().Parts.Skip(1).First());
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(3));
            Assert.IsType<TypedPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(5));
            Assert.Equal("NUMBER", script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<TypedPathSubjectPart>().First().Type);
            Assert.IsType<ConstantPathSubjectPart>(script.Sequences.First().Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.ElementAt(7));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path_With_Typed_BaadFood()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/[BaadFood]/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            Assert.Null(result.Script);
            Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path_With_Query_And_VariableAssignment()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/Third/Fourth\r\n$var1 <= /Fourth/4\r\n/Fifth/5\r\n/Sixth/6";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<VariableSubject>(script.Sequences.ElementAt(1).Parts.ElementAt(0));
            Assert.IsType<AssignOperator>(script.Sequences.ElementAt(1).Parts.ElementAt(1));
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(1).Parts.ElementAt(2));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Query_Path_With_Separator_No_Error()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var result = _parser.Parse("/First/Second//Third/Fourth", scope);

            // Assert.
            Assert.DoesNotContain(result.Errors, e => e.Exception is ScriptParserException);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Query_Unquoted_Path_With_Normal_Characters()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var count = 0;

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = $"ThirdIs{c}Cool";
                var query = $"/First/Second/{symbol}/Fourth";
                var script = _parser.Parse(query, scope).Script;
                count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in _normalCharacters)
            {
                act(character);
                Assert.NotEqual(0, count);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Query_Quoted_Path_With_Normal_Characters()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var count = 0;

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = $"\"ThirdIs{c}Cool\"";
                var query = $"/First/Second/{symbol}/Fourth";
                var script = _parser.Parse(query, scope).Script;
                count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in _normalCharacters)
            {
                act(character);
                Assert.NotEqual(0, count);
            }
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Query_Quoted_Path_With_Special_Characters()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var count = 0;

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = $"\"ThirdIs{c}Cool\"";
                var query = $"/First/Second/{symbol}/Fourth";
                var script = _parser.Parse(query, scope).Script;
                count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in _specialCharacters)
            {
                act(character);
                Assert.NotEqual(0, count);
            }
            act('/'); // Let's also check the separator character.
            Assert.NotEqual(0, count);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Query_Unquoted_Path_With_Special_Characters()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var act = new Func<char, ScriptParseResult>(c =>
            {
                var symbol = $"ThirdIsNot{c}Cool";
                var query = $"/First/Second/{symbol}/Fourth";
                return _parser.Parse(query, scope);
            });

            // Assert.
            foreach (var character in _specialCharacters)
            {
                var result = act(character);
                Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Query_Unquoted_Path_With_Quoted_Special_Characters()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var act = new Func<char, ScriptParseResult>(c =>
            {
                var symbol = $"\"ThirdIsNot{c}Cool\"";
                var query = $"/First/Second/{symbol}/Fourth";
                return _parser.Parse(query, scope);
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
