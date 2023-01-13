// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using EtAlii.Ubigia.Api.Functional.Traversal;
using Moppet.Lapa;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class TypedPathSubjectPartParserTests
{
    [Fact]
    public void TypedPathSubjectPartParser_Create()
    {
        // Arrange.

        // Act.
        var parser = new TypedPathSubjectPartParser(new NodeValidator(), new NodeFinder());

        // Assert.
        Assert.NotNull(parser);
    }

    [Fact]
    public void TypedPathSubjectPartParser_Words_Valid()
    {
        // Arrange.
        var parser = new TypedPathSubjectPartParser(new NodeValidator(), new NodeFinder());

        // Act.
        var result = new LpsParser(parser.Parser).Do("[Words]");

        // Assert.
        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.Rest.ToString());
        Assert.Equal("[Words]", result.Match.ToString());
    }

    [Fact]
    public void TypedPathSubjectPartParser_words_Valid()
    {
        // Arrange.
        var parser = new TypedPathSubjectPartParser(new NodeValidator(), new NodeFinder());

        // Act.
        var result = new LpsParser(parser.Parser).Do("[words]");

        // Assert.
        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.Rest.ToString());
        Assert.Equal("[words]", result.Match.ToString());
    }


    [Fact]
    public void TypedPathSubjectPartParser_WORDS_Valid()
    {
        // Arrange.
        var parser = new TypedPathSubjectPartParser(new NodeValidator(), new NodeFinder());

        // Act.
        var result = new LpsParser(parser.Parser).Do("[WORDS]");

        // Assert.
        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.Rest.ToString());
        Assert.Equal("[WORDS]", result.Match.ToString());
    }


    [Fact]
    public void TypedPathSubjectPartParser_Word_Valid()
    {
        // Arrange.
        var parser = new TypedPathSubjectPartParser(new NodeValidator(), new NodeFinder());

        // Act.
        var result = new LpsParser(parser.Parser).Do("[Word]");

        // Assert.
        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.Rest.ToString());
        Assert.Equal("[Word]", result.Match.ToString());
    }

    [Fact]
    public void TypedPathSubjectPartParser_word_Valid()
    {
        // Arrange.
        var parser = new TypedPathSubjectPartParser(new NodeValidator(), new NodeFinder());

        // Act.
        var result = new LpsParser(parser.Parser).Do("[word]");

        // Assert.
        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.Rest.ToString());
        Assert.Equal("[word]", result.Match.ToString());
    }


    [Fact]
    public void TypedPathSubjectPartParser_WORD_Valid()
    {
        // Arrange.
        var parser = new TypedPathSubjectPartParser(new NodeValidator(), new NodeFinder());

        // Act.
        var result = new LpsParser(parser.Parser).Do("[WORD]");

        // Assert.
        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.Rest.ToString());
        Assert.Equal("[WORD]", result.Match.ToString());
    }

    [Fact]
    public void TypedPathSubjectPartParser_WORDS_InValid()
    {
        // Arrange.
        var parser = new TypedPathSubjectPartParser(new NodeValidator(), new NodeFinder());

        // Act.
        var result = new LpsParser(parser.Parser).Do("[ WORDS]");

        // Assert.
        Assert.False(result.Success);
        Assert.Equal(string.Empty, result.Match.ToString());
        Assert.Equal("[ WORDS]", result.Rest.ToString());
    }

    [Fact]
    public void TypedPathSubjectPartParser_WORD_InValid()
    {
        // Arrange.
        var parser = new TypedPathSubjectPartParser(new NodeValidator(), new NodeFinder());

        // Act.
        var result = new LpsParser(parser.Parser).Do("[ WORD]");

        // Assert.
        Assert.False(result.Success);
        Assert.Equal(string.Empty, result.Match.ToString());
        Assert.Equal("[ WORD]", result.Rest.ToString());
    }
}
