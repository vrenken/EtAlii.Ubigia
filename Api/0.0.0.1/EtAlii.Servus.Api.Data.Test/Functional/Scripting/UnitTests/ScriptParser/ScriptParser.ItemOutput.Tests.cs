namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Model;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    public partial class ScriptParser_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Variable_Name()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/$Third/Fourth");

            // Assert.
            var sequence = script.Sequences.First();
            Assert.AreEqual("Third", sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(5).Cast<VariablePathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Component_Count_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth");

            // Assert.
            var sequence = script.Sequences.First();
            Assert.AreEqual(8, ((PathSubject)sequence.Parts.ElementAt(1)).Parts.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Quoted_Name()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/\"Third is cool\"/Fourth");

            // Assert.
            var sequence = script.Sequences.First();
            Assert.AreEqual("Third is cool", ((PathSubject)sequence.Parts.ElementAt(1)).Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Component_Count_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second\r\n/Third/Fourth");

            // Assert.
            var firstSequence = script.Sequences.ElementAt(0);
            Assert.AreEqual(4, firstSequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Count());
            var secondSequence = script.Sequences.ElementAt(1);
            Assert.AreEqual(4, secondSequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Component_Name_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth");

            // Assert.
            var sequence = script.Sequences.First();
            Assert.AreEqual("Fourth", ((ConstantPathSubjectPart)((PathSubject)sequence.Parts.ElementAt(1)).Parts.Skip(7).First()).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Component_Name_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second\r\n/Third/Fourth");

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.AreEqual("Fourth", sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Variable()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/$Third/Fourth");

            // Assert.
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(5).First(), typeof(VariablePathSubjectPart));
        }
    }
}