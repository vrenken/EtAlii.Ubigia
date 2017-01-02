namespace EtAlii.Servus.Api.Logical.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class GraphComposer_IntegrationTests
    {

        [TestMethod]
        public async Task GraphComposer_Remove()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);
            var traverserFactory = new GraphPathTraverserFactory();
            var composer = new GraphComposerFactory(traverserFactory).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            string item = Guid.NewGuid().ToString();
            var addedEntry = await composer.Add(entry.Id, item, scope);
            var configuration = new GraphPathTraverserConfiguration()
                .Use(fabric)
                .Use(_diagnostics);
            var updatedEntry = (await traverserFactory.Create(configuration).Traverse(GraphPath.Create(entry.Id), Traversal.DepthFirst, scope)).Single();

            // Act.
            var removedEntry = await composer.Remove(updatedEntry.Id, item, scope);

            // Assert.
            Assert.IsNotNull(addedEntry);
            var addUpdatedEntry = (await fabric.Entries.GetRelated(entry.Id, EntryRelation.Update, scope)).SingleOrDefault();
            Assert.IsNotNull(addUpdatedEntry);
            Assert.AreEqual(hierarchy.Last(), addUpdatedEntry.Type); // TODO: We somehow should be able to make this value empty.
            var addLinkedEntry = (await fabric.Entries.GetRelated(addUpdatedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.IsNotNull(addLinkedEntry);
            Assert.AreEqual(EntryType.Add, addLinkedEntry.Type);
            var addFinalEntry = (await fabric.Entries.GetRelated(addLinkedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.IsNotNull(addFinalEntry);
            Assert.AreEqual(item, addFinalEntry.Type);
            Assert.AreEqual(addedEntry.Id, addFinalEntry.Id);

            var removeUpdatedEntry = (await fabric.Entries.GetRelated(updatedEntry.Id, EntryRelation.Update, scope)).SingleOrDefault();
            Assert.IsNotNull(removeUpdatedEntry);
            Assert.AreEqual(hierarchy.Last(), removeUpdatedEntry.Type); // TODO: We somehow should be able to make this value empty.
            var removeLinkedEntry = (await fabric.Entries.GetRelated(removeUpdatedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.IsNotNull(removeLinkedEntry);
            Assert.AreEqual(EntryType.Remove, removeLinkedEntry.Type);
            var removeFinalEntry = (await fabric.Entries.GetRelated(removeLinkedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.IsNotNull(removeFinalEntry);
            Assert.AreEqual(item, removeFinalEntry.Type);
            Assert.AreEqual(removedEntry.Id, removeFinalEntry.Id);
        }

        [TestMethod]
        public async Task GraphComposer_Remove_Non_Existing_Footer()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);
            var traverserFactory = new GraphPathTraverserFactory();
            var composer = new GraphComposerFactory(traverserFactory).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);
            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;
            string item = Guid.NewGuid().ToString();
            var addedEntry = await composer.Add(entry.Id, item, scope);
            var configuration = new GraphPathTraverserConfiguration()
                .Use(_diagnostics)
                .Use(fabric);
            var updatedEntry = (await traverserFactory.Create(configuration).Traverse(GraphPath.Create(entry.Id), Traversal.DepthFirst, scope)).Single();

            // Act.
            var act = composer.Remove(updatedEntry.Id, item + "Wrong", scope);

            // Assert.
            await ExceptionAssert.ThrowsAsync<GraphComposeException>(act);
        }

        [TestMethod]
        public async Task GraphComposer_Remove_Non_Existing_Header()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);
            var traverserFactory = new GraphPathTraverserFactory();
            var composer = new GraphComposerFactory(traverserFactory).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);
            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;
            string item = Guid.NewGuid().ToString();
            var addedEntry = await composer.Add(entry.Id, item, scope);
            var configuration = new GraphPathTraverserConfiguration()
                .Use(_diagnostics)
                .Use(fabric);
            var updatedEntry = (await traverserFactory.Create(configuration).Traverse(GraphPath.Create(entry.Id), Traversal.DepthFirst, scope)).Single();

            // Act.
            var act = composer.Remove(updatedEntry.Id, "Wrong" + item, scope);

            // Assert.
            await ExceptionAssert.ThrowsAsync<GraphComposeException>(act);
        }
    }
}