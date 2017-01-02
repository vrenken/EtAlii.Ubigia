namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class ScriptParser_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableOutput()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$var1 <= /Fourth/4\r\n/Fifth/5\r\n/Sixth/6\r\n$var1").Script;

            // Assert.
            Assert.AreEqual(4, script.Sequences.Count());
            var sequence = script.Sequences.ElementAt(3);
            Assert.AreEqual("var1", ((VariableSubject)sequence.Parts.Skip(1).First()).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableOutput_Without_Spaces()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$var1<=/Fourth/4\r\n/Fifth/5\r\n/Sixth/6\r\n$var1").Script;

            // Assert.
            Assert.AreEqual(4, script.Sequences.Count());
            var sequence = script.Sequences.ElementAt(3);
            Assert.AreEqual("var1", ((VariableSubject)sequence.Parts.Skip(1).ElementAt(0)).Name);
        }
    }
}