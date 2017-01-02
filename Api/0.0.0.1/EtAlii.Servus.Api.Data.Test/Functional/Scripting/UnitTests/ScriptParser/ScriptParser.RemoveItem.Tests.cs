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
        public void ScriptParser_RemoveItem_Without_File()
        {
            // Arrange.
            var query = "/Documents/Files/-=Images";

            // Act.
            var script = _parser.Parse(query);

            // Assert.
            var sequence = script.Sequences.First();
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(RemoveOperator));
            Assert.AreEqual("Images", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Cast<ConstantPathSubjectPart>().First().Name);
        }
    }
}