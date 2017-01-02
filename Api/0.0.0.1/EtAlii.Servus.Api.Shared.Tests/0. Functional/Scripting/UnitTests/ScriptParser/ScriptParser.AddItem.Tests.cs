// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class ScriptParser_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Without_File()
        {
            // Arrange.
            const string query = "/Documents/Files+=/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));
            Assert.AreEqual("Images", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Quoted_01()
        {
            // Arrange.
            const string query = "/Contacts += \"Doe\"/\"John\"";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Contacts", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Quoted_02()
        {
            // Arrange.
            const string query = "/Contacts += 'Doe'/'John'";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Contacts", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Without_File_Spaced()
        {
            // Arrange.
            const string query = "/Documents/Files += /Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.First(), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Images", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Single()
        {
            // Arrange.
            const string query = "/Documents/Files += Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.First(), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Images", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Single_Quoted()
        {
            // Arrange.
            const string query = "/Documents/Files += \"Images\"";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.First(), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Images", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Single_Quoted_Special_Characters()
        {
            // Arrange.
            const string query = "/Documents/Files += \"Images äëöüáéóúâêôû\"";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.First(), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Images äëöüáéóúâêôû", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Path()
        {
            // Arrange.
            const string query = "/Documents/Files/Images+=/Vacation/Italy/Tuscany";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var firstSubject = (PathSubject)sequence.Parts.ElementAt(0);
            Assert.IsInstanceOfType(firstSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Documents", firstSubject.Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(firstSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Files", firstSubject.Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(firstSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Images", firstSubject.Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));
            var secondSubject = sequence.Parts.Skip(2).Cast<PathSubject>().First();
            Assert.IsInstanceOfType(secondSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Vacation", secondSubject.Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(secondSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Italy", secondSubject.Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(secondSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Tuscany", secondSubject.Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Path_Spaced()
        {
            // Arrange.
            const string query = "/Documents/Files/Images += /Vacation/Italy/Tuscany";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();

            Assert.AreEqual("Documents", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Images", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Vacation", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Italy", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Tuscany", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Path_Non_Rooted()
        {
            // Arrange.
            const string query = "/Documents/Files/Images+=Vacation/Italy/Tuscany";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();

            Assert.AreEqual("Documents", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Images", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Vacation", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Italy", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Tuscany", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(4).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Path_Non_Rooted_Spaced()
        {
            // Arrange.
            const string query = "/Documents/Files/Images += Vacation/Italy/Tuscany";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();

            Assert.AreEqual("Documents", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Images", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Vacation", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(0).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Italy", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(2).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.AreEqual("Tuscany", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(4).Cast<ConstantPathSubjectPart>().First().Name);
        }
    }
}