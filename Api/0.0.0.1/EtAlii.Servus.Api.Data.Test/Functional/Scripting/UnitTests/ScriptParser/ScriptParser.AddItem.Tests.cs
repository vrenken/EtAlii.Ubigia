namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Model;
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    public partial class ScriptParser_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Without_File()
        {
            // Arrange.
            const string query = "/Documents/Files+=/Images";

            // Act.
            var script = _parser.Parse(query);

            // Assert.
            var sequence = script.Sequences.First();
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));
            Assert.AreEqual("Images", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Without_File_Spaced()
        {
            // Arrange.
            const string query = "/Documents/Files += /Images";

            // Act.
            var script = _parser.Parse(query);

            // Assert.
            var sequence = script.Sequences.First();
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.First(), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Images", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Path()
        {
            // Arrange.
            const string query = "/Documents/Files/Images+=/Vacation/Italy/Tuscany";

            // Act.
            var script = _parser.Parse(query);

            // Assert.
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
            var script = _parser.Parse(query);

            // Assert.
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
            var act = new System.Action(() =>
            {
                var script = _parser.Parse(query);
                var count = script.Sequences.Count();
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_AddItem_Path_Non_Rooted_Spaced()
        {
            // Arrange.
            const string query = "/Documents/Files/Images += Vacation/Italy/Tuscany";

            // Act.
            var act = new System.Action(() =>
            {
                var script = _parser.Parse(query);
                var count = script.Sequences.Count();
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }
    }
}