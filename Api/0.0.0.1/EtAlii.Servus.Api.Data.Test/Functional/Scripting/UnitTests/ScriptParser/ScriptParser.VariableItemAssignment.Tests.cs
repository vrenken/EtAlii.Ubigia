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
        public void ScriptParser_VariableItemAssignment_With_Variable()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 <= /Fourth/4\r\n/Fifth/5\r\n/Sixth/6");

            // Assert.
            Assert.AreEqual(4, script.Sequences.Count());
            var sequence = script.Sequences.Skip(1).First();
            Assert.AreEqual("var1", ((VariableSubject)sequence.Parts.Skip(0).First()).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableItemAssignment_With_Variable_Without_Spaces()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth\r\n$var1<=/Fourth/4\r\n/Fifth/5\r\n/Sixth/6");

            // Assert.
            var sequence = script.Sequences.ElementAt(1);
            Assert.AreEqual("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableItemAssignment_With_Path()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 <= /Fourth/4\r\n/Fifth/5\r\n/Sixth/6");

            // Assert.
            var sequence = script.Sequences.ElementAt(1);
            Assert.AreEqual("4", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableItemAssignment_With_Path_Without_Spaces()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth\r\n$var1<=/Fourth/4\r\n/Fifth/5\r\n/Sixth/6");

            // Assert.
            var sequence = script.Sequences.ElementAt(1);
            Assert.AreEqual("4", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        }
    }
}