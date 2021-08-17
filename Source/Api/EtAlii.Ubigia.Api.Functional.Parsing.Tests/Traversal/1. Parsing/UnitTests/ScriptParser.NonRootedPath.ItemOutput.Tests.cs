// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserNonRootedPathTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_ItemOutput_With_Variable_Name()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/$Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Third", sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<VariablePathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_ItemOutput_With_Component_Count_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal(8, ((AbsolutePathSubject)sequence.Parts.ElementAt(1)).Parts.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_ItemOutput_With_Quoted_Name()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/\"Third is cool\"/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Third is cool", ((AbsolutePathSubject)sequence.Parts.ElementAt(1)).Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_ItemOutput_With_Quoted_Name_Special_Characters()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/\"Third is cool äëöüáéóúâêôû\"/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Third is cool äëöüáéóúâêôû", ((AbsolutePathSubject)sequence.Parts.ElementAt(1)).Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_ItemOutput_With_Component_Count_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second\r\n/Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var firstSequence = script.Sequences.ElementAt(0);
            Assert.Equal(4, firstSequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Length);
            var secondSequence = script.Sequences.ElementAt(1);
            Assert.Equal(4, secondSequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_ItemOutput_With_Component_Name_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Fourth", ((ConstantPathSubjectPart)((AbsolutePathSubject)sequence.Parts.ElementAt(1)).Parts.Skip(7).First()).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_ItemOutput_With_Component_Name_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second\r\n/Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.Skip(1).First();
            Assert.Equal("Fourth", sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_ItemOutput_With_Variable()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "/First/Second/$Third/Fourth";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.IsType<VariablePathSubjectPart>(sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(5).First());
        }
    }
}
