﻿namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System.Linq;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Tags_Assign()
        {
            // Arrange.
            const string query = "/Person/Doe/John# <= FirstName";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();

            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            var absolutePathSubject = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First();
            Assert.IsType<ConstantPathSubjectPart>(absolutePathSubject.Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(absolutePathSubject.Parts.ElementAt(3));
            Assert.IsType<TaggedPathSubjectPart>(absolutePathSubject.Parts.ElementAt(5));
            var taggedPathSubjectPart = absolutePathSubject.Parts.Skip(5).Cast<TaggedPathSubjectPart>().First();
            Assert.Equal("John", taggedPathSubjectPart.Name);
            Assert.Equal(string.Empty, taggedPathSubjectPart.Tag);

            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());

            Assert.IsType<StringConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal("FirstName", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Tags_Assign_With_Comment()
        {
            // Arrange.
            const string query = "/Person/Doe/John# <= FirstName --A comment";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();

            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            var absolutePathSubject = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First();
            Assert.IsType<ConstantPathSubjectPart>(absolutePathSubject.Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(absolutePathSubject.Parts.ElementAt(3));
            Assert.IsType<TaggedPathSubjectPart>(absolutePathSubject.Parts.ElementAt(5));
            var taggedPathSubjectPart = absolutePathSubject.Parts.Skip(5).Cast<TaggedPathSubjectPart>().First();
            Assert.Equal("John", taggedPathSubjectPart.Name);
            Assert.Equal(string.Empty, taggedPathSubjectPart.Tag);

            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());

            Assert.IsType<StringConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal("FirstName", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Tags_Query()
        {
            // Arrange.
            const string query = "/Person/Doe/John#";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();

            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(1).First());
            var absolutePathSubject = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First();
            Assert.IsType<ConstantPathSubjectPart>(absolutePathSubject.Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(absolutePathSubject.Parts.ElementAt(3));
            Assert.IsType<TaggedPathSubjectPart>(absolutePathSubject.Parts.ElementAt(5));
            var taggedPathSubjectPart = absolutePathSubject.Parts.Skip(5).Cast<TaggedPathSubjectPart>().First();
            Assert.Equal("John", taggedPathSubjectPart.Name);
            Assert.Equal(string.Empty, taggedPathSubjectPart.Tag);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Tags_Filter()
        {
            // Arrange.
            const string query = "/Person/Doe/#FirstName";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();

            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(1).First());
            var absolutePathSubject = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First();
            Assert.IsType<ConstantPathSubjectPart>(absolutePathSubject.Parts.ElementAt(1));
            Assert.IsType<ConstantPathSubjectPart>(absolutePathSubject.Parts.ElementAt(3));
            Assert.IsType<TaggedPathSubjectPart>(absolutePathSubject.Parts.ElementAt(5));
            var taggedPathSubjectPart = absolutePathSubject.Parts.Skip(5).Cast<TaggedPathSubjectPart>().First();
            Assert.Equal(string.Empty, taggedPathSubjectPart.Name);
            Assert.Equal("FirstName", taggedPathSubjectPart.Tag);
        }

    }
}