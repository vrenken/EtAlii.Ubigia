// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using Xunit;

    public partial class GraphComposerIntegrationTests
    {
        [Fact]
        public async Task GraphComposer_Remove()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope();
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .Use(fabric)
                .UseLogicalDiagnostics();
            var graphPathTraverserFactory = new GraphPathTraverserFactory();
            var traverser = graphPathTraverserFactory.Create(options);
            var composer = new GraphComposerFactory(traverser).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var item = Guid.NewGuid().ToString();
            var addedEntry = await composer.Add(entry.Id, item, scope).ConfigureAwait(false);

            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                graphPathTraverserFactory.Create(options).Traverse(GraphPath.Create(entry.Id), Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var updatedEntry = await results.SingleAsync();

            // Act.
            var removedEntry = await composer.Remove(updatedEntry.Id, item, scope).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(addedEntry);
            var addUpdatedEntry = await fabric.Entries.GetRelated(entry.Id, EntryRelations.Update, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(addUpdatedEntry);
            Assert.Equal(hierarchy.Last(), addUpdatedEntry.Type); // TODO: We somehow should be able to make this value empty.
            var addLinkedEntry = await fabric.Entries.GetRelated(addUpdatedEntry.Id, EntryRelations.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(addLinkedEntry);
            Assert.Equal(EntryType.Add, addLinkedEntry.Type);
            var addFinalEntry = await fabric.Entries.GetRelated(addLinkedEntry.Id, EntryRelations.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(addFinalEntry);
            Assert.Equal(item, addFinalEntry.Type);
            Assert.Equal(addedEntry.Id, addFinalEntry.Id);

            var removeUpdatedEntry = await fabric.Entries.GetRelated(updatedEntry.Id, EntryRelations.Update, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(removeUpdatedEntry);
            Assert.Equal(hierarchy.Last(), removeUpdatedEntry.Type); // TODO: We somehow should be able to make this value empty.
            var removeLinkedEntry = await fabric.Entries.GetRelated(removeUpdatedEntry.Id, EntryRelations.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(removeLinkedEntry);
            Assert.Equal(EntryType.Remove, removeLinkedEntry.Type);
            var removeFinalEntry = await fabric.Entries.GetRelated(removeLinkedEntry.Id, EntryRelations.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(removeFinalEntry);
            Assert.Equal(item, removeFinalEntry.Type);
            Assert.Equal(removedEntry.Id, removeFinalEntry.Id);
        }

        [Fact]
        public async Task GraphComposer_Remove_Non_Existing_Footer()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope();
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .UseLogicalDiagnostics()
                .Use(fabric);
            var graphPathTraverserFactory = new GraphPathTraverserFactory();
            var graphPathTraverser = graphPathTraverserFactory.Create(options);
            var composer = new GraphComposerFactory(graphPathTraverser).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);
            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var entry = hierarchyResult.Item1;
//            var hierarchy = hierarchyResult.Item2
            var item = Guid.NewGuid().ToString();
            await composer.Add(entry.Id, item, scope).ConfigureAwait(false);

            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                graphPathTraverserFactory.Create(options).Traverse(GraphPath.Create(entry.Id), Traversal.DepthFirst, scope,output);
                return Disposable.Empty;
            }).ToHotObservable();
            var updatedEntry = await results.SingleAsync();

            // Act.
            var act = new Func<Task>(async () => await composer.Remove(updatedEntry.Id, item + "Wrong", scope).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<GraphComposeException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task GraphComposer_Remove_Non_Existing_Header()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope();
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .Use(fabric)
                .UseLogicalDiagnostics();
            var graphPathTraverserFactory = new GraphPathTraverserFactory();
            var graphPathTraverser = graphPathTraverserFactory.Create(options);
            var composer = new GraphComposerFactory(graphPathTraverser).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);
            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var entry = hierarchyResult.Item1;
//            var hierarchy = hierarchyResult.Item2
            var item = Guid.NewGuid().ToString();
            await composer.Add(entry.Id, item, scope).ConfigureAwait(false);

            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                graphPathTraverserFactory.Create(options).Traverse(GraphPath.Create(entry.Id), Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var updatedEntry = await results.SingleAsync();

            // Act.
            var act = new Func<Task>(async () => await composer.Remove(updatedEntry.Id, "Wrong" + item, scope).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<GraphComposeException>(act).ConfigureAwait(false);
        }
    }
}
