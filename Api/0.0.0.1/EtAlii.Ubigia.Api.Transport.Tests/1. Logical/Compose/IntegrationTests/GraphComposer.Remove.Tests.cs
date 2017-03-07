namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public partial class GraphComposerIntegrationTests
    {

        [Fact]
        public async Task GraphComposer_Remove()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var traverserFactory = new GraphPathTraverserFactory();
            var composer = new GraphComposerFactory(traverserFactory).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            string item = Guid.NewGuid().ToString();
            var addedEntry = await composer.Add(entry.Id, item, scope);
            var configuration = new GraphPathTraverserConfiguration()
                .Use(fabric)
                .Use(_testContext.DiagnosticsConfiguration);

            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverserFactory.Create(configuration).Traverse(GraphPath.Create(entry.Id), Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var updatedEntry = await results.SingleAsync();

            // Act.
            var removedEntry = await composer.Remove(updatedEntry.Id, item, scope);

            // Assert.
            Assert.NotNull(addedEntry);
            var addUpdatedEntry = (await fabric.Entries.GetRelated(entry.Id, EntryRelation.Update, scope)).SingleOrDefault();
            Assert.NotNull(addUpdatedEntry);
            Assert.Equal(hierarchy.Last(), addUpdatedEntry.Type); // TODO: We somehow should be able to make this value empty.
            var addLinkedEntry = (await fabric.Entries.GetRelated(addUpdatedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.NotNull(addLinkedEntry);
            Assert.Equal(EntryType.Add, addLinkedEntry.Type);
            var addFinalEntry = (await fabric.Entries.GetRelated(addLinkedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.NotNull(addFinalEntry);
            Assert.Equal(item, addFinalEntry.Type);
            Assert.Equal(addedEntry.Id, addFinalEntry.Id);

            var removeUpdatedEntry = (await fabric.Entries.GetRelated(updatedEntry.Id, EntryRelation.Update, scope)).SingleOrDefault();
            Assert.NotNull(removeUpdatedEntry);
            Assert.Equal(hierarchy.Last(), removeUpdatedEntry.Type); // TODO: We somehow should be able to make this value empty.
            var removeLinkedEntry = (await fabric.Entries.GetRelated(removeUpdatedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.NotNull(removeLinkedEntry);
            Assert.Equal(EntryType.Remove, removeLinkedEntry.Type);
            var removeFinalEntry = (await fabric.Entries.GetRelated(removeLinkedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.NotNull(removeFinalEntry);
            Assert.Equal(item, removeFinalEntry.Type);
            Assert.Equal(removedEntry.Id, removeFinalEntry.Id);
        }

        [Fact]
        public async Task GraphComposer_Remove_Non_Existing_Footer()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var traverserFactory = new GraphPathTraverserFactory();
            var composer = new GraphComposerFactory(traverserFactory).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);
            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;
            string item = Guid.NewGuid().ToString();
            var addedEntry = await composer.Add(entry.Id, item, scope);
            var configuration = new GraphPathTraverserConfiguration()
                .Use(_testContext.DiagnosticsConfiguration)
                .Use(fabric);

            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverserFactory.Create(configuration).Traverse(GraphPath.Create(entry.Id), Traversal.DepthFirst, scope,output);
                return Disposable.Empty;
            }).ToHotObservable();
            var updatedEntry = await results.SingleAsync();

            // Act.
            var act = composer.Remove(updatedEntry.Id, item + "Wrong", scope);

            // Assert.
            await ExceptionAssert.ThrowsAsync<GraphComposeException>(act);
        }

        [Fact]
        public async Task GraphComposer_Remove_Non_Existing_Header()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var traverserFactory = new GraphPathTraverserFactory();
            var composer = new GraphComposerFactory(traverserFactory).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);
            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;
            string item = Guid.NewGuid().ToString();
            var addedEntry = await composer.Add(entry.Id, item, scope);
            var configuration = new GraphPathTraverserConfiguration()
                .Use(_testContext.DiagnosticsConfiguration)
                .Use(fabric);

            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverserFactory.Create(configuration).Traverse(GraphPath.Create(entry.Id), Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var updatedEntry = await results.SingleAsync();

            // Act.
            var act = composer.Remove(updatedEntry.Id, "Wrong" + item, scope);

            // Assert.
            await ExceptionAssert.ThrowsAsync<GraphComposeException>(act);
        }
    }
}