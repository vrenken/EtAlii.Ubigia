namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public class UpdatesComponentCollectionTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void UpdatesComponentCollection_Create()
        {
            // Arrange.

            // Act.
            var updatesComponentCollection = new UpdatesComponentCollection();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void UpdatesComponentCollection_Add()
        {
            // Arrange.
            var updatesComponentCollection = new UpdatesComponentCollection();
            var identifier = Identifier.NewIdentifier(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            // Act.
            updatesComponentCollection.Add(identifier);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void UpdatesComponentCollection_Add_Identifier_Empty()
        {
            // Arrange.
            var updatesComponentCollection = new UpdatesComponentCollection();
            var identifier = Identifier.Empty;

            // Act.
            updatesComponentCollection.Add(identifier);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void UpdatesComponentCollection_Add_Relation_Empty()
        {
            // Arrange.
            var updatesComponentCollection = new UpdatesComponentCollection();
            var identifier = Identifier.NewIdentifier(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var relation = Relation.NewRelation(identifier);
            var updatesComponent = new UpdatesComponent { Relations = new[] { relation } };

            // Act.
            updatesComponentCollection.Add(updatesComponent);

            // Assert.
            Assert.Contains(updatesComponent, updatesComponentCollection);
        }
    }
}
