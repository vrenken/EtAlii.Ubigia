namespace EtAlii.Servus.Api.Helpers.IntegrationTests
{
    using EtAlii.Servus.Api.Helpers;
    using EtAlii.Servus.Api.Shared.Tests;
    using EtAlii.Servus.Api;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CollectionManager_Tests : ApiTestBase
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public void CollectionManager_Construct()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Configuration, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);

            // Act.
            var collectionManager = new CollectionManager(connection);

            // Assert.
        }

        [TestMethod, Ignore]
        public void CollectionManager_Create()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Configuration, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var collectionManager = new CollectionManager(connection);

            // Act.
            collectionManager.Create(root.Identifier, "TestCollection");

            // Assert.
            // TODO.
        }

        [TestMethod, Ignore]
        public void CollectionManager_Add()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Configuration, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var collectionManager = new CollectionManager(connection);
            var entry = connection.Entries.Prepare();
            var collectionIdentifier = collectionManager.Create(root.Identifier, "TestCollection");

            // Act.
            collectionManager.Add(collectionIdentifier, entry.Id);

            // Assert.
            // TODO.
        }

        [TestMethod, Ignore]
        public void CollectionManager_Remove()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Configuration, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var collectionManager = new CollectionManager(connection);
            var entry = connection.Entries.Prepare();
            var collectionIdentifier = collectionManager.Create(root.Identifier, "TestCollection");
            collectionManager.Add(collectionIdentifier, entry.Id);

            // Act.
            collectionManager.Remove(collectionIdentifier, entry.Id);

            // Assert.
            // TODO.
        }
    }
}
