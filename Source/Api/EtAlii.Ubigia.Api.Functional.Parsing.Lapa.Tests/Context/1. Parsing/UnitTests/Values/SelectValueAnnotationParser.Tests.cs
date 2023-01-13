// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Context;
using EtAlii.Ubigia.Api.Functional.Tests;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class SelectValueAnnotationParserTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;

    public SelectValueAnnotationParserTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task SelectValueAnnotationParser_Create()
    {
        // Arrange.

        // Act.
        var parser = await _testContext
            .CreateComponentOnNewSpace<ISelectValueAnnotationParser>()
            .ConfigureAwait(false);

        // Assert.
        Assert.NotNull(parser);
    }

    [Fact]
    public async Task SelectValueAnnotationParser_Parse_Value_LastName()
    {
        // Arrange.
        var parser = await _testContext
            .CreateComponentOnNewSpace<ISelectValueAnnotationParser>()
            .ConfigureAwait(false);
        var text = @"@node(\\LastName)";

        // Act.
        var node = parser.Parser.Do(text);
        var annotation = parser.Parse(node);

        // Assert.
        Assert.NotNull(node);
        Assert.Empty(node.Rest);
        var valueAnnotation = annotation as SelectValueAnnotation;
        Assert.NotNull(valueAnnotation);
        Assert.Equal(@"\\LastName",valueAnnotation.Source.ToString());
    }

    [Fact]
    public async Task SelectValueAnnotationParser_Parse_Value()
    {
        // Arrange.
        var parser = await _testContext
            .CreateComponentOnNewSpace<ISelectValueAnnotationParser>()
            .ConfigureAwait(false);
        var text = @"@node()";

        // Act.
        var node = parser.Parser.Do(text);
        var annotation = parser.Parse(node);

        // Assert.
        Assert.NotNull(node);
        Assert.Empty(node.Rest);
        var valueAnnotation = annotation as SelectValueAnnotation;
        Assert.NotNull(valueAnnotation);
        Assert.Null(valueAnnotation.Source);
    }
}
