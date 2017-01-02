namespace EtAlii.Servus.Api.Data.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Storage.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;


    [TestClass]
    public class DataConnection_Content_Tests : EtAlii.Servus.Api.Data.Tests.TestBase
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

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Content_Store()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var content = TestContentFactory.Create();

            // Act.
            connection.Content.Store(entry.Id, content);

            // Assert.
            Assert.IsTrue(content.Stored); 
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Content_Retrieve_Complete()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);

            var datas = TestContentFactory.CreateData(100, 500, 3);
            var contentDefinition = TestContentDefinitionFactory.Create(datas);
            connection.Content.StoreDefinition(entry.Id, contentDefinition);
            var content = TestContentFactory.Create(3);
            var contentParts = TestContentFactory.CreateParts(datas);

            // Act.
            connection.Content.Store(entry.Id, content);
            foreach (var contentPart in contentParts)
            {
                connection.Content.Store(entry.Id, contentPart);
            }

            var retrievedContent = connection.Content.Retrieve(entry.Id);

            // Assert.
            Assert.AreEqual(content.TotalParts, retrievedContent.Summary.TotalParts);
            Assert.IsTrue(retrievedContent.Summary.IsComplete);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Content_Retrieve_Incomplete()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var datas = TestContentFactory.CreateData(100, 500, 3);
            var contentdefinition = TestContentDefinitionFactory.Create(datas);
            connection.Content.StoreDefinition(entry.Id, contentdefinition);
            var content = TestContentFactory.Create(3);
            var contentPartFirst = TestContentFactory.CreatePart(datas[0], 0);
            var contentPartSecond = TestContentFactory.CreatePart(datas[1], 1);
            var contentPartThird = TestContentFactory.CreatePart(datas[2], 2);

            // Act.
            connection.Content.Store(entry.Id, content);
            connection.Content.Store(entry.Id, contentPartFirst);
            var retrievedContent = connection.Content.Retrieve(entry.Id);

            // Assert.
            Assert.IsFalse(retrievedContent.Summary.IsComplete);

            // Act.
            connection.Content.Store(entry.Id, contentPartSecond);
            retrievedContent = connection.Content.Retrieve(entry.Id);

            // Assert.
            Assert.IsFalse(retrievedContent.Summary.IsComplete);

            // Act.
            connection.Content.Store(entry.Id, contentPartThird);
            retrievedContent = connection.Content.Retrieve(entry.Id);

            // Assert.
            Assert.AreEqual(content.TotalParts, retrievedContent.Summary.TotalParts);
            Assert.IsTrue(retrievedContent.Summary.IsComplete);
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public void DataConnection_ContentDefinition_Store_And_Retrieve_Check_Size()
        //{
        //    var connection = CreateDataConnection();

        //    var root = connection.Roots.Get(DefaultRoot.Hierarchy);
        //    var entry = connection.Entries.Get(root.Identifier);

        //    var contentDefinition = Create();
        //    connection.Content.StoreDefinition(entry.Id, contentDefinition);

        //    var retrievedContentDefinition = connection.Content.RetrieveDefinition(entry.Id);

        //    Assert.AreEqual(contentDefinition.Size, retrievedContentDefinition.Size);
        //}

        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public void DataConnection_ContentDefinition_Store_And_Retrieve_Check_Checksum()
        //{
        //    var connection = CreateDataConnection();

        //    var root = connection.Roots.Get(DefaultRoot.Hierarchy);
        //    var entry = connection.Entries.Get(root.Identifier);

        //    var contentDefinition = Create();
        //    connection.Content.StoreDefinition(entry.Id, contentDefinition);

        //    var retrievedContentDefinition = connection.Content.RetrieveDefinition(entry.Id);

        //    Assert.AreEqual(contentDefinition.Checksum, retrievedContentDefinition.Checksum);
        //}


        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public void DataConnection_ContentDefinition_Store_And_Retrieve_Check_Parts()
        //{
        //    var connection = CreateDataConnection();

        //    var root = connection.Roots.Get(DefaultRoot.Hierarchy);
        //    var entry = connection.Entries.Get(root.Identifier);

        //    var contentDefinition = Create();
        //    connection.Content.StoreDefinition(entry.Id, contentDefinition);

        //    var retrievedContentDefinition = connection.Content.RetrieveDefinition(entry.Id);

        //    Assert.AreEqual(contentDefinition.Parts.Count, retrievedContentDefinition.Parts.Count());
        //    for (int i = 0; i < contentDefinition.Parts.Count; i++)
        //    {
        //        Assert.AreEqual(contentDefinition.Parts[i].Checksum, retrievedContentDefinition.Parts.ElementAt(i).Checksum);
        //        Assert.AreEqual(contentDefinition.Parts[i].Size, retrievedContentDefinition.Parts.ElementAt(i).Size);
        //    }
        //}
    }
}
