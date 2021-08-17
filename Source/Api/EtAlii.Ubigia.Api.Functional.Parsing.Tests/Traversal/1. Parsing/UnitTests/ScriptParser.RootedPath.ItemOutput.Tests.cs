// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserRootedPathTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_ItemOutput_With_Variable_Name()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "First:Second/$Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Third", sequence.Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(2).Cast<VariablePathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_ItemOutput_With_Component_Count_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "First:Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal(5, ((RootedPathSubject)sequence.Parts.ElementAt(1)).Parts.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_ItemOutput_With_Quoted_Name()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "First:Second/\"Third is cool\"/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Third is cool", ((RootedPathSubject)sequence.Parts.ElementAt(1)).Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_ItemOutput_With_Quoted_Name_Special_Characters()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "First:Second/\"Third is cool äëöüáéóúâêôû\"/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Third is cool äëöüáéóúâêôû", ((RootedPathSubject)sequence.Parts.ElementAt(1)).Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_ItemOutput_With_Component_Count_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "First:Second\r\nThird:Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var firstSequence = script.Sequences.ElementAt(0);
            Assert.Single(firstSequence.Parts.Skip(1).Cast<RootedPathSubject>().First().Parts);
            var secondSequence = script.Sequences.ElementAt(1);
            Assert.Single(secondSequence.Parts.Skip(1).Cast<RootedPathSubject>().First().Parts);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_ItemOutput_With_Component_Name_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "First:Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Fourth", ((ConstantPathSubjectPart)((RootedPathSubject)sequence.Parts.ElementAt(1)).Parts.Skip(4).First()).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_ItemOutput_With_Component_Name_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "First:Second\r\nThird:Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.Skip(1).First();
            Assert.Equal("Fourth", sequence.Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_RootedPath_ItemOutput_With_Variable()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "First:Second/$Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.IsType<VariablePathSubjectPart>(sequence.Parts.Skip(1).Cast<RootedPathSubject>().First().Parts.Skip(2).First());
        }
    }
}
