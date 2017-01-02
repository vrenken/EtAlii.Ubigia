namespace EtAlii.Servus.Api.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    [TestClass]
    public class DataConnection_Entries_Tests : EtAlii.Servus.Api.Tests.TestBase
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
        public void DataConnection_Entries_Get_Root()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var root = connection.Roots.Get(DefaultRoot.Hierarchy);
            Assert.IsNotNull(root);
            var id = root.Identifier;
            Assert.IsNotNull(id);
            Assert.AreNotEqual(Identifier.Empty, id);

            // Act.
            var entry = connection.Entries.Get(id);

            // Assert.
            Assert.IsNotNull(entry);
            Assert.AreEqual(root.Identifier, entry.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Entries_Get_All_Root()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            foreach (var rootName in DefaultRoot.All)
            {
                var root = connection.Roots.Get(rootName);
                Assert.IsNotNull(root);
                var id = root.Identifier;
                Assert.IsNotNull(id);
                Assert.AreNotEqual(Identifier.Empty, id);

                // Act.
                var entry = connection.Entries.Get(id);

                // Assert.
                Assert.IsNotNull(entry);
                Assert.AreEqual(root.Identifier, entry.Id);
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Entries_Prepare()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            // Act.
            var entry = connection.Entries.Prepare();

            // Assert.
            Assert.IsNotNull(entry);
            Assert.AreNotEqual(Identifier.Empty, entry.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Entries_Prepare_Multiple()
        {
            // Arrange.
            var count = 50;
            var entries = new IEditableEntry[count];
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            // Act.
            var startTicks = Environment.TickCount;
            for (int i = 0; i < count; i++)
            {
                entries[i] = connection.Entries.Prepare();
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
        public void DataConnection_Entries_Change()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            var entry = connection.Entries.Prepare();

            // Act.
            connection.Entries.Change(entry);

            // Assert.
            var retrievedEntry = connection.Entries.Get(entry.Id);
            Assert.IsNotNull(retrievedEntry);
            Assert.AreNotEqual(Identifier.Empty, retrievedEntry.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Entries_Change_Multiple()
        {
            // Arrange.
            var count = 50;
            var entries = new IEditableEntry[count];
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            for (int i = 0; i < count; i++)
            {
                entries[i] = connection.Entries.Prepare();
            }

            // Act.
            var startTicks = Environment.TickCount;
            for (int i = 0; i < count; i++)
            {
                connection.Entries.Change(entries[i]);
            }
            var endTicks = Environment.TickCount;

            // Assert.
            for (int i = 0; i < count; i++)
            {
                var retrievedEntry = connection.Entries.Get(entries[i].Id);
                Assert.IsNotNull(retrievedEntry);
                Assert.AreNotEqual(Identifier.Empty, retrievedEntry.Id);
            }
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.IsTrue(duration < 2d, String.Format("{0} entry changes took: {1} seconds", count, duration));
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Entries_Event_Prepared()
        {
            var preparedEvent = new ManualResetEvent(false);
            var preparedIdentifier = Identifier.Empty;

            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            connection.Entries.Prepared += (i) => { preparedIdentifier = i; preparedEvent.Set(); };
            var entry = connection.Entries.Prepare();

            Assert.IsNotNull(entry);
            Assert.AreNotEqual(Identifier.Empty, entry.Id);

            preparedEvent.WaitOne(TimeSpan.FromSeconds(10));

            Assert.AreNotEqual(Identifier.Empty, preparedIdentifier);
            Assert.AreEqual(entry.Id, preparedIdentifier);
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Entries_Event_Stored()
        {
            var storedEvent = new ManualResetEvent(false);
            var storedIdentifier = Identifier.Empty;

            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            connection.Entries.Stored += (i) => { storedIdentifier = i; storedEvent.Set(); };
            var entry = connection.Entries.Prepare();

            connection.Entries.Change(entry);
            storedEvent.WaitOne(TimeSpan.FromSeconds(10));

            Assert.AreNotEqual(Identifier.Empty, storedIdentifier);
            Assert.AreEqual(entry.Id, storedIdentifier);
        }
    }
}
