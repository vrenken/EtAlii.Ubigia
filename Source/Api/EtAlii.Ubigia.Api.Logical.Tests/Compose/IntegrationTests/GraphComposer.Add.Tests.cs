﻿namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public partial class GraphComposerIntegrationTests
    {
        [Fact]
        public async Task GraphComposer_Add()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope(false);
            using var fabric = await _testContext.FabricTestContext.CreateFabricContext(true).ConfigureAwait(false);
            var graphPathTraverserConfiguration = new GraphPathTraverserConfiguration().Use(fabric);
            var graphPathTraverserFactory = new GraphPathTraverserFactory();
            var graphPathTraverser = graphPathTraverserFactory.Create(graphPathTraverserConfiguration);
            var composer = new GraphComposerFactory(graphPathTraverser).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var entry = hierarchyResult.Item1;
//            var hierarchy = hierarchyResult.Item2

            var itemToAdd = Guid.NewGuid().ToString();

            // Act.
            var addedEntry = await composer.Add(entry.Id, itemToAdd, scope).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(addedEntry);
            var updatedEntry = await fabric.Entries.GetRelated(entry.Id, EntryRelation.Update, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(updatedEntry);
            Assert.False(string.IsNullOrEmpty(updatedEntry.Type)); // TODO: We somehow should be able to make this value empty.
            var linkedEntry = await fabric.Entries.GetRelated(updatedEntry.Id, EntryRelation.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(linkedEntry);
            Assert.Equal(EntryType.Add, linkedEntry.Type);
            var finalEntry = await fabric.Entries.GetRelated(linkedEntry.Id, EntryRelation.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(finalEntry);
            Assert.Equal(itemToAdd, finalEntry.Type);
            Assert.Equal(addedEntry.Id, finalEntry.Id);
        }

        [Fact]
        public async Task GraphComposer_Add_Existing()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope(false);
            using var fabric = await _testContext.FabricTestContext.CreateFabricContext(true).ConfigureAwait(false);
            var graphPathTraverserConfiguration = new GraphPathTraverserConfiguration().Use(fabric);
            var graphPathTraverserFactory = new GraphPathTraverserFactory();
            var graphPathTraverser = graphPathTraverserFactory.Create(graphPathTraverserConfiguration);
            var composer = new GraphComposerFactory(graphPathTraverser).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var personRoot = await fabric.Roots.Get("Person").ConfigureAwait(false);
            var personEntry = (IEditableEntry)await fabric.Entries.Get(personRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var firstEntry = hierarchyResult.Item1;
//            var communicationsHierarchy = hierarchyResult.Item2

            hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, personEntry, depth).ConfigureAwait(false);
            var secondEntry = hierarchyResult.Item1;
//            var personHierarchy = hierarchyResult.Item2

            // Act.
            var addedEntry = await composer.Add(firstEntry.Id, secondEntry.Id, scope).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(addedEntry);
            var updatedFirstEntry = await fabric.Entries.GetRelated(firstEntry.Id, EntryRelation.Update, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(updatedFirstEntry);
            Assert.False(string.IsNullOrEmpty(updatedFirstEntry.Type)); // TODO: We somehow should be able to make this value empty.
            var linkedEntry = await fabric.Entries.GetRelated(updatedFirstEntry.Id, EntryRelation.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(linkedEntry);
            Assert.Equal(EntryType.Add, linkedEntry.Type);
            var finalEntry = await fabric.Entries.GetRelated(linkedEntry.Id, EntryRelation.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(finalEntry);
            Assert.Equal(addedEntry.Id, finalEntry.Id);
            var updatedSecondEntry = await fabric.Entries.GetRelated(secondEntry.Id, EntryRelation.Update, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(updatedSecondEntry);
        }
    }
}