// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserNonRootedPathTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Without_File()
        {
            // Arrange.
            const string query = "/Documents/Files+=/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Files", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal("Images", sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Unquoted()
        {
            // Arrange.
            const string query = "/Documents/Files+=Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Files", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal("Images", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Quoted_00()
        {
            // Arrange.
            const string query = "/Person += \"Doe\"";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Person", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Quoted_01()
        {
            // Arrange.
            const string query = "/Person += \"Doe\"/\"John\"";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Person", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<RelativePathSubject>().First().Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<RelativePathSubject>().First().Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Quoted_02()
        {
            // Arrange.
            const string query = "/Person += 'Doe'/'John'";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Person", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<RelativePathSubject>().First().Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<RelativePathSubject>().First().Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Without_File_Spaced()
        {
            // Arrange.
            const string query = "/Documents/Files += /Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Files", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            Assert.IsType<ParentPathSubjectPart>(sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.First());
            Assert.Equal("Images", sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Single()
        {
            // Arrange.
            const string query = "/Documents/Files += Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Files", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal("Images", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Single_Quoted()
        {
            // Arrange.
            const string query = "/Documents/Files += \"Images\"";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("Files", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal("Images", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Single_Quoted_Special_Characters()
        {
            // Arrange.
            const string query = "/Documents/Files += \"Images äëöüáéóúâêôû\"";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.IsType<ParentPathSubjectPart>(sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.First());
            Assert.Equal("Files", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal("Images äëöüáéóúâêôû", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Path()
        {
            // Arrange.
            const string query = "/Documents/Files/Images+=/Vacation/Italy/Tuscany";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var firstSubject = (AbsolutePathSubject)sequence.Parts.ElementAt(0);
            Assert.IsType<ParentPathSubjectPart>(firstSubject.Parts.ElementAt(0));
            Assert.Equal("Documents", firstSubject.Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<ParentPathSubjectPart>(firstSubject.Parts.ElementAt(2));
            Assert.Equal("Files", firstSubject.Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<ParentPathSubjectPart>(firstSubject.Parts.ElementAt(4));
            Assert.Equal("Images", firstSubject.Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var secondSubject = sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First();
            Assert.IsType<ParentPathSubjectPart>(secondSubject.Parts.ElementAt(0));
            Assert.Equal("Vacation", secondSubject.Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<ParentPathSubjectPart>(secondSubject.Parts.ElementAt(2));
            Assert.Equal("Italy", secondSubject.Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<ParentPathSubjectPart>(secondSubject.Parts.ElementAt(4));
            Assert.Equal("Tuscany", secondSubject.Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Path_Spaced()
        {
            // Arrange.
            const string query = "/Documents/Files/Images += /Vacation/Italy/Tuscany";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();

            Assert.Equal("Documents", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Files", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Images", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Vacation", sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Italy", sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Tuscany", sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Path_Non_Rooted()
        {
            // Arrange.
            const string query = "/Documents/Files/Images+=Vacation/Italy/Tuscany";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();

            Assert.Equal("Documents", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Files", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Images", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Vacation", sequence.Parts.Skip(2).Cast<RelativePathSubject>().First().Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Italy", sequence.Parts.Skip(2).Cast<RelativePathSubject>().First().Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Tuscany", sequence.Parts.Skip(2).Cast<RelativePathSubject>().First().Parts.Skip(4).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_NonRootedPath_AddItem_Path_Non_Rooted_Spaced()
        {
            // Arrange.
            const string query = "/Documents/Files/Images += Vacation/Italy/Tuscany";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();

            Assert.Equal("Documents", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Files", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Images", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Vacation", sequence.Parts.Skip(2).Cast<RelativePathSubject>().First().Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Italy", sequence.Parts.Skip(2).Cast<RelativePathSubject>().First().Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Tuscany", sequence.Parts.Skip(2).Cast<RelativePathSubject>().First().Parts.Skip(4).Cast<ConstantPathSubjectPart>().First().Name);
        }


   }
}
