namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class UpdatesComponentCollection_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void UpdatesComponentCollection_Create()
        {
            // Arrange.

            // Act.
            var updatesComponentCollection = new UpdatesComponentCollection();

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void UpdatesComponentCollection_Add()
        {
            // Arrange.
            var updatesComponentCollection = new UpdatesComponentCollection();
            var identifier = Identifier.NewIdentifier(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            // Act.
            updatesComponentCollection.Add(identifier);

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void UpdatesComponentCollection_Add_Identifier_Empty()
        {
            // Arrange.
            var updatesComponentCollection = new UpdatesComponentCollection();
            var identifier = Identifier.Empty;

            // Act.
            updatesComponentCollection.Add(identifier);

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void UpdatesComponentCollection_Add_Relation_Empty()
        {
            // Arrange.
            var updatesComponentCollection = new UpdatesComponentCollection();
            var identifier = Identifier.NewIdentifier(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var relation = Relation.NewRelation(identifier);
            var updatesComponent = new UpdatesComponent { Relations = new Relation[] { relation } };

            // Act.
            updatesComponentCollection.Add(updatesComponent);

            // Assert.
            Assert.IsTrue(updatesComponentCollection.Contains(updatesComponent));
        }
    }
}
