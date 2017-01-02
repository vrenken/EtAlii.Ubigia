namespace EtAlii.Servus.Api.Fabric.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    [TestClass]
    public class FabricContext_Entries_Tests
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
        public async Task FabricContext_Entries_Get_Root()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            Assert.IsNotNull(root);
            var id = root.Identifier;
            Assert.IsNotNull(id);
            Assert.AreNotEqual(Identifier.Empty, id);

            // Act.
            var entry = await _fabric.Entries.Get(id, scope);

            // Assert.
            Assert.IsNotNull(entry);
            Assert.AreEqual(root.Identifier, entry.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Entries_Get_All_Root()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            foreach (var rootName in SpaceTemplate.Data.RootsToCreate)
            {
                var root = await _fabric.Roots.Get(rootName);
                Assert.IsNotNull(root);
                var id = root.Identifier;
                Assert.IsNotNull(id);
                Assert.AreNotEqual(Identifier.Empty, id);

                // Act.
                var entry = await _fabric.Entries.Get(id, scope);

                // Assert.
                Assert.IsNotNull(entry);
                Assert.AreEqual(root.Identifier, entry.Id);
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Entries_Prepare()
        {
            // Arrange.

            // Act.
            var entry = await _fabric.Entries.Prepare();

            // Assert.
            Assert.IsNotNull(entry);
            Assert.AreNotEqual(Identifier.Empty, entry.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Entries_Prepare_Multiple()
        {
            // Arrange.
            var count = 50;
            var entries = new IEditableEntry[count];

            // Act.
            var startTicks = Environment.TickCount;
            for (int i = 0; i < count; i++)
            {
                entries[i] = await _fabric.Entries.Prepare();
            }
            var endTicks = Environment.TickCount;

            // Assert.
            for (int i = 0; i < count; i++)
            {
                Assert.IsNotNull(entries[i]);
                Assert.AreNotEqual(Identifier.Empty, entries[i].Id);
            }
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.IsTrue(duration < 2d, String.Format("{0} entry perparations took: {1} seconds", count, duration));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Entries_Change()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var entry = await _fabric.Entries.Prepare();

            // Act.
            entry = (IEditableEntry)await _fabric.Entries.Change(entry, scope);

            // Assert.
            var retrievedEntry = await _fabric.Entries.Get(entry.Id, scope);
            Assert.IsNotNull(retrievedEntry);
            Assert.AreNotEqual(Identifier.Empty, retrievedEntry.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Entries_Change_Multiple()
        {
            // Arrange.
            var count = 50;
            var scope = new ExecutionScope(false);
            var entries = new IEditableEntry[count];
            for (int i = 0; i < count; i++)
            {
                entries[i] = await _fabric.Entries.Prepare();
            }

            // Act.
            var startTicks = Environment.TickCount;
            for (int i = 0; i < count; i++)
            {
                entries[i] = (IEditableEntry)await _fabric.Entries.Change(entries[i], scope);
            }
            var endTicks = Environment.TickCount;

            // Assert.
            for (int i = 0; i < count; i++)
            {
                var retrievedEntry = await _fabric.Entries.Get(entries[i].Id, scope);
                Assert.IsNotNull(retrievedEntry);
                Assert.AreNotEqual(Identifier.Empty, retrievedEntry.Id);
            }
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.IsTrue(duration < 2d, String.Format("{0} entry changes took: {1} seconds", count, duration));
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Entries_Event_Prepared()
        {
            // Arrange.
            var preparedEvent = new ManualResetEvent(false);
            var preparedIdentifier = Identifier.Empty;
            _fabric.Entries.Prepared += (i) => { preparedIdentifier = i; preparedEvent.Set(); };
            
            // Act.
            var entry = await _fabric.Entries.Prepare();

            // Assert.
            Assert.IsNotNull(entry);
            Assert.AreNotEqual(Identifier.Empty, entry.Id);
            preparedEvent.WaitOne(TimeSpan.FromSeconds(10));
            Assert.AreNotEqual(Identifier.Empty, preparedIdentifier);
            Assert.AreEqual(entry.Id, preparedIdentifier);
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        public async Task FabricContext_Entries_Event_Stored()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var storedEvent = new ManualResetEvent(false);
            var storedIdentifier = Identifier.Empty;
            _fabric.Entries.Stored += (i) => { storedIdentifier = i; storedEvent.Set(); };

            // Act.
            var entry = await _fabric.Entries.Prepare();
            entry = (IEditableEntry)await _fabric.Entries.Change(entry, scope);
            storedEvent.WaitOne(TimeSpan.FromSeconds(10));

            // Assert.
            Assert.AreNotEqual(Identifier.Empty, storedIdentifier);
            Assert.AreEqual(entry.Id, storedIdentifier);
        }
    }
}
