namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using Xunit;
    
    public class FabricContextEntriesTests : IClassFixture<TransportUnitTestContext>, IDisposable
    {
        private IFabricContext _fabric;

        public FabricContextEntriesTests(TransportUnitTestContext testContext)
        {
            var task = Task.Run(async () =>
            {
                var connection = await testContext.TransportTestContext.CreateDataConnection();
                var fabricContextConfiguration = new FabricContextConfiguration()
                    .Use(connection);
                _fabric = new FabricContextFactory().Create(fabricContextConfiguration);
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(() =>
            {
                _fabric.Dispose();
                _fabric = null;
            });
            task.Wait();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Entries_Get_Root()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy");
            Assert.NotNull(root);
            var id = root.Identifier;
            Assert.NotEqual(Identifier.Empty, id);

            // Act.
            var entry = await _fabric.Entries.Get(id, scope);

            // Assert.
            Assert.NotNull(entry);
            Assert.Equal(root.Identifier, entry.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Entries_Get_All_Root()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            foreach (var rootName in SpaceTemplate.Data.RootsToCreate)
            {
                var root = await _fabric.Roots.Get(rootName);
                Assert.NotNull(root);
                var id = root.Identifier;
                Assert.NotEqual(Identifier.Empty, id);

                // Act.
                var entry = await _fabric.Entries.Get(id, scope);

                // Assert.
                Assert.NotNull(entry);
                Assert.Equal(root.Identifier, entry.Id);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Entries_Prepare()
        {
            // Arrange.

            // Act.
            var entry = await _fabric.Entries.Prepare();

            // Assert.
            Assert.NotNull(entry);
            Assert.NotEqual(Identifier.Empty, entry.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
                Assert.NotNull(entries[i]);
                Assert.NotEqual(Identifier.Empty, entries[i].Id);
            }
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.True(duration < 2d, $"{count} entry perparations took: {duration} seconds");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Entries_Change()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var entry = await _fabric.Entries.Prepare();

            // Act.
            entry = (IEditableEntry)await _fabric.Entries.Change(entry, scope);

            // Assert.
            var retrievedEntry = await _fabric.Entries.Get(entry.Id, scope);
            Assert.NotNull(retrievedEntry);
            Assert.NotEqual(Identifier.Empty, retrievedEntry.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Entries_Change_Add_Tag()
        {
            // Arrange.
            var tag = Guid.NewGuid().ToString();
            var scope = new ExecutionScope(false);
            var entry = await _fabric.Entries.Prepare();
            entry.Tag = tag;

            // Act.
            entry = (IEditableEntry)await _fabric.Entries.Change(entry, scope);

            // Assert.
            var retrievedEntry = await _fabric.Entries.Get(entry.Id, scope);
            Assert.NotNull(retrievedEntry);
            Assert.NotEqual(Identifier.Empty, retrievedEntry.Id);
            Assert.Equal(tag, retrievedEntry.Tag);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
                Assert.NotNull(retrievedEntry);
                Assert.NotEqual(Identifier.Empty, retrievedEntry.Id);
            }
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.True(duration < 2d, $"{count} entry changes took: {duration} seconds");
        }

        [Fact(Skip = "Unknown reason"), Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Entries_Event_Prepared()
        {
            // Arrange.
            var preparedEvent = new ManualResetEvent(false);
            var preparedIdentifier = Identifier.Empty;
            _fabric.Entries.Prepared += (i) => { preparedIdentifier = i; preparedEvent.Set(); };
            
            // Act.
            var entry = await _fabric.Entries.Prepare();

            // Assert.
            Assert.NotNull(entry);
            Assert.NotEqual(Identifier.Empty, entry.Id);
            preparedEvent.WaitOne(TimeSpan.FromSeconds(10));
            Assert.NotEqual(Identifier.Empty, preparedIdentifier);
            Assert.Equal(entry.Id, preparedIdentifier);
        }

        [Fact(Skip = "Unknown reason"), Trait("Category", TestAssembly.Category)]
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
            Assert.NotEqual(Identifier.Empty, storedIdentifier);
            Assert.Equal(entry.Id, storedIdentifier);
        }
    }
}
