// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using EtAlii.Ubigia.Api.Functional.Tests;
using Xunit;

public partial class SchemaParserTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;

    public SchemaParserTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public void SchemaParser_Create()
    {
        // Arrange.

        // Act.
        var parser = _testContext.CreateSchemaParser();

        // Assert.
        Assert.NotNull(parser);
    }

    [Fact]
    public void SchemaParser_Parse_Comment()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var parser = _testContext.CreateSchemaParser();
        var text = @"-- This is a comment { }";


        // Act.
        var parseResult = parser.Parse(text, scope);

        // Assert.
        Assert.NotNull(parseResult);
        Assert.Empty(parseResult.Errors);
        Assert.Null(parseResult.Schema);
    }

    [Fact]
    public void SchemaParser_Parse_Comment_And_Object_Single_Line()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var parser = _testContext.CreateSchemaParser();
        var text = @"-- This is a comment { ""key"": ""value"" }";


        // Act.
        var parseResult = parser.Parse(text, scope);

        // Assert.
        Assert.NotNull(parseResult);
        Assert.Empty(parseResult.Errors);
        Assert.Null(parseResult.Schema);
    }
}
