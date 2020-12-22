namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableStringAssignment()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$var1 <= \"Test\"").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableStringAssignment_Without_Spaces()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$var1<=\"Test\"").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableStringAssignment_Next()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1 <= \"Test\"").Script;

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableStringAssignment_Next_Without_Spaces()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1<=\"Test\"").Script;

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableStringAssignment_Middle()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1 <= \"Test\"\r\n/Test3/Test4").Script;

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableStringAssignment_Middle_Without_Spaces()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1<=\"Test\"\r\n/Test3/Test4").Script;

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.Equal("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }
    }
}
