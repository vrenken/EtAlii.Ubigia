namespace EtAlii.Servus.Api.Data.UnitTests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;

    [TestClass]
    public class IndexesComponentCollection_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexesComponentCollection_Create()
        {
            // Arrange.

            // Act.
            var collection = new IndexesComponentCollection();

            // Assert.
            Assert.IsNotNull(collection);
            Assert.AreEqual(0, collection.Count);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Relation_Add()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = ApiTestHelper.CreateRandomIdentifier();

            // Act.
            collection.Add(new Relation[] { Relation.NewRelation(identifier) }, true);

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Relation_Contains_Fail()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = ApiTestHelper.CreateRandomIdentifier();

            // Act.

            // Assert.
            Assert.IsNotNull(collection);
            Assert.IsFalse(collection.Contains(identifier));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Relation_Contains_Single()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = ApiTestHelper.CreateRandomIdentifier();

            // Act.
            collection.Add(new Relation[] { Relation.NewRelation(identifier) }, true);

            // Assert.
            Assert.IsNotNull(collection);
            Assert.IsTrue(collection.Contains(identifier));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Relation_Contains_Multiple()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var first = ApiTestHelper.CreateRandomIdentifier();
            var second = ApiTestHelper.CreateRandomIdentifier();
            var third = ApiTestHelper.CreateRandomIdentifier();

            // Act.
            collection.Add(new Relation[]
            {
                Relation.NewRelation(first),
                Relation.NewRelation(second),
                Relation.NewRelation(third)
            }, true);

            // Assert.
            Assert.IsNotNull(collection);
            Assert.IsTrue(collection.Contains(second));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Relation_Contains_Multiple_Fail()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var first = ApiTestHelper.CreateRandomIdentifier();
            var second = ApiTestHelper.CreateRandomIdentifier();
            var third = ApiTestHelper.CreateRandomIdentifier();

            // Act.
            collection.Add(new Relation[]
            {
                Relation.NewRelation(first),
                Relation.NewRelation(third)
            }, true);

            // Assert.
            Assert.IsNotNull(collection);
            Assert.IsFalse(collection.Contains(second));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Identifier_Add()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = ApiTestHelper.CreateRandomIdentifier();

            // Act.
            collection.Add(identifier);

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Identifier_Contains_Fail()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = ApiTestHelper.CreateRandomIdentifier();

            // Act.

            // Assert.
            Assert.IsNotNull(collection);
            Assert.IsFalse(collection.Contains(identifier));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Identifier_Contains_Single()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var identifier = ApiTestHelper.CreateRandomIdentifier();

            // Act.
            collection.Add(identifier);

            // Assert.
            Assert.IsNotNull(collection);
            Assert.IsTrue(collection.Contains(identifier));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Identifier_Contains_Multiple()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var first = ApiTestHelper.CreateRandomIdentifier();
            var second = ApiTestHelper.CreateRandomIdentifier();
            var third = ApiTestHelper.CreateRandomIdentifier();

            // Act.
            collection.Add(first);
            collection.Add(second);
            collection.Add(third);

            // Assert.
            Assert.IsNotNull(collection);
            Assert.IsTrue(collection.Contains(second));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexesComponentCollection_Add_Identifier_Contains_Multiple_Fail()
        {
            // Arrange.
            var collection = new IndexesComponentCollection();
            var first = ApiTestHelper.CreateRandomIdentifier();
            var second = ApiTestHelper.CreateRandomIdentifier();
            var third = ApiTestHelper.CreateRandomIdentifier();

            // Act.
            collection.Add(first);
            collection.Add(third);

            // Assert.
            Assert.IsNotNull(collection);
            Assert.IsFalse(collection.Contains(second));
        }
    }
}
