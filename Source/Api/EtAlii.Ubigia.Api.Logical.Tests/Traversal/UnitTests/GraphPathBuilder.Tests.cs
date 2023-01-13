// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests;

using EtAlii.Ubigia.Api.Logical;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class GraphPathBuilderTests
{
    [Fact]
    public void GraphPathBuilder_Create()
    {
        // Arrange.

        // Act.
        var builder = new GraphPathBuilder();

        // Assert.
        Assert.NotNull(builder);
    }

    [Fact]
    public void GraphPathBuilder_Add_Node()
    {
        // Arrange.
        var builder = new GraphPathBuilder();

        // Act.
        builder.Add("First");

        // Assert.
        Assert.NotEmpty(builder.ToPath());
    }

    [Fact]
    public void GraphPathBuilder_Add_Relation()
    {
        // Arrange.
        var builder = new GraphPathBuilder();

        // Act.
        builder.Add(GraphRelation.Children);

        // Assert.
        Assert.NotEmpty(builder.ToPath());
    }

    [Fact]
    public void GraphPathBuilder_Add_Node_ToPath()
    {
        // Arrange.
        var builder = new GraphPathBuilder();
        builder.Add("First");

        // Act.
        var path = builder.ToPath();

        // Assert.
        Assert.Equal(1, path.Length);
        Assert.IsAssignableFrom<GraphNode>(path[0]);
    }

    [Fact]
    public void GraphPathBuilder_Add_Relation_ToPath()
    {
        // Arrange.
        var builder = new GraphPathBuilder();
        builder.Add(GraphRelation.Children);

        // Act.
        var path = builder.ToPath();

        // Assert.
        Assert.Equal(1, path.Length);
        Assert.IsAssignableFrom<GraphRelation>(path[0]);
    }

    [Fact]
    public void GraphPathBuilder_Add_Node_And_Relation_ToPath()
    {
        // Arrange.
        var builder = new GraphPathBuilder();
        builder.Add("First");
        builder.Add(GraphRelation.Children);

        // Act.
        var path = builder.ToPath();

        // Assert.
        Assert.Equal(2, path.Length);
        Assert.IsAssignableFrom<GraphNode>(path[0]);
        Assert.IsAssignableFrom<GraphRelation>(path[1]);
    }

    [Fact]
    public void GraphPathBuilder_Add_Node_And_Relation_Clear()
    {
        // Arrange.
        var builder = new GraphPathBuilder();
        builder.Add("First");
        builder.Add(GraphRelation.Children);

        // Act.
        builder.Clear();

        // Assert.
        var path = builder.ToPath();
        Assert.Equal(0, path.Length);
    }
}
