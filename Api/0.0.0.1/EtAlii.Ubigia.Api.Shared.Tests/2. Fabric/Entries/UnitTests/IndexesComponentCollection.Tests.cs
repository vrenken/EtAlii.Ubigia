namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using EtAlii.Ubigia.Storage.Tests;
    using Xunit;
    using TestAssembly = EtAlii.Ubigia.Api.Tests.TestAssembly;

    
    public class IndexesComponentCollection_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexesComponentCollection_Create()
        {
            // Arrange.

            // Act.
            var collection = new IndexesComponentCollection();

            // Assert.
            Assert.NotNull(collection);
            Assert.Equal(0, collection.Count);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Relation_Add()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = TestIdentifier.Create();

            // Act.
            collection.Add(new Relation[] { Relation.NewRelation(identifier) }, true);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Relation_Contains_Fail()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = TestIdentifier.Create();

            // Act.

            // Assert.
            Assert.NotNull(collection);
            Assert.False(collection.Contains(identifier));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Relation_Contains_Single()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = TestIdentifier.Create();

            // Act.
            collection.Add(new Relation[] { Relation.NewRelation(identifier) }, true);

            // Assert.
            Assert.NotNull(collection);
            Assert.True(collection.Contains(identifier));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Relation_Contains_Multiple()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var first = TestIdentifier.Create();
            var second = TestIdentifier.Create();
            var third = TestIdentifier.Create();

            // Act.
            collection.Add(new Relation[]
            {
                Relation.NewRelation(first),
                Relation.NewRelation(second),
                Relation.NewRelation(third)
            }, true);

            // Assert.
            Assert.NotNull(collection);
            Assert.True(collection.Contains(second));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Relation_Contains_Multiple_Fail()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var first = TestIdentifier.Create();
            var second = TestIdentifier.Create();
            var third = TestIdentifier.Create();

            // Act.
            collection.Add(new Relation[]
            {
                Relation.NewRelation(first),
                Relation.NewRelation(third)
            }, true);

            // Assert.
            Assert.NotNull(collection);
            Assert.False(collection.Contains(second));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Identifier_Add()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = TestIdentifier.Create();

            // Act.
            collection.Add(identifier);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Identifier_Contains_Fail()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = TestIdentifier.Create();

            // Act.

            // Assert.
            Assert.NotNull(collection);
            Assert.False(collection.Contains(identifier));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Identifier_Contains_Single()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = TestIdentifier.Create();

            // Act.
            collection.Add(identifier);

            // Assert.
            Assert.NotNull(collection);
            Assert.True(collection.Contains(identifier));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Identifier_Contains_Multiple()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var first = TestIdentifier.Create();
            var second = TestIdentifier.Create();
            var third = TestIdentifier.Create();

            // Act.
            collection.Add(first);
            collection.Add(second);
            collection.Add(third);

            // Assert.
            Assert.NotNull(collection);
            Assert.True(collection.Contains(second));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Identifier_Contains_Multiple_Fail()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var first = TestIdentifier.Create();
            var second = TestIdentifier.Create();
            var third = TestIdentifier.Create();

            // Act.
            collection.Add(first);
            collection.Add(third);

            // Assert.
            Assert.NotNull(collection);
            Assert.False(collection.Contains(second));
        }
    }
}
