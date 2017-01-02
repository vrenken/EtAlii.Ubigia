namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class ScriptParser_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Variable_Name()
        {
            // Arrange.
            const string query = "/First/Second/$Third/Fourth/";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Third", sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(5).Cast<VariablePathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Component_Count_1()
        {
            // Arrange.
            const string query = "/First/Second/Third/Fourth/";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual(9, sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Quoted_Name()
        {
            // Arrange.
            const string query = "/First/Second/\"Third is cool\"/Fourth/";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Third is cool", sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Quoted_Name_Special_Characters()
        {
            // Arrange.
            const string query = "/First/Second/\"Third is cool äëöüáéóúâêôû\"/Fourth/";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Third is cool äëöüáéóúâêôû", sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        }

        

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Component_Count_2()
        {
            // Arrange.
            var query = "/First/Second\r\n/Third/Fourth/";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var firstSequence = script.Sequences.First();
            Assert.AreEqual(4, firstSequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Count());
            var secondSequence = script.Sequences.Skip(1).First();
            Assert.AreEqual(5, secondSequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Component_Name_1()
        {
            // Arrange.
            const string query = "/First/Second/Third/Fourth/";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.AreEqual("Fourth", sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(7).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Component_Name_2()
        {
            // Arrange.
            const string query = "/First/Second\r\n/Third/Fourth/";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.ElementAt(1);
            Assert.AreEqual("Fourth", sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Variable()
        {
            // Arrange.
            const string query = "/First/Second/$Third/Fourth/";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(5).First(), typeof(VariablePathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Identifier()
        {
            // Arrange.
            const string query = "/&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40/$Third/Fourth/";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(1).First(), typeof(IdentifierPathSubjectPart));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).Cast<PathSubject>().First().Parts.Skip(3).First(), typeof(VariablePathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Identifier_Invalid_Storage_Additional_Character()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("/&38a52be49352453eaf975c3b448652f0_A.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40$Third/Fourth/");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Identifier_Invalid_With_Two_Identifiers()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("/&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40/&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Identifier_Invalid_Storage_Invalid_Letter()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("/&38a52be49352453eaf975c3b448652fP.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40$Third/Fourth/");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Identifier_Invalid_Storage_Invalid_Character()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("/&38a52be49352453eaf975c3b448652f-.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40$Third/Fourth/");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_ItemsOutput_With_Identifier_Invalid_Structure()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("/&38a52be49352453.eaf975c3b448652f.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40$Third/Fourth/");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }
    }
}