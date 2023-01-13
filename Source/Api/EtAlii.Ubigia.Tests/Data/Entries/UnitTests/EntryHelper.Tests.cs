namespace EtAlii.Ubigia.Tests;

using System;
using System.Linq;
using Xunit;

public class EntryHelperTests
{
    [Fact]
    public void EntryHelper_Compose_Identifier()
    {
        // Arrange.
        var identifier = new TestIdentifierFactory().Create();
        var identifierComponent = new IdentifierComponent { Id = identifier };
        var components = new IComponent[]
        {
            identifierComponent
        };

        // Act.
        var entry = EntryHelper.Compose(components);

        // Assert.
        Assert.NotNull(entry);
        Assert.Equal(identifier, entry.Id);
    }


    [Fact]
    public void EntryHelper_Compose_Identifier_And_Parent()
    {
        // Arrange.
        var identifier = new TestIdentifierFactory().Create();
        var parentIdentifier = new TestIdentifierFactory().Create();
        var identifierComponent = new IdentifierComponent { Id = identifier };
        var parentComponent = new ParentComponent { Relation = Relation.NewRelation(parentIdentifier) };
        var components = new IComponent[]
        {
            identifierComponent,
            parentComponent
        };

        // Act.
        var entry = EntryHelper.Compose(components);

        // Assert.
        Assert.NotNull(entry);
        Assert.Equal(identifier, entry.Id);
        Assert.Equal(parentIdentifier, entry.Parent.Id);
    }

    [Fact]
    public void EntryHelper_Decompose_Identifier_And_Parent()
    {
        // Arrange.
        var identifier = new TestIdentifierFactory().Create();
        var parentIdentifier = new TestIdentifierFactory().Create();
        var identifierComponent = new IdentifierComponent { Id = identifier };
        var parentComponent = new ParentComponent { Relation = Relation.NewRelation(parentIdentifier) };
        var components = new IComponent[]
        {
            identifierComponent,
            parentComponent
        };
        var entry = EntryHelper.Compose(components);

        // Act.
        var decomposedComponents = EntryHelper.Decompose(entry);

        // Assert.
        Assert.Equal(2, decomposedComponents.Count());
        Assert.Equal(identifier, decomposedComponents.OfType<IdentifierComponent>().Single().Id);
        Assert.Equal(parentIdentifier, decomposedComponents.OfType<ParentComponent>().Single().Relation.Id);
    }

    [Fact]
    public void EntryHelper_Compose_Identifier_And_Tag()
    {
        // Arrange.
        var identifier = new TestIdentifierFactory().Create();
        var tag = Guid.NewGuid().ToString();
        var identifierComponent = new IdentifierComponent { Id = identifier };
        var tagComponent = new TagComponent { Tag = tag };
        var components = new IComponent[]
        {
            identifierComponent,
            tagComponent
        };

        // Act.
        var entry = EntryHelper.Compose(components);

        // Assert.
        Assert.NotNull(entry);
        Assert.Equal(identifier, entry.Id);
        Assert.Equal(tag, entry.Tag);
    }

    [Fact]
    public void EntryHelper_Decompose_Identifier_And_Tag()
    {
        // Arrange.
        var identifier = new TestIdentifierFactory().Create();
        var tag = Guid.NewGuid().ToString();
        var identifierComponent = new IdentifierComponent { Id = identifier };
        var tagComponent = new TagComponent { Tag = tag };
        var components = new IComponent[]
        {
            identifierComponent,
            tagComponent
        };
        var entry = EntryHelper.Compose(components);

        // Act.
        var decomposedComponents = EntryHelper.Decompose(entry);

        // Assert.
        Assert.Equal(2, decomposedComponents.Count());
        Assert.Equal(identifier, decomposedComponents.OfType<IdentifierComponent>().Single().Id);
        Assert.Equal(tag, decomposedComponents.OfType<TagComponent>().Single().Tag);
    }

    [Fact]
    public void EntryHelper_Compose_Identifier_And_Type()
    {
        // Arrange.
        var identifier = new TestIdentifierFactory().Create();
        var type = Guid.NewGuid().ToString();
        var identifierComponent = new IdentifierComponent { Id = identifier };
        var typeComponent = new TypeComponent { Type = type };
        var components = new IComponent[]
        {
            identifierComponent,
            typeComponent
        };

        // Act.
        var entry = EntryHelper.Compose(components);

        // Assert.
        Assert.NotNull(entry);
        Assert.Equal(identifier, entry.Id);
        Assert.Equal(type, entry.Type);
    }

    [Fact]
    public void EntryHelper_Decompose_Identifier_And_Type()
    {
        // Arrange.
        var identifier = new TestIdentifierFactory().Create();
        var type = Guid.NewGuid().ToString();
        var identifierComponent = new IdentifierComponent { Id = identifier };
        var typeComponent = new TypeComponent { Type = type };
        var components = new IComponent[]
        {
            identifierComponent,
            typeComponent
        };
        var entry = EntryHelper.Compose(components);

        // Act.
        var decomposedComponents = EntryHelper.Decompose(entry);

        // Assert.
        Assert.Equal(2, decomposedComponents.Count());
        Assert.Equal(identifier, decomposedComponents.OfType<IdentifierComponent>().Single().Id);
        Assert.Equal(type, decomposedComponents.OfType<TypeComponent>().Single().Type);
    }

}
