// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System.Linq;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Xunit;

public partial class ScriptParserTests
{
    [Fact]
    public void ScriptParser_ItemsOutput_With_Variable_Name()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/First/Second/$Third/Fourth/";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal("Third", sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<VariablePathSubjectPart>().First().Name);
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Component_Count_1()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/First/Second/Third/Fourth/";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal(9, sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Length);
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Quoted_Name()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/First/Second/\"Third is cool\"/Fourth/";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal("Third is cool", sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Quoted_Name_Special_Characters()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/First/Second/\"Third is cool äëöüáéóúâêôû\"/Fourth/";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal("Third is cool äëöüáéóúâêôû", sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
    }



    [Fact]
    public void ScriptParser_ItemsOutput_With_Component_Count_2()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var query = "/First/Second\r\n/Third/Fourth/";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var firstSequence = script.Sequences.First();
        Assert.Equal(4, firstSequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Length);
        var secondSequence = script.Sequences.Skip(1).First();
        Assert.Equal(5, secondSequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Length);
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Component_Name_1()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/First/Second/Third/Fourth/";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal("Fourth", sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(7).Cast<ConstantPathSubjectPart>().First().Name);
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Component_Name_2()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/First/Second\r\n/Third/Fourth/";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.ElementAt(1);
        Assert.Equal("Fourth", sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Variable()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/First/Second/$Third/Fourth/";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.IsType<VariablePathSubjectPart>(sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(5).First());
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Identifier()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40/$Third/Fourth/";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.IsType<IdentifierPathSubjectPart>(sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(1).First());
        Assert.IsType<VariablePathSubjectPart>(sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First().Parts.Skip(3).First());
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Identifier_Invalid_Storage_Additional_Character()
    {
        // Arrange.
        var scope = new ExecutionScope();

        // Act.
        var result = _parser.Parse("/&38a52be49352453eaf975c3b448652f0_A.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40$Third/Fourth/", scope);

        // Assert.
        Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Identifier_Invalid_With_Two_Identifiers()
    {
        // Arrange.
        var scope = new ExecutionScope();

        // Act.
        var result = _parser.Parse("/&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40/&38a52be49352453eaf975c3b448652f0.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40", scope);

        // Assert.
        Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Identifier_Invalid_Storage_Invalid_Letter()
    {
        // Arrange.
        var scope = new ExecutionScope();

        // Act.
        var result = _parser.Parse("/&38a52be49352453eaf975c3b448652fP.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40$Third/Fourth/", scope);

        // Assert.
        Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Identifier_Invalid_Storage_Invalid_Character()
    {
        // Arrange.
        var scope = new ExecutionScope();

        // Act.
        var result = _parser.Parse("/&38a52be49352453eaf975c3b448652f-.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40$Third/Fourth/", scope);

        // Assert.
        Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
    }

    [Fact]
    public void ScriptParser_ItemsOutput_With_Identifier_Invalid_Structure()
    {
        // Arrange.
        var scope = new ExecutionScope();

        // Act.
        var result = _parser.Parse("/&38a52be49352453.eaf975c3b448652f.3f2504e04f8941D39a0c0305e82c3301.21ec20203aea4069a2dd08002b30309d.20.30.40$Third/Fourth/", scope);

        // Assert.
        Assert.Contains(result.Errors, e => e.Exception is ScriptParserException);
    }
}
