// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

// ReSharper disable RedundantUsingDirective
using EtAlii.Ubigia.Api.Functional.Context;
using Xunit;
// ReSharper restore RedundantUsingDirective

public partial class SchemaParserTests
{
    #if USE_LAPA_PARSING_IN_TESTS
    // The test below only works on the Antlr4 parser. We still keep it in as the outcome is better than that of the Lapa parser.

    [Fact]
    public void SchemaParser_Parse_Mutation_Using_Variables_01()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var parser = _testContext.CreateSchemaParser();
        var normalPersonText = @"
        Settings = @node-add(/Data, ServiceSettings)
        {
            string AdminUsername = $adminUsername,
            string AdminPassword = $adminPassword,
            string Certificate = $certificate,
            string LocalStorageId = $localStorageId
        }";

        // Act.
        var parseResult = parser.Parse(normalPersonText, scope);

        // Assert.
        Assert.NotNull(parseResult);
        Assert.Empty(parseResult.Errors);
        Assert.NotNull(parseResult.Schema);
        Assert.NotNull(parseResult.Schema.Structure);
        Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);
    }

    [Fact]
    public void SchemaParser_Parse_Mutation_Using_Variables_02()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var parser = _testContext.CreateSchemaParser();
        var normalPersonText = @"
        Person = @node-add(Person:$lastName/, $firstName)
        {
            FirstName = @node(),
            LastName = \#FamilyName,
            NickName = $nickName
        }";

        // Act.
        var parseResult = parser.Parse(normalPersonText, scope);

        // Assert.
        Assert.NotNull(parseResult);
        Assert.Empty(parseResult.Errors);
        Assert.NotNull(parseResult.Schema);
        Assert.NotNull(parseResult.Schema.Structure);
        Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);
    }

    #endif
}
