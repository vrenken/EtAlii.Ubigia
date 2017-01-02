namespace EtAlii.Servus.Api.Logical.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class GraphComposer_IntegrationTests
    {
        [TestMethod]
        public async Task GraphComposer_Add()
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

            string itemToAdd = Guid.NewGuid().ToString();

            // Act.
            var addedEntry = await composer.Add(entry.Id, itemToAdd, scope);

            // Assert.
            Assert.IsNotNull(addedEntry);
            var updatedEntry = (await fabric.Entries.GetRelated(entry.Id, EntryRelation.Update, scope)).SingleOrDefault();
            Assert.IsNotNull(updatedEntry);
            Assert.IsFalse(String.IsNullOrEmpty(updatedEntry.Type)); // TODO: We somehow should be able to make this value empty.
            var linkedEntry = (await fabric.Entries.GetRelated(updatedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.IsNotNull(linkedEntry);
            Assert.AreEqual(EntryType.Add, linkedEntry.Type);
            var finalEntry = (await fabric.Entries.GetRelated(linkedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.IsNotNull(finalEntry);
            Assert.AreEqual(itemToAdd, finalEntry.Type);
            Assert.AreEqual(addedEntry.Id, finalEntry.Id);
        }

        [TestMethod]
        public async Task GraphComposer_Add_Existing()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);
            var traverserFactory = new GraphPathTraverserFactory();
            var composer = new GraphComposerFactory(traverserFactory).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var contactsRoot = await fabric.Roots.Get("Contacts");
            var contactsEntry = (IEditableEntry)await fabric.Entries.Get(contactsRoot, scope);

            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var firstEntry = hierarchyResult.Item1;
            var communicationsHierarchy = hierarchyResult.Item2;

            hierarchyResult = await _testContext.CreateHierarchy(fabric, contactsEntry, depth);
            var secondEntry = hierarchyResult.Item1;
            var contactsHierarchy = hierarchyResult.Item2;

            // Act.
            var addedEntry = await composer.Add(firstEntry.Id, secondEntry.Id, scope);

            // Assert.
            Assert.IsNotNull(addedEntry);
            var updatedFirstEntry = (await fabric.Entries.GetRelated(firstEntry.Id, EntryRelation.Update, scope)).SingleOrDefault();
            Assert.IsNotNull(updatedFirstEntry);
            Assert.IsFalse(String.IsNullOrEmpty(updatedFirstEntry.Type)); // TODO: We somehow should be able to make this value empty.
            var linkedEntry = (await fabric.Entries.GetRelated(updatedFirstEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.IsNotNull(linkedEntry);
            Assert.AreEqual(EntryType.Add, linkedEntry.Type);
            var finalEntry = (await fabric.Entries.GetRelated(linkedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.IsNotNull(finalEntry);
            Assert.AreEqual(addedEntry.Id, finalEntry.Id);
            var updatedSecondEntry = (await fabric.Entries.GetRelated(secondEntry.Id, EntryRelation.Update, scope)).SingleOrDefault();
            Assert.IsNotNull(updatedSecondEntry);
        }
    }
}