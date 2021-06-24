// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_UpdateItem()
        {
            // Arrange.
            const string query = "/Documents/Files/Image01 <= $var1";

            // Act.
            var script = _parser.Parse(query).Script;

            // Assert.
            var sequence = script.Sequences.First();
            Assert.Equal("Files", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Image01", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("var1", sequence.Parts.Skip(2).Cast<VariableSubject>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_UpdateItem_Without_Spaces()
        {
            // Arrange.
            const string query = "/Documents/Files/Image01<=$var1";

            // Act.
            var script = _parser.Parse(query).Script;

            // Assert.
            var sequence = script.Sequences.First();
            Assert.Equal("Files", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("Image01", sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
            Assert.Equal("var1", sequence.Parts.Skip(2).Cast<VariableSubject>().First().Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_UpdateItem_Based_On_Identifier()
        {
            // Arrange.
            const string query = "/&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40 <= $var1";

            // Act.
            var script = _parser.Parse(query).Script;

            // Assert.
            var sequence = script.Sequences.First();
            var identifierPart = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
            Assert.Equal(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
            Assert.Equal(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
            Assert.Equal(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
            Assert.Equal((ulong)20, identifierPart.Identifier.Era);
            Assert.Equal((ulong)30, identifierPart.Identifier.Period);
            Assert.Equal((ulong)40, identifierPart.Identifier.Moment);
            var variableSubject = sequence.Parts.Skip(2).Cast<VariableSubject>().First();
            Assert.Equal("var1", variableSubject.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_UpdateItem_Based_On_Identifier_Without_Spaces()
        {
            // Arrange.
            const string query = "/&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40<=$var1";

            // Act.
            var script = _parser.Parse(query).Script;

            // Assert.
            var sequence = script.Sequences.First();
            var identifierPart = sequence.Parts.Skip(0).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
            Assert.Equal(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
            Assert.Equal(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
            Assert.Equal(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
            Assert.Equal((ulong)20, identifierPart.Identifier.Era);
            Assert.Equal((ulong)30, identifierPart.Identifier.Period);
            Assert.Equal((ulong)40, identifierPart.Identifier.Moment);
            var variableSubject = sequence.Parts.Skip(2).Cast<VariableSubject>().First();
            Assert.Equal("var1", variableSubject.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_UpdateItem_Based_On_Identifier_In_Variable()
        {
            // Arrange.
            const string query = "$id <= /&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40\r\n/$id <= $var1";

            // Act.
            var script = _parser.Parse(query).Script;

            // Assert.
            var firstSequence = script.Sequences.First();
            var identifierPart = firstSequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
            Assert.Equal(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
            Assert.Equal(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
            Assert.Equal(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
            Assert.Equal((ulong)20, identifierPart.Identifier.Era);
            Assert.Equal((ulong)30, identifierPart.Identifier.Period);
            Assert.Equal((ulong)40, identifierPart.Identifier.Moment);
            var secondSequence = script.Sequences.ElementAt(1);
            var variableSubject = secondSequence.Parts.Skip(2).Cast<VariableSubject>().First();
            Assert.Equal("var1", variableSubject.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_UpdateItem_Based_On_Identifier_In_Variable_Without_Spaces()
        {
            // Arrange.
            const string query = "$id <= /&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40\r\n/$id<=$var1";

            // Act.
            var script = _parser.Parse(query).Script;

            // Assert.
            var firstSequence = script.Sequences.First();
            var identifierPart = firstSequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First().Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
            Assert.Equal(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
            Assert.Equal(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
            Assert.Equal(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
            Assert.Equal((ulong)20, identifierPart.Identifier.Era);
            Assert.Equal((ulong)30, identifierPart.Identifier.Period);
            Assert.Equal((ulong)40, identifierPart.Identifier.Moment);
            var secondSequence = script.Sequences.ElementAt(1);
            var variableSubject = secondSequence.Parts.Skip(2).Cast<VariableSubject>().First();
            Assert.Equal("var1", variableSubject.Name);
        }
    }
}
