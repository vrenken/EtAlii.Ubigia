// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using EtAlii.Ubigia.Api.Functional.Context;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class SchemaParserBugsTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;

    public SchemaParserBugsTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public void SchemaParserBugs_Parse_Comment()
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
    public void SchemaParserBugs_Parse_Node_Set_Annotation()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var parser = _testContext.CreateSchemaParser();
        var text = @"Person = @node(Person:Doe/John)
            {
                NickName = @node-set(""Johnny"")
            }";


        // Act.
        var parseResult = parser.Parse(text, scope);

        // Assert.
        Assert.NotNull(parseResult);
        Assert.Empty(parseResult.Errors);
        Assert.NotNull(parseResult.Schema);
        Assert.NotNull(parseResult.Schema.Structure);
        Assert.NotNull(parseResult.Schema.Structure.Annotation);
        var valueFragment = Assert.Single(parseResult.Schema.Structure.Values);
        Assert.NotNull(valueFragment!.Annotation);
        var assignAndSelectValueAnnotation = Assert.IsType<AssignAndSelectValueAnnotation>(valueFragment.Annotation);
        Assert.Null(assignAndSelectValueAnnotation.Source);
        Assert.NotNull(assignAndSelectValueAnnotation.Subject);
        var subject = Assert.IsType<StringConstantSubject>(assignAndSelectValueAnnotation.Subject);
        Assert.Equal("Johnny",subject.Value);
    }

    [Fact]
    public void SchemaParserBugs_Parse_Node_Clear_Annotation()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var parser = _testContext.CreateSchemaParser();
        var text = @"Person = @node(Person:Doe/John)
            {
                FirstName = @node-clear()
            }";

        // Act.
        var parseResult = parser.Parse(text, scope);

        // Assert.
        Assert.NotNull(parseResult);
        Assert.Empty(parseResult.Errors);
        Assert.NotNull(parseResult.Schema);
        Assert.NotNull(parseResult.Schema.Structure);
        Assert.NotNull(parseResult.Schema.Structure.Annotation);
        var valueFragment = Assert.Single(parseResult.Schema.Structure.Values);
        Assert.NotNull(valueFragment!.Annotation);
        var assignAndSelectValueAnnotation = Assert.IsType<ClearAndSelectValueAnnotation>(valueFragment.Annotation);
        Assert.Null(assignAndSelectValueAnnotation.Source);
    }

    [Fact]
    public void SchemaParserBugs_Parse_Generated_Code_Not_Working()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var parser = _testContext.CreateSchemaParser();
        const string text = @"
			[namespace=EtAlii.Ubigia.Infrastructure.Transport]
			ServiceSettings = @node(Data:ServiceSettings)
			{
				string AdminUsername
				string AdminPassword
				string Certificate
				Guid LocalStorageId
			}";

        // Act.
        var parseResult = parser.Parse(text, scope);

        // Assert.
        Assert.NotNull(parseResult);
        Assert.Empty(parseResult.Errors);
        Assert.NotNull(parseResult.Schema);
        Assert.NotNull(parseResult.Schema.Structure);
        Assert.NotNull(parseResult.Schema.Structure.Annotation);
    }
}
