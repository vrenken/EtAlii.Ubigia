namespace EtAlii.Servus.Api.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.Infrastructure;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;


    [TestClass]
    public class DataConnection_ContentDefinition_Tests : EtAlii.Servus.Api.Tests.TestBase
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ApiTestHelper.AddTestAccountAndSpace(Host, AccountName, AccountPassword, SpaceName);
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Store()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinition = TestContentDefinitionFactory.Create();

            // Act.
            connection.Content.StoreDefinition(entry.Id, contentDefinition);

            // Assert.
            Assert.IsTrue(contentDefinition.Stored);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Store_Null()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinitionPart = (ContentDefinitionPart)null;

            // Act.
            var act = new Action(() =>
            {
                connection.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            });

            // Assert.
            ExceptionAssert.Throws<ArgumentNullException>(act);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Store_Part()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinition = TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 3;
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart(0);
            connection.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            connection.Content.StoreDefinition(entry.Id, contentDefinitionPart);

            // Assert.
            Assert.IsTrue(contentDefinitionPart.Stored);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Store_Part_Outside_Bounds()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinition = TestContentDefinitionFactory.Create();
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart(2);
            connection.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = new Action(() =>
            {
                connection.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Store_Part_At_Bounds()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinition = TestContentDefinitionFactory.Create();
            contentDefinition.TotalParts = 1;
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart(1);
            connection.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = new Action(() =>
            {
                connection.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Store_Part_Before_ContentDefinition()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinition = TestContentDefinitionFactory.Create();
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart(0);
            //connection.Content.StoreDefinition(entry.Id, contentDefinition);

            var act = new Action(() =>
            {
                connection.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Store_Existing_Part()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinition = TestContentDefinitionFactory.Create(10);
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart(5);
            connection.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = new Action(() =>
            {
                connection.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Store_Invalid_Part()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinition = TestContentDefinitionFactory.Create(10);
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart(15);
            connection.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = new Action(() =>
            {
                connection.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Store_Part_Null()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinition = TestContentDefinitionFactory.Create();
            var contentDefinitionPart = (ContentDefinitionPart)null;
            connection.Content.StoreDefinition(entry.Id, contentDefinition);

            // Act.
            var act = new Action(() =>
            {
                connection.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            });

            // Assert.
            ExceptionAssert.Throws<ArgumentNullException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Retrieve()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinition = TestContentDefinitionFactory.Create();

            // Act.
            connection.Content.StoreDefinition(entry.Id, contentDefinition);
            var retrievedContentDefinition = connection.Content.RetrieveDefinition(entry.Id);

            // Assert.
            AssertData.AreEqual(contentDefinition, retrievedContentDefinition, false);
            Assert.AreEqual((UInt64)contentDefinition.Parts.Count, retrievedContentDefinition.Summary.TotalParts);
            Assert.IsTrue(retrievedContentDefinition.Summary.IsComplete);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Retrieve_Incomplete_1()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinition = TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 2;
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart(1);

            // Act.
            connection.Content.StoreDefinition(entry.Id, contentDefinition);
            connection.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            var retrievedContentDefinition = connection.Content.RetrieveDefinition(entry.Id);

            // Assert.
            Assert.AreEqual(contentDefinition.TotalParts, retrievedContentDefinition.Summary.TotalParts);
            Assert.IsFalse(retrievedContentDefinition.Summary.IsComplete);
            Assert.AreEqual(1, retrievedContentDefinition.Summary.AvailableParts.Length);
            Assert.AreEqual((UInt64)1, retrievedContentDefinition.Summary.AvailableParts.First());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_ContentDefinition_Retrieve_Incomplete_2()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            var entry = connection.Entries.Get(root.Identifier);
            var contentDefinition = TestContentDefinitionFactory.Create(0);
            contentDefinition.TotalParts = 3;
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart(2);

            // Act.
            connection.Content.StoreDefinition(entry.Id, contentDefinition);
            connection.Content.StoreDefinition(entry.Id, contentDefinitionPart);
            var retrievedContentDefinition = connection.Content.RetrieveDefinition(entry.Id);

            // Assert.
            Assert.AreEqual(contentDefinition.TotalParts, retrievedContentDefinition.Summary.TotalParts);
            Assert.IsFalse(retrievedContentDefinition.Summary.IsComplete);
            Assert.AreEqual(1, retrievedContentDefinition.Summary.AvailableParts.Length);
            Assert.AreEqual((UInt64)2, retrievedContentDefinition.Summary.AvailableParts.First());
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
