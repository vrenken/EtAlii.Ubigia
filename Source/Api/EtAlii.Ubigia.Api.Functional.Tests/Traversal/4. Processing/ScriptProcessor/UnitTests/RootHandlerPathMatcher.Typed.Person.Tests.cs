// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using System.Linq;
using System.Threading.Tasks;
using Xunit;

public partial class RootHandlerPathMatcherTests
{

    [Fact]
    public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_07()
    {
        // Arrange.
        var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
        var scope = new ExecutionScope();
        var template = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.FirstNameFormatter)
        };
        var path = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Banner"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter")
        };
        var rootHandler = new TestRootHandler(template);

        // Act.
        var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

        // Assert.
        Assert.NotEqual(MatchResult.NoMatch, match);
        Assert.Equal("/Banner/Peter", string.Join("", match.Match.Select(m => m.ToString())));
        Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));
    }

    [Fact]
    public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_08()
    {
        // Arrange.
        var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
        var scope = new ExecutionScope();
        var template = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.FirstNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(TextPathFormatter.NumberFormatter)
        };
        var path = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Banner"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("0"),
        };
        var rootHandler = new TestRootHandler(template);

        // Act.
        var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

        // Assert.
        Assert.NotEqual(MatchResult.NoMatch, match);
        Assert.Equal("/Banner/Peter/0", string.Join("", match.Match.Select(m => m.ToString())));
        Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));
    }

    [Fact]
    public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_09()
    {
        // Arrange.
        var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
        var scope = new ExecutionScope();
        var template = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.FirstNameFormatter)
        };
        var path = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Banner"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("0"),
        };
        var rootHandler = new TestRootHandler(template);

        // Act.
        var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

        // Assert.
        Assert.NotEqual(MatchResult.NoMatch, match);
        Assert.Equal("/Banner/Peter", string.Join("", match.Match.Select(m => m.ToString())));
        Assert.Equal("/0", string.Join("", match.Rest.Select(m => m.ToString())));
    }

    [Fact]
    public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_100_False()
    {
        // Arrange.
        var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
        var scope = new ExecutionScope();
        var template = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.FirstNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(TextPathFormatter.WordFormatter)
        };
        var path = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Banner"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("0"),
        };
        var rootHandler = new TestRootHandler(template);

        // Act.
        var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

        // Assert.
        Assert.Equal(MatchResult.NoMatch, match);
    }

    [Fact]
    public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_101_False()
    {
        // Arrange.
        var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
        var scope = new ExecutionScope();
        var template = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.FirstNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(TextPathFormatter.WordFormatter)
        };
        var path = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Banner"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart(" "),
        };
        var rootHandler = new TestRootHandler(template);

        // Act.
        var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

        // Assert.
        Assert.Equal(MatchResult.NoMatch, match);
    }

    [Fact]
    public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_102_False()
    {
        // Arrange.
        var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
        var scope = new ExecutionScope();
        var template = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.FirstNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(TextPathFormatter.WordFormatter)
        };
        var path = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Banner"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("_"),
        };
        var rootHandler = new TestRootHandler(template);

        // Act.
        var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

        // Assert.
        Assert.Equal(MatchResult.NoMatch, match);
    }

    [Fact]
    public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_103_False()
    {
        // Arrange.
        var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
        var scope = new ExecutionScope();
        var template = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.FirstNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(TextPathFormatter.WordFormatter)
        };
        var path = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Banner"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("="),
        };
        var rootHandler = new TestRootHandler(template);

        // Act.
        var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

        // Assert.
        Assert.Equal(MatchResult.NoMatch, match);
    }


    [Fact]
    public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_104_False()
    {
        // Arrange.
        var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
        var scope = new ExecutionScope();
        var template = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.FirstNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(TextPathFormatter.WordFormatter)
        };
        var path = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Banner"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("*"),
        };
        var rootHandler = new TestRootHandler(template);

        // Act.
        var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

        // Assert.
        Assert.Equal(MatchResult.NoMatch, match);
    }

    [Fact]
    public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_11_False()
    {
        // Arrange.
        var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
        var scope = new ExecutionScope();
        var template = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(TextPathFormatter.NumberFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(TextPathFormatter.NumberFormatter)
        };
        var path = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Banner"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("0"),
        };
        var rootHandler = new TestRootHandler(template);

        // Act.
        var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

        // Assert.
        Assert.Equal(MatchResult.NoMatch, match);
    }


    [Fact]
    public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_12_False()
    {
        // Arrange.
        var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
        var scope = new ExecutionScope();
        var template = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new TypedPathSubjectPart(TextPathFormatter.NumberFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(NamePathFormatter.FirstNameFormatter),
            new ParentPathSubjectPart(), new TypedPathSubjectPart(TextPathFormatter.NumberFormatter)
        };
        var path = new PathSubjectPart[]
        {
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Banner"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
            new ParentPathSubjectPart(), new ConstantPathSubjectPart("0"),
        };
        var rootHandler = new TestRootHandler(template);

        // Act.
        var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

        // Assert.
        Assert.Equal(MatchResult.NoMatch, match);
    }

}
