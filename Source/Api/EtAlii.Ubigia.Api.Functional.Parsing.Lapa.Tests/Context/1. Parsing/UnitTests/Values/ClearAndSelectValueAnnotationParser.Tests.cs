// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Context;
using EtAlii.Ubigia.Api.Functional.Tests;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class ClearAndSelectValueAnnotationParserTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;

    public ClearAndSelectValueAnnotationParserTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task ClearAndSelectValueAnnotationParser_Create()
    {
        // Arrange.

        // Act.
        var parser = await _testContext
            .CreateComponentOnNewSpace<IClearAndSelectValueAnnotationParser>()
            .ConfigureAwait(false);

        // Assert.
        Assert.NotNull(parser);
    }

    [Fact]
    public async Task ClearAndSelectValueAnnotationParser_Parse_Value_LastName()
    {
        // Arrange.
        var parser = await _testContext
            .CreateComponentOnNewSpace<IClearAndSelectValueAnnotationParser>()
            .ConfigureAwait(false);
        var text = @"@node-clear(\\LastName)";

        // Act.
        var node = parser.Parser.Do(text);
        var annotation = parser.Parse(node);

        // Assert.
        Assert.NotNull(node);
        Assert.Empty(node.Rest);
        var valueAnnotation = annotation as ClearAndSelectValueAnnotation;
        Assert.NotNull(valueAnnotation);
        Assert.Equal(@"\\LastName",valueAnnotation.Source.ToString());
    }

    [Fact]
    public async Task ClearAndSelectValueAnnotationParser_Parse_Value_NickName()
    {
        // Arrange.
        var parser = await _testContext
            .CreateComponentOnNewSpace<IClearAndSelectValueAnnotationParser>()
            .ConfigureAwait(false);
        var text = @"@node-clear(NickName)";

        // Act.
        var node = parser.Parser.Do(text);
        var annotation = parser.Parse(node);

        // Assert.
        Assert.NotNull(node);
        Assert.Empty(node.Rest);
        var valueAnnotation = annotation as ClearAndSelectValueAnnotation;
        Assert.NotNull(valueAnnotation);
        Assert.Equal(@"NickName",valueAnnotation.Source.ToString());
    }

    [Fact]
    public async Task ClearAndSelectValueAnnotationParser_Parse_Value()
    {
        // Arrange.
        var parser = await _testContext
            .CreateComponentOnNewSpace<IClearAndSelectValueAnnotationParser>()
            .ConfigureAwait(false);
        var text = @"@node-clear(//Weight)";

        // Act.
        var node = parser.Parser.Do(text);
        var annotation = parser.Parse(node);

        // Assert.
        Assert.NotNull(node);
        Assert.Empty(node.Rest);
        var valueAnnotation = annotation as ClearAndSelectValueAnnotation;
        Assert.NotNull(valueAnnotation);
        Assert.Equal(@"//Weight",valueAnnotation.Source.ToString());
    }
}
