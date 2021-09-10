// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact]
        public void ScriptParser_VariableOutput()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("$var1 <= /Fourth/4\r\n/Fifth/5\r\n/Sixth/6\r\n$var1", scope).Script;

            // Assert.
            Assert.Equal(4, script.Sequences.Count());
            var sequence = script.Sequences.ElementAt(3);
            Assert.Equal("var1", ((VariableSubject)sequence.Parts.Skip(1).First()).Name);
        }

        [Fact]
        public void ScriptParser_VariableOutput_Without_Spaces()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("$var1<=/Fourth/4\r\n/Fifth/5\r\n/Sixth/6\r\n$var1", scope).Script;

            // Assert.
            Assert.Equal(4, script.Sequences.Count());
            var sequence = script.Sequences.ElementAt(3);
            Assert.Equal("var1", ((VariableSubject)sequence.Parts.Skip(1).ElementAt(0)).Name);
        }
    }
}
