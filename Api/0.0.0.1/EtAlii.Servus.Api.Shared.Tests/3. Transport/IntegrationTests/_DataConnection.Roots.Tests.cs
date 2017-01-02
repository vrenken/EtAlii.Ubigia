namespace EtAlii.Servus.Api.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.Infrastructure;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    [TestClass]
    public sealed class DataConnection_Roots_Tests : EtAlii.Servus.Api.Tests.TestBase
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
        public void DataConnection_Roots_Add()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var name = Guid.NewGuid().ToString();

            var root = connection.Roots.Add(name);

            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Event_Added()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var name = Guid.NewGuid().ToString();

            var addedEvent = new ManualResetEvent(false);
            var addedId = Guid.Empty;

            connection.Roots.Added += (id) => { addedId = id; addedEvent.Set(); };
            var root = connection.Roots.Add(name);
            addedEvent.WaitOne(TimeSpan.FromSeconds(10));

            Assert.AreNotEqual(Guid.Empty, addedId);
            Assert.AreEqual(root.Id, addedId);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Add_Multiple()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                var root = connection.Roots.Add(name);

                Assert.IsNotNull(root);
                Assert.AreEqual(name, root.Name);
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Get_By_Id()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var name = Guid.NewGuid().ToString();

            var root = connection.Roots.Add(name);

            root = connection.Roots.Get(root.Id);

            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Get_By_Name()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var name = Guid.NewGuid().ToString();

            var root = connection.Roots.Add(name);

            root = connection.Roots.Get(root.Name);

            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Get_Multiple()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                var root = connection.Roots.Add(name);

                root = connection.Roots.Get(root.Id);

                Assert.IsNotNull(root);
                Assert.AreEqual(name, root.Name);
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Get_Multiple_First_Full_Add()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var roots = new List<Root>();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                var root = connection.Roots.Add(name);
                Assert.IsNotNull(root);
                Assert.AreEqual(name, root.Name);
                roots.Add(root);
            }

            foreach (var root in roots)
            {
                var retrievedRoot = connection.Roots.Get(root.Id);

                Assert.IsNotNull(retrievedRoot);
                Assert.AreEqual(root.Name, retrievedRoot.Name);
            }
        }
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Get_No_Roots()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var retrievedRoots = connection.Roots.GetAll();
            Assert.IsNotNull(retrievedRoots);
            Assert.AreEqual(DefaultRoot.All.Length, retrievedRoots.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Get_All()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);

            var roots = new List<Root>();

            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                var root = connection.Roots.Add(name);
                Assert.IsNotNull(root);
                Assert.AreEqual(name, root.Name);
                roots.Add(root);
            }

            var retrievedRoots = connection.Roots.GetAll();

            Assert.AreEqual(roots.Count + DefaultRoot.All.Length, retrievedRoots.Count());

            foreach (var root in roots)
            {
                var matchingRoot = retrievedRoots.Single(r => r.Id == root.Id);
                Assert.IsNotNull(matchingRoot);
                Assert.AreEqual(root.Name, matchingRoot.Name);
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Change()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var name = Guid.NewGuid().ToString();

            var root = connection.Roots.Add(name);
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);

            root = connection.Roots.Get(root.Id);
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);

            name = Guid.NewGuid().ToString();
            root = connection.Roots.Change(root.Id, name);
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);

            root = connection.Roots.Get(root.Id);
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Event_Changed()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var name = Guid.NewGuid().ToString();

            var root = connection.Roots.Add(name);

            var changedEvent = new ManualResetEvent(false);
            var changedId = Guid.Empty;

            connection.Roots.Changed += (id) => { changedId = id; changedEvent.Set(); };

            name = Guid.NewGuid().ToString();
            root = connection.Roots.Change(root.Id, name);

            changedEvent.WaitOne(TimeSpan.FromSeconds(10));

            Assert.AreNotEqual(Guid.Empty, changedId);
            Assert.AreEqual(root.Id, changedId);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Remove()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var name = Guid.NewGuid().ToString();

            var root = connection.Roots.Add(name);
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);

            root = connection.Roots.Get(root.Id);
            Assert.IsNotNull(root);

            connection.Roots.Remove(root.Id);

            root = connection.Roots.Get(root.Id);
            Assert.IsNull(root);
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public void DataConnection_Roots_Event_Removed()
        //{
        //    var connection = ApiTestHelper.CreateDataConnection(Configuration, SpaceName, AccountName, AccountPassword);

        //    var name = Guid.NewGuid().ToString();

        //    var root = connection.Roots.Add(name);

        //    var removedEvent = new ManualResetEvent(false);
        //    var removedId = Guid.Empty; 

        //    connection.Roots.Removed += (id) => { removedId = id; removedEvent.Set(); };
        //    connection.Roots.Remove(root.Id);

        //    removedEvent.WaitOne(TimeSpan.FromSeconds(10));

        //    Assert.AreNotEqual(Guid.Empty, removedId);
        //    Assert.AreNotEqual(root.Id, removedId);
        //}

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Delete_Non_Existing()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var id = Guid.NewGuid();

            // Act.
            var act = new Action(() =>
            {
                connection.Roots.Remove(id);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Change_Non_Existing()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();

            // Act.
            var act = new Action(() =>
            {
                connection.Roots.Change(id, name);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Add_With_Closed_Connection()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);
            
            // Act.
            var act = new Action(() =>
            {
                connection.Roots.Add(Guid.NewGuid().ToString());
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Get_With_Closed_Connection()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);

            // Act.
            var act = new Action(() =>
            {
                connection.Roots.Get(Guid.NewGuid());
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Remove_With_Closed_Connection()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);

            // Act.
            var act = new Action(() =>
            {
                connection.Roots.Remove(Guid.NewGuid());
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_GetAll_With_Closed_Connection()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);

            // Act.
            var act = new Action(() =>
            {
                connection.Roots.GetAll();
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Change_With_Closed_Connection()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);

            // Act.
            var act = new Action(() =>
            {
                connection.Roots.Change(Guid.NewGuid(), Guid.NewGuid().ToString());
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Roots_Add_Already_Existing_Storage()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var root = connection.Roots.Add(name);
            Assert.IsNotNull(root);

            // Act.
            var act = new Action(() =>
            {
                root = connection.Roots.Add(name);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }
    }
}
