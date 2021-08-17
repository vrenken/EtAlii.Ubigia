// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_UnAssign_Time_Root()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "root:time <= ";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("time", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal(2,sequence.Parts.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_UnAssign_Time_Root_Under_Other_Name()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "root:specialtime <= ";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("specialtime", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal(2, sequence.Parts.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_UnAssign_Object_Root()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "root:projects <= ";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("projects", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal(2, sequence.Parts.Length);
        }
    }
}
