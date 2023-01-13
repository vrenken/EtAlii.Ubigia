// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System;
using System.Linq;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Xunit;

public partial class ScriptParserNonRootedPathTests
{
    [Fact]
    public void ScriptParser_IdentifierOutput_Uppercase_Separated()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/&38A52BE4-9352-453E-AF97-5C3B448652F0.3F2504E0-4F89-41D3-9A0C-0305E82C3301.21EC2020-3AEA-4069-A2DD-08002B30309D.20.30.40";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        var subject = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First();
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.ElementAt(0));
        var identifierPart = subject.Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
        Assert.Equal(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
        Assert.Equal(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
        Assert.Equal(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
        Assert.Equal((ulong)20, identifierPart.Identifier.Era);
        Assert.Equal((ulong)30, identifierPart.Identifier.Period);
        Assert.Equal((ulong)40, identifierPart.Identifier.Moment);
    }

    [Fact]
    public void ScriptParser_IdentifierOutput_Uppercase_Not_Separated()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/&38A52BE49352453EAF975C3B448652F0.3F2504E04F8941D39A0C0305E82C3301.21EC20203AEA4069A2DD08002B30309D.20.30.40";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        var subject = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First();
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.ElementAt(0));
        var identifierPart = subject.Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
        Assert.Equal(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
        Assert.Equal(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
        Assert.Equal(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
        Assert.Equal((ulong)20, identifierPart.Identifier.Era);
        Assert.Equal((ulong)30, identifierPart.Identifier.Period);
        Assert.Equal((ulong)40, identifierPart.Identifier.Moment);
    }

    [Fact]
    public void ScriptParser_IdentifierOutput_Lowercase_Separated()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/&38a52be4-9352-453e-af97-5c3b448652f0.3f2504e0-4f89-41D3-9a0c-0305e82c3301.21ec2020-3aea-4069-a2dd-08002b30309d.20.30.40";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        var subject = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First();
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.ElementAt(0));
        var identifierPart = subject.Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
        Assert.Equal(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
        Assert.Equal(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
        Assert.Equal(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
        Assert.Equal((ulong)20, identifierPart.Identifier.Era);
        Assert.Equal((ulong)30, identifierPart.Identifier.Period);
        Assert.Equal((ulong)40, identifierPart.Identifier.Moment);
    }

    [Fact]
    public void ScriptParser_IdentifierOutput_Lowercase_Not_Separated()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        var subject = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First();
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.ElementAt(0));
        var identifierPart = subject.Parts.Skip(1).Cast<IdentifierPathSubjectPart>().First();
        Assert.Equal(Guid.Parse("38A52BE4-9352-453E-AF97-5C3B448652F0"), identifierPart.Identifier.Storage);
        Assert.Equal(Guid.Parse("3F2504E0-4F89-41D3-9A0C-0305E82C3301"), identifierPart.Identifier.Account);
        Assert.Equal(Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), identifierPart.Identifier.Space);
        Assert.Equal((ulong)20, identifierPart.Identifier.Era);
        Assert.Equal((ulong)30, identifierPart.Identifier.Period);
        Assert.Equal((ulong)40, identifierPart.Identifier.Moment);
    }
}
