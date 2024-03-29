﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Context;
using EtAlii.Ubigia.Api.Functional.Tests;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class UnlinkAndSelectMultipleNodesAnnotationParserTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;

    public UnlinkAndSelectMultipleNodesAnnotationParserTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task UnlinkAndSelectMultipleNodesAnnotationParser_Create()
    {
        // Arrange.

        // Act.
        var parser = await _testContext
            .CreateComponentOnNewSpace<IUnlinkAndSelectMultipleNodesAnnotationParser>()
            .ConfigureAwait(false);

        // Assert.
        Assert.NotNull(parser);
    }

    [Fact]
    public async Task UnlinkAndSelectMultipleNodesAnnotationParser_Parse_01()
    {
        // Arrange.
        var parser = await _testContext
            .CreateComponentOnNewSpace<IUnlinkAndSelectMultipleNodesAnnotationParser>()
            .ConfigureAwait(false);
        var text = @"@nodes-unlink(/Time, time:'2000-05-02 23:07', /Event)";

        // Act.
        var node = parser.Parser.Do(text);
        var annotation = parser.Parse(node);

        // Assert.
        Assert.NotNull(node);
        Assert.Empty(node.Rest);
        var nodeAnnotation = annotation as UnlinkAndSelectMultipleNodesAnnotation;
        Assert.NotNull(nodeAnnotation);
        Assert.Equal("/Time",nodeAnnotation.Source.ToString());
        Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
        Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
    }

    [Fact]
    public async Task UnlinkAndSelectMultipleNodesAnnotationParser_Parse_02()
    {
        // Arrange.
        var parser = await _testContext
            .CreateComponentOnNewSpace<IUnlinkAndSelectMultipleNodesAnnotationParser>()
            .ConfigureAwait(false);
        var text = @"@nodes-unlink(/Time, time:'2000-05-02 23:07',/Event)";

        // Act.
        var node = parser.Parser.Do(text);
        var annotation = parser.Parse(node);

        // Assert.
        Assert.NotNull(node);
        Assert.Empty(node.Rest);
        var nodeAnnotation = annotation as UnlinkAndSelectMultipleNodesAnnotation;
        Assert.NotNull(nodeAnnotation);
        Assert.Equal("/Time",nodeAnnotation.Source.ToString());
        Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
        Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
    }

    [Fact]
    public async Task UnlinkAndSelectMultipleNodesAnnotationParser_Parse_03()
    {
        // Arrange.
        var parser = await _testContext
            .CreateComponentOnNewSpace<IUnlinkAndSelectMultipleNodesAnnotationParser>()
            .ConfigureAwait(false);
        var text = @"@nodes-unlink(/Time,time:'2000-05-02 23:07', /Event)";

        // Act.
        var node = parser.Parser.Do(text);
        var annotation = parser.Parse(node);

        // Assert.
        Assert.NotNull(node);
        Assert.Empty(node.Rest);
        var nodeAnnotation = annotation as UnlinkAndSelectMultipleNodesAnnotation;
        Assert.NotNull(nodeAnnotation);
        Assert.Equal("/Time",nodeAnnotation.Source.ToString());
        Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
        Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
    }

    [Fact]
    public async Task UnlinkAndSelectMultipleNodesAnnotationParser_Parse_04()
    {
        // Arrange.
        var parser = await _testContext
            .CreateComponentOnNewSpace<IUnlinkAndSelectMultipleNodesAnnotationParser>()
            .ConfigureAwait(false);
        var text = @"@nodes-unlink(/Time,time:'2000-05-02 23:07', /Event)";

        // Act.
        var node = parser.Parser.Do(text);
        var annotation = parser.Parse(node);

        // Assert.
        Assert.NotNull(node);
        Assert.Empty(node.Rest);
        var nodeAnnotation = annotation as UnlinkAndSelectMultipleNodesAnnotation;
        Assert.NotNull(nodeAnnotation);
        Assert.Equal("/Time",nodeAnnotation.Source.ToString());
        Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
        Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
    }
}
