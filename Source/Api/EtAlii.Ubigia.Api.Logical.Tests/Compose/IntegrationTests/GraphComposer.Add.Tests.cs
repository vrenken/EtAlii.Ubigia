// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using Xunit;

    public partial class GraphComposerIntegrationTests
    {
        [Fact]
        public async Task GraphComposer_Add()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope();
            var fabricOptions = await _testContext.Fabric
                .CreateFabricOptions(true)
                .ConfigureAwait(false);

            var logicalOptions = fabricOptions
                .UseLogicalContext()
                .UseDiagnostics();
            var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);
            var fabricContext = logicalOptions.FabricContext;
            var composer = new GraphComposerFactory(traverser).Create(fabricContext);

            var communicationsRoot = await fabricContext.Roots
                .Get("Communication")
                .ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabricContext.Entries
                .Get(communicationsRoot, scope)
                .ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric
                .CreateHierarchy(fabricContext, communicationsEntry, depth)
                .ConfigureAwait(false);
            var entry = hierarchyResult.Item1;
//            var hierarchy = hierarchyResult.Item2

            var itemToAdd = Guid.NewGuid().ToString();

            // Act.
            var addedEntry = await composer.Add(entry.Id, itemToAdd, scope).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(addedEntry);
            var updatedEntry = await fabricContext.Entries.GetRelated(entry.Id, EntryRelations.Update, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(updatedEntry);
            Assert.False(string.IsNullOrEmpty(updatedEntry.Type)); // TODO: We somehow should be able to make this value empty.
            var linkedEntry = await fabricContext.Entries.GetRelated(updatedEntry.Id, EntryRelations.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(linkedEntry);
            Assert.Equal(EntryType.Add, linkedEntry.Type);
            var finalEntry = await fabricContext.Entries.GetRelated(linkedEntry.Id, EntryRelations.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(finalEntry);
            Assert.Equal(itemToAdd, finalEntry.Type);
            Assert.Equal(addedEntry.Id, finalEntry.Id);
        }

        [Fact]
        public async Task GraphComposer_Add_Existing()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope();
            var fabricOptions = await _testContext.Fabric
                .CreateFabricOptions(true)
                .ConfigureAwait(false);
            var logicalOptions = fabricOptions
                .UseLogicalContext()
                .UseDiagnostics();
            var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);
            var fabricContext = logicalOptions.FabricContext;

            var composer = new GraphComposerFactory(traverser).Create(fabricContext);

            var communicationsRoot = await fabricContext.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabricContext.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var personRoot = await fabricContext.Roots.Get("Person").ConfigureAwait(false);
            var personEntry = (IEditableEntry)await fabricContext.Entries.Get(personRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabricContext, communicationsEntry, depth).ConfigureAwait(false);
            var firstEntry = hierarchyResult.Item1;
//            var communicationsHierarchy = hierarchyResult.Item2

            hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabricContext, personEntry, depth).ConfigureAwait(false);
            var secondEntry = hierarchyResult.Item1;
//            var personHierarchy = hierarchyResult.Item2

            // Act.
            var addedEntry = await composer.Add(firstEntry.Id, secondEntry.Id, scope).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(addedEntry);
            var updatedFirstEntry = await fabricContext.Entries.GetRelated(firstEntry.Id, EntryRelations.Update, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(updatedFirstEntry);
            Assert.False(string.IsNullOrEmpty(updatedFirstEntry.Type)); // TODO: We somehow should be able to make this value empty.
            var linkedEntry = await fabricContext.Entries.GetRelated(updatedFirstEntry.Id, EntryRelations.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(linkedEntry);
            Assert.Equal(EntryType.Add, linkedEntry.Type);
            var finalEntry = await fabricContext.Entries.GetRelated(linkedEntry.Id, EntryRelations.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(finalEntry);
            Assert.Equal(addedEntry.Id, finalEntry.Id);
            var updatedSecondEntry = await fabricContext.Entries.GetRelated(secondEntry.Id, EntryRelations.Update, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(updatedSecondEntry);
        }
    }
}
