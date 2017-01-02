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
        public void ScriptParser_VariableStringAssignment()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$var1 <= \"Test\"");

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableStringAssignment_Without_Spaces()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$var1<=\"Test\"");

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableStringAssignment_Next()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1 <= \"Test\"");

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.AreEqual("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableStringAssignment_Next_Without_Spaces()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1<=\"Test\"");

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.AreEqual("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableStringAssignment_Middle()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1 <= \"Test\"\r\n/Test3/Test4");

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.AreEqual("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableStringAssignment_Middle_Without_Spaces()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Test1/Test2\r\n$var1<=\"Test\"\r\n/Test3/Test4");

            // Assert.
            var sequence = script.Sequences.Skip(1).First();
            Assert.AreEqual("var1", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Test", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }
    }
}