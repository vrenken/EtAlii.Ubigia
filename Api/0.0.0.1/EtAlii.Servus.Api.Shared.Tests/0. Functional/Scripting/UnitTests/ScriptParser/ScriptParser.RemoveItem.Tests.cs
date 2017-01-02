// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class ScriptParser_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_RemoveItem_Without_File()
        {
            // Arrange.
            const string query = "/Documents/Files/-=Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(RemoveOperator));
            Assert.AreEqual("Images", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_RemoveItem_Quoted()
        {
            // Arrange.
            const string query = "/Documents/Files/-= \"Images\"";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(RemoveOperator));
            Assert.AreEqual("Images", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_RemoveItem_Rooted()
        {
            // Arrange.
            const string query = "/Documents/Files/-=/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Files", sequence.Parts.Skip(0).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(RemoveOperator));
            Assert.AreEqual("Images", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        }
    }
}