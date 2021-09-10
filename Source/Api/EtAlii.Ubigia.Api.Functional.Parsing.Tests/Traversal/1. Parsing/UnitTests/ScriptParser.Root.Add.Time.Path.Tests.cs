// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact]
        public void ScriptParser_Root_Add_Time_MMDDHHMMSS_Path_Absolute()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/time/2016 += /12/04/13/38/22";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var absolutePathSubject = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First();
            Assert.Equal("/time/2016", absolutePathSubject.ToString());
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("/12/04/13/38/22", pathSubject.ToString());
        }

        [Fact]
        public void ScriptParser_Root_Add_Time_MMDDHHMMSS_Path_Relative()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/time/2016 += 12/04/13/38/22";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var absolutePathSubject = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First();
            Assert.Equal("/time/2016", absolutePathSubject.ToString());
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<RelativePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("12/04/13/38/22", pathSubject.ToString());
        }

        [Fact]
        public void ScriptParser_Root_Add_Time_DDHHMMSS_Path_Absolute()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/time/2016/12 += /04/13/38/22";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var absolutePathSubject = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First();
            Assert.Equal("/time/2016/12", absolutePathSubject.ToString());
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("/04/13/38/22", pathSubject.ToString());
        }

        [Fact]
        public void ScriptParser_Root_Add_Time_DDHHMMSS_Path_Relative()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/time/2016/12 += 04/13/38/22";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var absolutePathSubject = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First();
            Assert.Equal("/time/2016/12", absolutePathSubject.ToString());
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<RelativePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("04/13/38/22", pathSubject.ToString());
        }
    }
}
