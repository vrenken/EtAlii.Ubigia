namespace EtAlii.Servus.Api.Data.UnitTests
{
    using System;
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using EtAlii.Servus.Api.Data.Model;

    public partial class ScriptParser_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableUpdateItem()
        {
            // Arrange.
            const string query = "$var0 <= /Documents/Files/Image01 <= $var1";

            // Act.
            var script = _parser.Parse(query);

            // Assert.
            var sequence = script.Sequences.First();
            Assert.AreEqual("var0", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Files", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(4).First(), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Image01", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(3).First(), typeof(AssignOperator));
            Assert.AreEqual("var1", sequence.Parts.Skip(4).Cast<VariableSubject>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableUpdateItem_Without_Spaces()
        {
            // Arrange.
            const string query = "$var0 <= /Documents/Files/Image01<=$var1";

            // Act.
            var script = _parser.Parse(query);

            // Assert.
            var sequence = script.Sequences.First();
            Assert.AreEqual("var0", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Files", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(4).First(), typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Image01", sequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(3).First(), typeof(AssignOperator));
            Assert.AreEqual("var1", sequence.Parts.Skip(4).Cast<VariableSubject>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableUpdateItem_Based_On_Identifier()
        {
            // Arrange.
            const string query = "$var0 <= /&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40 <= $var1";

            // Act.
            var script = _parser.Parse(query);

            // Assert.
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(0), typeof(VariableSubject));
            Assert.AreEqual("var0", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(2), typeof(PathSubject));
            var secondSubject = sequence.Parts.Skip(2).Cast<PathSubject>().First();
            Assert.IsInstanceOfType(secondSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            var identifierPart = secondSubject.Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
            Assert.AreEqual(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
            Assert.AreEqual(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
            Assert.AreEqual(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
            Assert.AreEqual((ulong)20, identifierPart.Identifier.Era);
            Assert.AreEqual((ulong)30, identifierPart.Identifier.Period);
            Assert.AreEqual((ulong)40, identifierPart.Identifier.Moment);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(3), typeof(AssignOperator));
            Assert.AreEqual("var1", sequence.Parts.Skip(4).Cast<VariableSubject>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableUpdateItem_Based_On_Identifier_Without_Spaces()
        {
            // Arrange.
            const string query = "$var0 <= /&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40<=$var1";

            // Act.
            var script = _parser.Parse(query);

            // Assert.
            var sequence = script.Sequences.First();
            Assert.AreEqual("var0", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));
            var secondSubject = sequence.Parts.Skip(2).Cast<PathSubject>().First();
            Assert.IsInstanceOfType(secondSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            var identifierPart = secondSubject.Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
            Assert.AreEqual(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
            Assert.AreEqual(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
            Assert.AreEqual(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
            Assert.AreEqual((ulong)20, identifierPart.Identifier.Era);
            Assert.AreEqual((ulong)30, identifierPart.Identifier.Period);
            Assert.AreEqual((ulong)40, identifierPart.Identifier.Moment);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(3), typeof(AssignOperator));
            Assert.AreEqual("var1", sequence.Parts.Skip(4).Cast<VariableSubject>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableUpdateItem_Based_On_Identifier_In_Variable()
        {
            // Arrange.
            const string query = "$id <= /&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40\r\n$var0 <= /$id <= $var1";

            // Act.
            var script = _parser.Parse(query);

            // Assert.
            var firstSequence = script.Sequences.ElementAt(0);
            Assert.AreEqual("id", firstSequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(firstSequence.Parts.ElementAt(1), typeof(AssignOperator));
            var secondSubject = firstSequence.Parts.Skip(2).Cast<PathSubject>().First();
            Assert.IsInstanceOfType(secondSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            var identifierPart = secondSubject.Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
            Assert.AreEqual(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
            Assert.AreEqual(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
            Assert.AreEqual(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
            Assert.AreEqual((ulong)20, identifierPart.Identifier.Era);
            Assert.AreEqual((ulong)30, identifierPart.Identifier.Period);
            Assert.AreEqual((ulong)40, identifierPart.Identifier.Moment);
            // $var0 <= /$id <= $var1
            var secondSequence = script.Sequences.ElementAt(1);
            Assert.AreEqual("var0", secondSequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(secondSequence.Parts.ElementAt(1), typeof(AssignOperator));
            Assert.AreEqual("id", secondSequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(1).Cast<VariablePathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(secondSequence.Parts.ElementAt(3), typeof(AssignOperator));
            Assert.AreEqual("var1", secondSequence.Parts.Skip(4).Cast<VariableSubject>().First().Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_VariableUpdateItem_Based_On_Identifier_In_Variable_Without_Spaces()
        {
            // Arrange.
            const string query = "$id <= /&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40\r\n$var0 <= /$id<=$var1";

            // Act.
            var script = _parser.Parse(query);

            // Assert.
            var firstSequence = script.Sequences.ElementAt(0);
            Assert.AreEqual("id", firstSequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(firstSequence.Parts.ElementAt(1), typeof(AssignOperator));
            var secondSubject = firstSequence.Parts.Skip(2).Cast<PathSubject>().First();
            Assert.IsInstanceOfType(secondSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            var identifierPart = secondSubject.Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
            Assert.AreEqual(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
            Assert.AreEqual(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
            Assert.AreEqual(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
            Assert.AreEqual((ulong)20, identifierPart.Identifier.Era);
            Assert.AreEqual((ulong)30, identifierPart.Identifier.Period);
            Assert.AreEqual((ulong)40, identifierPart.Identifier.Moment);
            // $var0 <= /$id <= $var1
            var secondSequence = script.Sequences.ElementAt(1);
            Assert.AreEqual("var0", secondSequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(secondSequence.Parts.ElementAt(1), typeof(AssignOperator));
            Assert.AreEqual("id", secondSequence.Parts.Skip(2).Cast<PathSubject>().First().Parts.Skip(1).Cast<VariablePathSubjectPart>().First().Name);
            Assert.IsInstanceOfType(secondSequence.Parts.ElementAt(3), typeof(AssignOperator));
            Assert.AreEqual("var1", secondSequence.Parts.Skip(4).Cast<VariableSubject>().First().Name);
        }
    }
}