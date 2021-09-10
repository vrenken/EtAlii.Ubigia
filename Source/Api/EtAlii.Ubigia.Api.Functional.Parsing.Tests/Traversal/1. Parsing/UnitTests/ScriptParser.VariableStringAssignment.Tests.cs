// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact]
        public void ScriptParser_VariableStringAssignment()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("$var1 <= \"Test\"", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact]
        public void ScriptParser_VariableStringAssignment_Without_Spaces()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("$var1<=\"Test\"", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact]
        public void ScriptParser_VariableStringAssignment_Next()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1 <= \"Test\"", scope).Script;

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact]
        public void ScriptParser_VariableStringAssignment_Next_Without_Spaces()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1<=\"Test\"", scope).Script;

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact]
        public void ScriptParser_VariableStringAssignment_Middle()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1 <= \"Test\"\r\n/Test3/Test4", scope).Script;

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact]
        public void ScriptParser_VariableStringAssignment_Middle_Without_Spaces()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1<=\"Test\"\r\n/Test3/Test4", scope).Script;

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }
    }
}
