﻿namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Xunit;


    public partial class ScriptParser_NonRootedPath_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path()
        {
            // Arrange.
            const string query = "/First/Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Equal(1, script.Sequences.Count());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path_With_Query()
        {
            // Arrange.
            const string query = "/First/Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.First().Parts.Skip(1).First());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path_With_Typed_Word()
        {
            // Arrange.
            const string query = "/First/Second/[Word]/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
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
            const string query = "/First/Second/[word]/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
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
            const string query = "/First/Second/[WORD]/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
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
            const string query = "/First/Second/[NUMBER]/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
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
            const string query = "/First/Second/[BaadFood]/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Path_With_Query_And_VariableAssignment()
        {
            // Arrange.
            const string query = "/First/Second/Third/Fourth\r\n$var1 <= /Fourth/4\r\n/Fifth/5\r\n/Sixth/6";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(0).Parts.Skip(1).First());
            Assert.IsType<VariableSubject>(script.Sequences.ElementAt(1).Parts.ElementAt(0));
            Assert.IsType<AssignOperator>(script.Sequences.ElementAt(1).Parts.ElementAt(1));
            Assert.IsType<AbsolutePathSubject>(script.Sequences.ElementAt(1).Parts.ElementAt(2));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Query_Path_With_Separator_Error()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("/First/Second//Third/Fourth");

            // Assert.
            Assert.True(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Query_Unquoted_Path_With_Normal_Characters()
        {
            // Arrange.

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = String.Format("ThirdIs{0}Cool", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
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
        public void ScriptParser_NonRootedPath_Parse_Query_Quoted_Path_With_Normal_Characters()
        {
            // Arrange.

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = String.Format("\"ThirdIs{0}Cool\"", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
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
        public void ScriptParser_NonRootedPath_Parse_Query_Quoted_Path_With_Special_Characters()
        {
            // Arrange.

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = String.Format("\"ThirdIs{0}Cool\"", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
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
        public void ScriptParser_NonRootedPath_Parse_Query_Unquoted_Path_With_Special_Characters()
        {
            // Arrange.

            // Act.
            var act = new Func<char, ScriptParseResult>(c =>
            {
                var symbol = String.Format("ThirdIsNot{0}Cool", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
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
        public void ScriptParser_NonRootedPath_Parse_Query_Unquoted_Path_With_Quoted_Special_Characters()
        {
            // Arrange.

            // Act.
            var act = new Func<char, ScriptParseResult>(c =>
            {
                var symbol = String.Format("\"ThirdIsNot{0}Cool\"", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
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