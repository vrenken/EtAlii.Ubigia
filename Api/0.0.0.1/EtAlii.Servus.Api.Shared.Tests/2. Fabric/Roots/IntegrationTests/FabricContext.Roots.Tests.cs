namespace EtAlii.Servus.Api.Fabric.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    [TestClass]
    public sealed class FabricContext_Roots_Tests
    {
        private IFabricContext _fabric;
        private static ITransportTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(async () =>
            {
                _testContext = new TransportTestContextFactory().Create();
                await _testContext.Start();
            });
            task.Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var task = Task.Run(async () =>
            {
                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }


        [TestInitialize]
        public void TestInitialize()
        {
            var task = Task.Run(async () =>
            {
                var connection = await _testContext.CreateDataConnection();
                var fabricContextConfiguration = new FabricContextConfiguration()
                    .Use(connection);
                _fabric = new FabricContextFactory().Create(fabricContextConfiguration);
            });
            task.Wait();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var task = Task.Run(() =>
            {
                _fabric.Dispose();
                _fabric = null;
            });
            task.Wait();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Add()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();

            // Act.
            var root = await _fabric.Roots.Add(name);

            // Assert.
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Event_Added()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var addedEvent = new ManualResetEvent(false);
            var addedId = Guid.Empty;
            _fabric.Roots.Added += (id) => { addedId = id; addedEvent.Set(); };
            
            // Act.
            var root = await _fabric.Roots.Add(name);
            addedEvent.WaitOne(TimeSpan.FromSeconds(10));

            // Assert.
            Assert.AreNotEqual(Guid.Empty, addedId);
            Assert.AreEqual(root.Id, addedId);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Add_Multiple()
        {
            for (int i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();

                // Act.
                var root = await _fabric.Roots.Add(name);

                // Assert.
                Assert.IsNotNull(root);
                Assert.AreEqual(name, root.Name);
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_By_Id()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabric.Roots.Add(name);

            // Act.
            root = await _fabric.Roots.Get(root.Id);

            // Assert.
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_By_Name()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabric.Roots.Add(name);

            // Act.
            root = await _fabric.Roots.Get(root.Name);

            // Assert.
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_Multiple()
        {
            for (int i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();
                var root = await _fabric.Roots.Add(name);

                // Act.
                root = await _fabric.Roots.Get(root.Id);

                // Assert.
                Assert.IsNotNull(root);
                Assert.AreEqual(name, root.Name);
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_Multiple_First_Full_Add()
        {
            // Arrange.
            var roots = new List<Root>();
            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var root = await _fabric.Roots.Add(name);
                Assert.IsNotNull(root);
                Assert.AreEqual(name, root.Name);
                roots.Add(root);
            }

            foreach (var root in roots)
            {
                // Act.
                var retrievedRoot = await _fabric.Roots.Get(root.Id);

                // Assert.
                Assert.IsNotNull(retrievedRoot);
                Assert.AreEqual(root.Name, retrievedRoot.Name);
            }
        }
        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_No_Roots()
        {
            // Arrange.
            var connection = await _testContext.CreateDataConnection(true);
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(connection);
            var fabric = new FabricContextFactory().Create(fabricContextConfiguration);

            // Act.
            var retrievedRoots = await fabric.Roots.GetAll();

            // Assert.
            Assert.IsNotNull(retrievedRoots);
            Assert.AreEqual(SpaceTemplate.Data.RootsToCreate.Length, retrievedRoots.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_All()
        {
            // Arrange.
            var connection = await _testContext.CreateDataConnection(true);
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(connection);
            var fabric = new FabricContextFactory().Create(fabricContextConfiguration);
            var roots = new List<Root>();
            for (int i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                var root = await fabric.Roots.Add(name);
                Assert.IsNotNull(root);
                Assert.AreEqual(name, root.Name);
                roots.Add(root);
            }

            // Act.
            var retrievedRoots = await fabric.Roots.GetAll();

            // Assert.
            Assert.AreEqual(roots.Count + SpaceTemplate.Data.RootsToCreate.Length, retrievedRoots.Count());
            foreach (var root in roots)
            {
                var matchingRoot = retrievedRoots.Single(r => r.Id == root.Id);
                Assert.IsNotNull(matchingRoot);
                Assert.AreEqual(root.Name, matchingRoot.Name);
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Change()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();

            var root = await _fabric.Roots.Add(name);
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
            root = await _fabric.Roots.Get(root.Id);
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
            name = Guid.NewGuid().ToString();

            // Act.
            root = await _fabric.Roots.Change(root.Id, name);

            // Assert.
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
            root = await _fabric.Roots.Get(root.Id);
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
        }

        // TODO: THe roots changed event should be raised, right?
        //[TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Event_Changed()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabric.Roots.Add(name);
            var changedEvent = new ManualResetEvent(false);
            var changedId = Guid.Empty;
            _fabric.Roots.Changed += (id) => { changedId = id; changedEvent.Set(); };
            name = Guid.NewGuid().ToString();

            // Act.
            root = await _fabric.Roots.Change(root.Id, name);
            changedEvent.WaitOne(TimeSpan.FromSeconds(10));

            // Assert.
            Assert.AreNotEqual(Guid.Empty, changedId);
            Assert.AreEqual(root.Id, changedId);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Remove()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabric.Roots.Add(name);
            Assert.IsNotNull(root);
            Assert.AreEqual(name, root.Name);
            root = await _fabric.Roots.Get(root.Id);
            Assert.IsNotNull(root);

            // Act.
            await _fabric.Roots.Remove(root.Id);

            // Assert.
            root = await _fabric.Roots.Get(root.Id);
            Assert.IsNull(root);
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public async Task FabricContext_Roots_Event_Removed()
        //{
        //    var name = Guid.NewGuid().ToString();

        //    var root = await connection.Roots.Add(name);

        //    var removedEvent = new ManualResetEvent(false);
        //    var removedId = Guid.Empty; 

        //    connection.Roots.Removed += (id) => { removedId = id; removedEvent.Set(); };
        //    await connection.Roots.Remove(root.Id);

        //    removedEvent.WaitOne(TimeSpan.FromSeconds(10));

        //    Assert.AreNotEqual(Guid.Empty, removedId);
        //    Assert.AreNotEqual(root.Id, removedId);
        //}

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Delete_Non_Existing()
        {
            // Arrange.
            var id = Guid.NewGuid();

            // Act.
            var act = _fabric.Roots.Remove(id);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Change_Non_Existing()
        {
            // Arrange.
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();

            // Act.
            var act = _fabric.Roots.Change(id, name);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public async Task FabricContext_Roots_Add_With_Closed_Connection()
        //{
        //    // Arrange.
        //    var connection = await _testContext.CreateDataConnection(false);
        //    var fabricContextConfiguration = new FabricContextConfiguration()
        //        .Use(connection);
            

        //    // Act.
        //    var act = (Action)(() => { var fabric = new FabricContextFactory().Create(fabricContextConfiguration); });
        //    //var act = fabric.Roots.Add(Guid.NewGuid().ToString());

        //    // Assert.
        //    ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        //}

        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public async Task FabricContext_Roots_Get_With_Closed_Connection()
        //{
        //    // Arrange.
        //    var connection = await _testContext.CreateDataConnection(false);
        //    var fabricContextConfiguration = new FabricContextConfiguration()
        //        .Use(connection);

        //    // Act.
        //    var act = (Action)(() => { var fabric = new FabricContextFactory().Create(fabricContextConfiguration); });
        //    //var act = fabric.Roots.Get(Guid.NewGuid());

        //    // Assert.
        //    ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        //}

        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public async Task FabricContext_Roots_Remove_With_Closed_Connection()
        //{
        //    // Arrange.
        //    var connection = await _testContext.CreateDataConnection(false);
        //    var fabricContextConfiguration = new FabricContextConfiguration()
        //        .Use(connection);

        //    // Act.
        //    var act = (Action)(() => { var fabric = new FabricContextFactory().Create(fabricContextConfiguration); });
        //    //var act = fabric.Roots.Remove(Guid.NewGuid());

        //    // Assert.
        //    ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        //}

        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public async Task FabricContext_Roots_GetAll_With_Closed_Connection()
        //{
        //    // Arrange.
        //    var connection = await _testContext.CreateDataConnection(false);
        //    var fabricContextConfiguration = new FabricContextConfiguration()
        //        .Use(connection);

        //    // Act.
        //    var act = (Action)(() => { var fabric = new FabricContextFactory().Create(fabricContextConfiguration); });
        //    //var act = fabric.Roots.GetAll();

        //    // Assert.
        //    ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        //}

        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public async Task FabricContext_Roots_Change_With_Closed_Connection()
        //{
        //    // Arrange.
        //    var connection = await _testContext.CreateDataConnection(false);
        //    var fabricContextConfiguration = new FabricContextConfiguration()
        //        .Use(connection);

        //    // Act.
        //    var act = (Action)(() => { var fabric = new FabricContextFactory().Create(fabricContextConfiguration); });
        //    //act = fabric.Roots.Change(Guid.NewGuid(), Guid.NewGuid().ToString());

        //    // Assert.
        //    ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        //}

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Roots_Add_Already_Existing_Storage()
        {
            // Arrange.
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var root = await _fabric.Roots.Add(name);
            Assert.IsNotNull(root);

            // Act.
            var act = _fabric.Roots.Add(name);

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }
    }
}
