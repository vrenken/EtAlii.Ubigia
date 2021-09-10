// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact]
        public void ScriptParser_Root_Add_Time_MMDDHHMMSS_String_NoQuotes_Absolute()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "time:2016 += /12/04/13/38/22";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("2016", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("/12/04/13/38/22", pathSubject.ToString());
        }

        [Fact]
        public void ScriptParser_Root_Add_Time_MMDDHHMMSS_String_NoQuotes_Relative()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "time:2016 += 12/04/13/38/22";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("2016", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<RelativePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("12/04/13/38/22", pathSubject.ToString());
        }

        [Fact]
        public void ScriptParser_Root_Add_Time_DDHHMMSS_String_NoQuotes_Absolute()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "time:201612 += /04/13/38/22";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("201612", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("/04/13/38/22", pathSubject.ToString());
        }

        [Fact]
        public void ScriptParser_Root_Add_Time_DDHHMMSS_String_NoQuotes_Relative()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "time:201612 += 04/13/38/22";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("201612", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<RelativePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("04/13/38/22", pathSubject.ToString());
        }


        [Fact]
        public void ScriptParser_Root_Add_Time_SS_String_NoQuotes_Absolute()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "time:201612041338 += /22";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("201612041338", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("/22", pathSubject.ToString());
        }

        [Fact]
        public void ScriptParser_Root_Add_Time_SS_String_NoQuotes_Relative()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "time:201612041338 += 22";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("201612041338", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<StringConstantSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("22", pathSubject.Value);
        }
    }
}
