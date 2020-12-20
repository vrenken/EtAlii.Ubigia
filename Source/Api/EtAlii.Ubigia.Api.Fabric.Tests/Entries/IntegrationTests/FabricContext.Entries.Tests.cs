﻿namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class FabricContextEntriesTests : IClassFixture<FabricUnitTestContext>, IAsyncLifetime
    {
        private readonly FabricUnitTestContext _testContext;
        private IFabricContext _fabric;

        public FabricContextEntriesTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var connection = await _testContext.TransportTestContext.CreateDataConnectionToNewSpace().ConfigureAwait(false);
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(connection)
                .UseFabricDiagnostics(DiagnosticsConfiguration.Default);
            _fabric = new FabricContextFactory().Create(fabricContextConfiguration);
        }

        public Task DisposeAsync()
        {
            _fabric.Dispose();
            _fabric = null;
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Entries_Get_Root()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var root = await _fabric.Roots.Get("Hierarchy").ConfigureAwait(false);
            Assert.NotNull(root);
            var id = root.Identifier;
            Assert.NotEqual(Identifier.Empty, id);

            // Act.
            var entry = await _fabric.Entries.Get(id, scope).ConfigureAwait(false);

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
                var root = await _fabric.Roots.Get(rootName).ConfigureAwait(false);
                Assert.NotNull(root);
                var id = root.Identifier;
                Assert.NotEqual(Identifier.Empty, id);

                // Act.
                var entry = await _fabric.Entries.Get(id, scope).ConfigureAwait(false);

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
            var entry = await _fabric.Entries.Prepare().ConfigureAwait(false);

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
            for (var i = 0; i < count; i++)
            {
                entries[i] = await _fabric.Entries.Prepare().ConfigureAwait(false);
            }
            var endTicks = Environment.TickCount;

            // Assert.
            for (var i = 0; i < count; i++)
            {
                Assert.NotNull(entries[i]);
                Assert.NotEqual(Identifier.Empty, entries[i].Id);
            }
            var duration = TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds;
            Assert.True(duration < 2d, $"{count} entry preparations took: {duration} seconds");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Entries_Change()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var entry = await _fabric.Entries.Prepare().ConfigureAwait(false);

            // Act.
            entry = (IEditableEntry)await _fabric.Entries.Change(entry, scope).ConfigureAwait(false);

            // Assert.
            var retrievedEntry = await _fabric.Entries.Get(entry.Id, scope).ConfigureAwait(false);
            Assert.NotNull(retrievedEntry);
            Assert.NotEqual(Identifier.Empty, retrievedEntry.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Entries_Change_Add_Tag()
        {
            // Arrange.
            var tag = Guid.NewGuid().ToString();
            var scope = new ExecutionScope(false);
            var entry = await _fabric.Entries.Prepare().ConfigureAwait(false);
            entry.Tag = tag;

            // Act.
            entry = (IEditableEntry)await _fabric.Entries.Change(entry, scope).ConfigureAwait(false);

            // Assert.
            var retrievedEntry = await _fabric.Entries.Get(entry.Id, scope).ConfigureAwait(false);
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
            for (var i = 0; i < count; i++)
            {
                entries[i] = await _fabric.Entries.Prepare().ConfigureAwait(false);
            }

            // Act.
            var startTicks = Environment.TickCount;
            for (var i = 0; i < count; i++)
            {
                entries[i] = (IEditableEntry)await _fabric.Entries.Change(entries[i], scope).ConfigureAwait(false);
            }
            var endTicks = Environment.TickCount;

            // Assert.
            for (var i = 0; i < count; i++)
            {
                var retrievedEntry = await _fabric.Entries.Get(entries[i].Id, scope).ConfigureAwait(false);
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
            IEditableEntry entry = null;

            // Act.
            var action = await ActionAssert
                .RaisesAsync<Identifier>(
                    m => _fabric.Entries.Prepared += m,
                    m => _fabric.Entries.Prepared -= m,
                    async () => entry = await _fabric.Entries.Prepare().ConfigureAwait(false))
                .ConfigureAwait(false);

            // Assert.
            var preparedIdentifier = action.Argument;
            Assert.NotNull(entry);
            Assert.NotEqual(Identifier.Empty, entry.Id);
            Assert.NotEqual(Identifier.Empty, preparedIdentifier);
            Assert.Equal(entry.Id, preparedIdentifier);
        }

        [Fact(Skip = "Unknown reason"), Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Entries_Event_Stored()
        {
            // Arrange.
            var scope = new ExecutionScope(false);
            var entry = await _fabric.Entries.Prepare().ConfigureAwait(false);

            // Act.
            var action = await ActionAssert
                .RaisesAsync<Identifier>(
                    m => _fabric.Entries.Stored += m,
                    m => _fabric.Entries.Stored -= m,
                    async () => entry = (IEditableEntry)await _fabric.Entries.Change(entry, scope).ConfigureAwait(false))
                .ConfigureAwait(false);


            // Assert.
            var storedIdentifier = action.Argument;
            Assert.NotEqual(Identifier.Empty, storedIdentifier);
            Assert.Equal(entry.Id, storedIdentifier);
        }
    }
}
