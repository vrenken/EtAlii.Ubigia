// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public partial class ScriptParserTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableItemAssignment_With_Variable()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 <= /Fourth/4\r\n/Fifth/5\r\n/Sixth/6").Script;

            // Assert.
            Assert.Equal(4, script.Sequences.Count());
            var sequence = script.Sequences.Skip(1).First();
            Assert.Equal("var1", ((VariableSubject)sequence.Parts.Skip(0).First()).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableItemAssignment_With_Variable_Without_Spaces()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth\r\n$var1<=/Fourth/4\r\n/Fifth/5\r\n/Sixth/6").Script;

            // Assert.
            var sequence = script.Sequences.ElementAt(1);
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableItemAssignment_With_Path()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 <= /Fourth/4\r\n/Fifth/5\r\n/Sixth/6").Script;

            // Assert.
            var sequence = script.Sequences.ElementAt(1);
            Assert.Equal("4", sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableItemAssignment_With_Path_Without_Spaces()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth\r\n$var1<=/Fourth/4\r\n/Fifth/5\r\n/Sixth/6").Script;

            // Assert.
            var sequence = script.Sequences.ElementAt(1);
            Assert.Equal("4", sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        }
    }
}
