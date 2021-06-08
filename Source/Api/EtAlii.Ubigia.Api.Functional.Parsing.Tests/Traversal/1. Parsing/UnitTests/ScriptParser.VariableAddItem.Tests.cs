namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableAddItem_Without_File()
        {
            // Arrange.
            var query = "$v0 <= /Documents/Files+=/Images";

            // Act.
            var script = _parser.Parse(query).Script;

            // Assert.
            var sequence = script.Sequences.First();
            Assert.Equal("v0", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Files", sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.Skip(3).First());
            Assert.IsType<ParentPathSubjectPart>(sequence.Parts.Skip(4).Cast<AbsolutePathSubject>().First().Parts.First());
            Assert.Equal("Images", sequence.Parts.Skip(4).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_VariableAddItem_Without_File_Spaced()
        {
            // Arrange.
            var query = "$v0 <= /Documents/Files += /Images";

            // Act.
            var script = _parser.Parse(query).Script;

            // Assert.
            var sequence = script.Sequences.First();
            Assert.Equal("v0", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Files", sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.Skip(3).First());
            Assert.IsType<ParentPathSubjectPart>(sequence.Parts.Skip(4).Cast<AbsolutePathSubject>().First().Parts.First());
            Assert.Equal("Images", sequence.Parts.Skip(4).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        }
    }
}
