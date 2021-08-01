// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public partial class GraphComposerIntegrationTests
    {
        [Fact(Skip = "Not working (yet)")]
        public async Task GraphComposer_Link()
        {
            // Arrange.
            const int depth = 3;
            var scope = new ExecutionScope(false);
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);

            var graphPathTraverserConfiguration = new GraphPathTraverserConfiguration().Use(fabric);
            var graphPathTraverserFactory = new GraphPathTraverserFactory();
            var graphPathTraverser = graphPathTraverserFactory.Create(graphPathTraverserConfiguration);
            var composer = new GraphComposerFactory(graphPathTraverser).Create(fabric);

            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var personRoot = await fabric.Roots.Get("Person").ConfigureAwait(false);
            var personEntry = (IEditableEntry)await fabric.Entries.Get(personRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var firstEntry = hierarchyResult.Item1;
//            var communicationsHierarchy = hierarchyResult.Item2

            hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, personEntry, depth).ConfigureAwait(false);
            var secondEntry = hierarchyResult.Item1;
//            var personHierarchy = hierarchyResult.Item2

            var linkItem = Guid.NewGuid().ToString();

            // Act.
            var addedEntry = await composer.Link(firstEntry.Id, linkItem, secondEntry.Id, scope).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(addedEntry);
            var updatedEntry = await fabric.Entries.GetRelated(firstEntry.Id, EntryRelations.Update, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(updatedEntry);
            Assert.False(string.IsNullOrEmpty(updatedEntry.Type)); // TODO: We somehow should be able to make this value empty.
            var linkedEntry = await fabric.Entries.GetRelated(updatedEntry.Id, EntryRelations.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(linkedEntry);
            Assert.Equal(EntryType.Add, linkedEntry.Type);
            var finalEntry = await fabric.Entries.GetRelated(linkedEntry.Id, EntryRelations.Child, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            Assert.NotNull(finalEntry);
            Assert.Equal(linkItem, finalEntry.Type);
            Assert.Equal(addedEntry.Id, finalEntry.Id);
        }

    }
}
