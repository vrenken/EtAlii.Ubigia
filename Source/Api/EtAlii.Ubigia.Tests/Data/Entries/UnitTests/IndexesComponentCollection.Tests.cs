namespace EtAlii.Ubigia.Tests;

using Xunit;

public class IndexesComponentCollectionTests
{
    private readonly TestIdentifierFactory _testIdentifierFactory;

    public IndexesComponentCollectionTests()
    {
        _testIdentifierFactory = new TestIdentifierFactory();

    }
    [Fact]
    public void IndexesComponentCollection_Create()
    {
        // Arrange.

        // Act.
        var collection = new IndexesComponentCollection();

        // Assert.
        Assert.NotNull(collection);
        Assert.Empty(collection);
    }


    [Fact]
    public void IndexesComponentCollection_Add_Relation_Add()
    {
        // Arrange.
        var collection = new IndexesComponentCollection();
        var identifier = _testIdentifierFactory.Create();

        // Act.
        collection.Add(new[] { Relation.NewRelation(identifier) }, true);

        // Assert.
        Assert.Single(collection);
    }

    [Fact]
    public void IndexesComponentCollection_Add_Relation_Contains_Fail()
    {
        // Arrange.
        var collection = new IndexesComponentCollection();
        var identifier = _testIdentifierFactory.Create();

        // Act.

        // Assert.
        Assert.NotNull(collection);
        Assert.False(collection.Contains(identifier));
    }

    [Fact]
    public void IndexesComponentCollection_Add_Relation_Contains_Single()
    {
        // Arrange.
        var collection = new IndexesComponentCollection();
        var identifier = _testIdentifierFactory.Create();

        // Act.
        collection.Add(new[] { Relation.NewRelation(identifier) }, true);

        // Assert.
        Assert.NotNull(collection);
        Assert.True(collection.Contains(identifier));
    }

    [Fact]
    public void IndexesComponentCollection_Add_Relation_Contains_Multiple()
    {
        // Arrange.
        var collection = new IndexesComponentCollection();
        var first = _testIdentifierFactory.Create();
        var second = _testIdentifierFactory.Create();
        var third = _testIdentifierFactory.Create();

        // Act.
        collection.Add(new[]
        {
            Relation.NewRelation(first),
            Relation.NewRelation(second),
            Relation.NewRelation(third)
        }, true);

        // Assert.
        Assert.NotNull(collection);
        Assert.True(collection.Contains(second));
    }

    [Fact]
    public void IndexesComponentCollection_Add_Relation_Contains_Multiple_Fail()
    {
        // Arrange.
        var collection = new IndexesComponentCollection();
        var first = _testIdentifierFactory.Create();
        var second = _testIdentifierFactory.Create();
        var third = _testIdentifierFactory.Create();

        // Act.
        collection.Add(new[]
        {
            Relation.NewRelation(first),
            Relation.NewRelation(third)
        }, true);

        // Assert.
        Assert.NotNull(collection);
        Assert.False(collection.Contains(second));
    }

    [Fact]
    public void IndexesComponentCollection_Add_Identifier_Add()
    {
        // Arrange.
        var collection = new IndexesComponentCollection();
        var identifier = _testIdentifierFactory.Create();

        // Act.
        collection.Add(identifier);

        // Assert.
        Assert.Single(collection);
    }

    [Fact]
    public void IndexesComponentCollection_Add_Identifier_Contains_Fail()
    {
        // Arrange.
        var collection = new IndexesComponentCollection();
        var identifier = _testIdentifierFactory.Create();

        // Act.

        // Assert.
        Assert.NotNull(collection);
        Assert.False(collection.Contains(identifier));
    }

    [Fact]
    public void IndexesComponentCollection_Add_Identifier_Contains_Single()
    {
        // Arrange.
        var collection = new IndexesComponentCollection();
        var identifier = _testIdentifierFactory.Create();

        // Act.
        collection.Add(identifier);

        // Assert.
        Assert.NotNull(collection);
        Assert.True(collection.Contains(identifier));
    }

    [Fact]
    public void IndexesComponentCollection_Add_Identifier_Contains_Multiple()
    {
        // Arrange.
        var collection = new IndexesComponentCollection();
        var first = _testIdentifierFactory.Create();
        var second = _testIdentifierFactory.Create();
        var third = _testIdentifierFactory.Create();

        // Act.
        collection.Add(first);
        collection.Add(second);
        collection.Add(third);

        // Assert.
        Assert.NotNull(collection);
        Assert.True(collection.Contains(second));
    }

    [Fact]
    public void IndexesComponentCollection_Add_Identifier_Contains_Multiple_Fail()
    {
        // Arrange.
        var collection = new IndexesComponentCollection();
        var first = _testIdentifierFactory.Create();
        var second = _testIdentifierFactory.Create();
        var third = _testIdentifierFactory.Create();

        // Act.
        collection.Add(first);
        collection.Add(third);

        // Assert.
        Assert.NotNull(collection);
        Assert.False(collection.Contains(second));
    }
}
