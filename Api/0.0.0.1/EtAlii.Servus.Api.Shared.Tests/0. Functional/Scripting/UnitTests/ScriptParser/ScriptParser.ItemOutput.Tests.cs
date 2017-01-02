namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class ScriptParser_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Variable_Name()
        {
            // Arrange.
            const string query = "/First/Second/$Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Third", sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(5).Cast<VariablePathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Component_Count_1()
        {
            // Arrange.
            const string query = "/First/Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual(8, ((PathSubject)sequence.Parts.ElementAt(1)).Parts.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Quoted_Name()
        {
            // Arrange.
            const string query = "/First/Second/\"Third is cool\"/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Third is cool", ((PathSubject)sequence.Parts.ElementAt(1)).Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Quoted_Name_Special_Characters()
        {
            // Arrange.
            const string query = "/First/Second/\"Third is cool äëöüáéóúâêôû\"/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Third is cool äëöüáéóúâêôû", ((PathSubject)sequence.Parts.ElementAt(1)).Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        }

        

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Component_Count_2()
        {
            // Arrange.
            const string query = "/First/Second\r\n/Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var firstSequence = script.Sequences.ElementAt(0);
            Assert.AreEqual(4, firstSequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Count());
            var secondSequence = script.Sequences.ElementAt(1);
            Assert.AreEqual(4, secondSequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Component_Name_1()
        {
            // Arrange.
            const string query = "/First/Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Fourth", ((ConstantPathSubjectPart)((PathSubject)sequence.Parts.ElementAt(1)).Parts.Skip(7).First()).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Component_Name_2()
        {
            // Arrange.
            const string query = "/First/Second\r\n/Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.Skip(1).First();
            Assert.AreEqual("Fourth", sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemOutput_With_Variable()
        {
            // Arrange.
            const string query = "/First/Second/$Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(5).First(), typeof(VariablePathSubjectPart));
        }
    }
}