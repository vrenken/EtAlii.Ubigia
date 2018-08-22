// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
    using Xunit;

    public partial class ScriptParserNonRootedPathTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Traverse_All_Hierarchical_Childs()
        {
            // Arrange.
            const string query = "/2018/08/22//";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var subject = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First();
            Assert.Equal("2018", subject.Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(1).First());
            Assert.Equal("08", subject.Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(3).First());
            Assert.Equal("22", subject.Parts.Skip(4).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AllChildPathSubjectPart>(subject.Parts.Skip(5).First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Traverse_All_Hierarchical_Parents()
        {
            // Arrange.
            const string query = "/2018/08/22\\\\";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var subject = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First();
            Assert.Equal("2018", subject.Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(1).First());
            Assert.Equal("08", subject.Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(3).First());
            Assert.Equal("22", subject.Parts.Skip(4).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AllParentOfPathSubjectPart>(subject.Parts.Skip(5).First());
        }
        
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Traverse_All_Sequential_Next()
        {
            // Arrange.
            const string query = "/Device/Canon/BT342/Feed/123>>";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var subject = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First();
            Assert.Equal("Media", subject.Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(1).First());
            Assert.Equal("Canon", subject.Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(3).First());
            Assert.Equal("BT342", subject.Parts.Skip(4).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(5).First());
            Assert.Equal("Feed", subject.Parts.Skip(6).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(7).First());
            Assert.Equal("123", subject.Parts.Skip(8).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AllPreviousPathSubjectPart>(subject.Parts.Skip(9).First());
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_Parse_Traverse_All_Sequential_Previous()
        {
            // Arrange.
            const string query = "/Device/Canon/BT342/Feed/123<<";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var subject = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First();
            Assert.Equal("Media", subject.Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(1).First());
            Assert.Equal("Canon", subject.Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(3).First());
            Assert.Equal("BT342", subject.Parts.Skip(4).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(5).First());
            Assert.Equal("Feed", subject.Parts.Skip(6).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<IsChildOfPathSubjectPart>(subject.Parts.Skip(7).First());
            Assert.Equal("123", subject.Parts.Skip(8).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AllNextOfPathSubjectPart>(subject.Parts.Skip(9).First());
        }
   }
}