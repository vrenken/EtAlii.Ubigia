// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact]
        public void ScriptParser_RootedPath_Tags_Assign()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "Person:Doe/John# <= FirstName";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();

            Assert.IsType<RootedPathSubject>(sequence.Parts.Skip(0).First());
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("Person", rootedPathSubject.Root);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts.ElementAt(0));
            Assert.IsType<TaggedPathSubjectPart>(rootedPathSubject.Parts.ElementAt(2));
            var taggedPathSubjectPart = rootedPathSubject.Parts.Skip(2).Cast<TaggedPathSubjectPart>().First();
            Assert.Equal("John", taggedPathSubjectPart.Name);
            Assert.Equal(string.Empty, taggedPathSubjectPart.Tag);

            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());

            Assert.IsType<StringConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal("FirstName", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact]
        public void ScriptParser_RootedPath_Tags_Assign_With_Comment()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "Person:Doe/John# <= FirstName --A comment";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();

            Assert.IsType<RootedPathSubject>(sequence.Parts.Skip(0).First());
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("Person", rootedPathSubject.Root);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts.ElementAt(0));
            Assert.IsType<TaggedPathSubjectPart>(rootedPathSubject.Parts.ElementAt(2));
            var taggedPathSubjectPart = rootedPathSubject.Parts.Skip(2).Cast<TaggedPathSubjectPart>().First();
            Assert.Equal("John", taggedPathSubjectPart.Name);
            Assert.Equal(string.Empty, taggedPathSubjectPart.Tag);

            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());

            Assert.IsType<StringConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal("FirstName", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact]
        public void ScriptParser_RootedPath_Tags_Query()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "Person:Doe/John#";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();

            Assert.IsType<RootedPathSubject>(sequence.Parts.Skip(1).First());
            var rootedPathSubject = sequence.Parts.Skip(1).Cast<RootedPathSubject>().First();
            Assert.Equal("Person", rootedPathSubject.Root);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts.ElementAt(0));
            Assert.IsType<TaggedPathSubjectPart>(rootedPathSubject.Parts.ElementAt(2));
            var taggedPathSubjectPart = rootedPathSubject.Parts.Skip(2).Cast<TaggedPathSubjectPart>().First();
            Assert.Equal("John", taggedPathSubjectPart.Name);
            Assert.Equal(string.Empty, taggedPathSubjectPart.Tag);
        }

        [Fact]
        public void ScriptParser_RootedPath_Tags_Filter()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string query = "Person:Doe/#FirstName";

            // Act.
            var result = _parser.Parse(query, scope);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();

            Assert.IsType<RootedPathSubject>(sequence.Parts.Skip(1).First());
            var rootedPathSubject = sequence.Parts.Skip(1).Cast<RootedPathSubject>().First();
            Assert.Equal("Person", rootedPathSubject.Root);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts.ElementAt(0));
            Assert.IsType<TaggedPathSubjectPart>(rootedPathSubject.Parts.ElementAt(2));
            var taggedPathSubjectPart = rootedPathSubject.Parts.Skip(2).Cast<TaggedPathSubjectPart>().First();
            Assert.Equal(string.Empty, taggedPathSubjectPart.Name);
            Assert.Equal("FirstName", taggedPathSubjectPart.Tag);
        }
    }
}
