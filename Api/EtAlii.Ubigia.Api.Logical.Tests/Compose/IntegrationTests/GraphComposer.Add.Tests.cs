namespace EtAlii.Ubigia.Api.Logical.Tests
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
            using var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var graphPathTraverserConfiguration = new GraphPathTraverserConfiguration().Use(fabric);
            var graphPathTraverserFactory = new GraphPathTraverserFactory();
            var graphPathTraverser = graphPathTraverserFactory.Create(graphPathTraverserConfiguration);
            var composer = new GraphComposerFactory(graphPathTraverser).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communication");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
//            var hierarchy = hierarchyResult.Item2

            string itemToAdd = Guid.NewGuid().ToString();

            // Act.
            var addedEntry = await composer.Add(entry.Id, itemToAdd, scope);

            // Assert.
            Assert.NotNull(addedEntry);
            var updatedEntry = (await fabric.Entries.GetRelated(entry.Id, EntryRelation.Update, scope)).SingleOrDefault();
            Assert.NotNull(updatedEntry);
            Assert.False(string.IsNullOrEmpty(updatedEntry.Type)); // TODO: We somehow should be able to make this value empty.
            var linkedEntry = (await fabric.Entries.GetRelated(updatedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.NotNull(linkedEntry);
            Assert.Equal(EntryType.Add, linkedEntry.Type);
            var finalEntry = (await fabric.Entries.GetRelated(linkedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
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
            using var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var graphPathTraverserConfiguration = new GraphPathTraverserConfiguration().Use(fabric);
            var graphPathTraverserFactory = new GraphPathTraverserFactory();
            var graphPathTraverser = graphPathTraverserFactory.Create(graphPathTraverserConfiguration);
            var composer = new GraphComposerFactory(graphPathTraverser).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communication");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var personRoot = await fabric.Roots.Get("Person");
            var personEntry = (IEditableEntry)await fabric.Entries.Get(personRoot, scope);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var firstEntry = hierarchyResult.Item1;
//            var communicationsHierarchy = hierarchyResult.Item2

            hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, personEntry, depth);
            var secondEntry = hierarchyResult.Item1;
//            var personHierarchy = hierarchyResult.Item2

            // Act.
            var addedEntry = await composer.Add(firstEntry.Id, secondEntry.Id, scope);

            // Assert.
            Assert.NotNull(addedEntry);
            var updatedFirstEntry = (await fabric.Entries.GetRelated(firstEntry.Id, EntryRelation.Update, scope)).SingleOrDefault();
            Assert.NotNull(updatedFirstEntry);
            Assert.False(string.IsNullOrEmpty(updatedFirstEntry.Type)); // TODO: We somehow should be able to make this value empty.
            var linkedEntry = (await fabric.Entries.GetRelated(updatedFirstEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.NotNull(linkedEntry);
            Assert.Equal(EntryType.Add, linkedEntry.Type);
            var finalEntry = (await fabric.Entries.GetRelated(linkedEntry.Id, EntryRelation.Child, scope)).SingleOrDefault();
            Assert.NotNull(finalEntry);
            Assert.Equal(addedEntry.Id, finalEntry.Id);
            var updatedSecondEntry = (await fabric.Entries.GetRelated(secondEntry.Id, EntryRelation.Update, scope)).SingleOrDefault();
            Assert.NotNull(updatedSecondEntry);
        }
    }
}