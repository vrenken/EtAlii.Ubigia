namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore.Tests
{
    using EtAlii.Ubigia.Api;
    using Xunit;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;


    [Trait("Technology", "AspNetCore")]
    public class EntryRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;
        private const int Count = 10;

        public EntryRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task EntryRepository_Prepare()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);

            // Act.
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);

            var containerId = context.Host.Storage.ContainerProvider.FromIdentifier(entry.Id);
            var folder = context.Host.Storage.PathBuilder.GetFolder(containerId);
            Assert.True(context.Host.Storage.FolderManager.Exists(folder));
        }

        [Fact]
        public async Task EntryRepository_Prepare_Timed_01()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var start = Environment.TickCount;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);

            // Act.
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);

            // Assert.
            Assert.NotNull(entry);
            Assert.True(1500 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact]
        public async Task EntryRepository_Prepare_Timed_02()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var start = Environment.TickCount;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);

            // Act.
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);

            // Assert.
            Assert.NotNull(entry);
            Assert.True(1500 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact]
        public async Task EntryRepository_Prepare_Timed_03()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var start = Environment.TickCount;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);

            // Act.
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);

            // Assert.
            Assert.NotNull(entry);
            Assert.True(1500 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact]
        public async Task EntryRepository_Store_Only_Previous()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);

            // Act.
            entry = context.Host.Infrastructure.Entries.Store(entry);

            var containerId = context.Host.Storage.ContainerProvider.FromIdentifier(entry.Id);
            var folder = context.Host.Storage.PathBuilder.GetFolder(containerId);
            Assert.True(context.Host.Storage.FolderManager.Exists(folder));
            var fileName = String.Format(context.Host.Storage.StorageSerializer.FileNameFormat, "Identifier");
            var file = Path.Combine(folder, fileName);
            Assert.True(context.Host.Storage.FileManager.Exists(file));
        }

        [Fact]
        public async Task EntryRepository_Store_Previous_And_Next()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry1 = (IEditableEntry)context.Host.Infrastructure.Entries.Prepare(space.Id);
            var entry2 = (IEditableEntry)context.Host.Infrastructure.Entries.Prepare(space.Id);
            entry2 = context.Host.Infrastructure.Entries.Store(entry2);

            // Act.
            entry2.Previous = Relation.NewRelation(entry1.Id);
            entry2 = context.Host.Infrastructure.Entries.Store(entry2);

            // Arrange.
            var containerId = context.Host.Storage.ContainerProvider.FromIdentifier(entry1.Id);
            var folder = context.Host.Storage.PathBuilder.GetFolder(containerId);
            Assert.True(context.Host.Storage.FolderManager.Exists(folder));
            var fileName = String.Format(context.Host.Storage.StorageSerializer.FileNameFormat, "Identifier");
            var file = Path.Combine(folder, fileName);
            Assert.True(context.Host.Storage.FileManager.Exists(file));
            fileName = String.Format(context.Host.Storage.StorageSerializer.FileNameFormat, "Next");
            file = Path.Combine(folder, fileName);
            Assert.True(context.Host.Storage.FileManager.Exists(file));
        }

        [Fact]
        public async Task EntryRepository_Store_Sequence_Same_Identifiers()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateSequence(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            for (int i = 0; i < Count; i++)
            {
                // Act.
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.NotEqual(Identifier.Empty, createdEntry.Id);
                Assert.Equal(loadedEntry.Id, createdEntry.Id);
            }
        }

        [Fact]
        public async Task EntryRepository_Store_Sequence_Check_Next_Identifiers_Based_On_Created()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateSequence(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            for (int i = 0; i < Count - 1; i++)
            {
                // Act.
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.NotNull(createdEntry);
                Assert.NotEqual(Relation.None, loadedEntry.Next);
                Assert.Equal(createdEntries[i + 1].Id, loadedEntry.Next.Id);
            }
        }

        [Fact]
        public async Task EntryRepository_Store_First_Type_Hierarchy_Check_Child_Identifiers_Based_On_Created()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateFirstTypeHierarchy(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            for (int i = 0; i < Count - 1; i++)
            {
                // Act.
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                var loadedChildren = ((Entry)loadedEntry).Children;
                var createdChildren = ((Entry)createdEntry).Children;
                Assert.NotNull(createdChildren);
                Assert.Equal(loadedChildren.First().Id, createdEntries[i + 1].Id);
            }
        }

        [Fact]
        public async Task EntryRepository_Store_Second_Type_Hierarchy_Check_Child_Identifiers_Based_On_Created()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateSecondTypeHierarchy(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            for (int i = 0; i < Count - 1; i++)
            {
                // Act.
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                var loadedChildren2 = ((Entry)loadedEntry).Children2;
                var createdChildren2 = ((Entry)createdEntry).Children2;
                Assert.NotNull(createdChildren2);
                Assert.Equal(loadedChildren2.First().Id, createdEntries[i + 1].Id);
            }
        }

        [Fact]
        public async Task EntryRepository_Store_Sequence_Check_Previous_Identifiers_Based_On_Created()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateSequence(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            for (int i = 1; i < Count; i++)
            {
                // Act.
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.NotEqual(Relation.None, loadedEntry.Previous);
                Assert.Equal(createdEntry.Previous, loadedEntry.Previous);
            }
        }

        [Fact]
        public async Task EntryRepository_Store_First_Type_Hierarchy_Parent_Previous_Identifiers_Based_On_Created()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateFirstTypeHierarchy(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            for (int i = 1; i < Count; i++)
            {
                // Act.
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.NotEqual(Relation.None, loadedEntry.Parent);
                Assert.Equal(createdEntry.Parent, loadedEntry.Parent);
            }
        }

        [Fact]
        public async Task EntryRepository_Store_Second_Type_Hierarchy_Parent_Previous_Identifiers_Based_On_Created()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateSecondTypeHierarchy(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            for (int i = 1; i < Count; i++)
            {
                // Act.
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.NotEqual(Relation.None, loadedEntry.Parent2);
                Assert.Equal(createdEntry.Parent2, loadedEntry.Parent2);
            }
        }

        [Fact]
        public async Task EntryRepository_Store_First_Type_Hierarchy_Child_With_Parent()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var parentEntry = (IEditableEntry)context.Host.Infrastructure.Entries.Prepare(space.Id);
            parentEntry = context.Host.Infrastructure.Entries.Store(parentEntry);
            var childEntry = (IEditableEntry)context.Host.Infrastructure.Entries.Prepare(space.Id);

            // Act.
            childEntry.Parent = Relation.NewRelation(parentEntry.Id);
            childEntry = context.Host.Infrastructure.Entries.Store(childEntry);

            // Assert.
            parentEntry = context.Host.Infrastructure.Entries.Get(parentEntry.Id, EntryRelation.Parent | EntryRelation.Child);
            Assert.True(parentEntry.Children.Count == 1);
            var childId = parentEntry.Children.First().Relations.First().Id;
            Assert.Equal(childEntry.Id, childId);
        }

        [Fact]
        public async Task EntryRepository_Store_Second_Type_Hierarchy_Child_With_Parent()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var parent2Entry = (IEditableEntry)context.Host.Infrastructure.Entries.Prepare(space.Id);
            parent2Entry = context.Host.Infrastructure.Entries.Store(parent2Entry);
            var child2Entry = (IEditableEntry)context.Host.Infrastructure.Entries.Prepare(space.Id);

            // Act.
            child2Entry.Parent2 = Relation.NewRelation(parent2Entry.Id);
            child2Entry = context.Host.Infrastructure.Entries.Store(child2Entry);

            // Assert.
            parent2Entry = context.Host.Infrastructure.Entries.Get(parent2Entry.Id, EntryRelation.Parent | EntryRelation.Child);
            Assert.True(parent2Entry.Children2.Count == 1);
            var child2Id = parent2Entry.Children2.First().Relations.First().Id;
            Assert.Equal(child2Entry.Id, child2Id);
        }

        [Fact]
        public async Task EntryRepository_Relate_Indexed_Entry_Using_Indexes()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var index = (IEditableEntry)context.Host.Infrastructure.Entries.Prepare(space.Id);
            index = context.Host.Infrastructure.Entries.Store(index);
            var entry = (IEditableEntry)context.Host.Infrastructure.Entries.Prepare(space.Id);

            // Act.
            entry.Indexes.Add(index.Id);
            entry = context.Host.Infrastructure.Entries.Store(entry);

            // Assert.
            index = context.Host.Infrastructure.Entries.Get(index.Id, EntryRelation.Index | EntryRelation.Indexed);
            Assert.NotEqual(Relation.None, index.Indexed);
            Assert.Equal(index.Indexed.Id, entry.Id);
        }


        [Fact]
        public async Task EntryRepository_Relate_Indexed_Entry_Using_Indexed()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = (IEditableEntry)context.Host.Infrastructure.Entries.Prepare(space.Id);
            entry = context.Host.Infrastructure.Entries.Store(entry);
            var index = (IEditableEntry)context.Host.Infrastructure.Entries.Prepare(space.Id);

            // Act.
            index.Indexed = Relation.NewRelation(entry.Id);
            index = context.Host.Infrastructure.Entries.Store(index);

            // Assert.
            entry = context.Host.Infrastructure.Entries.Get(entry.Id, EntryRelation.Index | EntryRelation.Indexed);
            Assert.True(entry.Indexes.Contains(index.Id));
        }

        [Fact]
        public async Task EntryRepository_Store_Sequence_Check_Previous_Identifiers_Based_On_Loaded()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateSequence(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            // Act.
            for (int i = 0; i < Count; i++)
            {
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);
            }

            // Assert.
            for (int i = 0; i < Count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                Assert.NotEqual(Identifier.Empty, loadedEntry.Id);
                Assert.Equal(loadedEntries[i + 1].Previous.Id, loadedEntry.Id);
            }
        }


        [Fact]
        public async Task EntryRepository_Store_Hierarchy_Check_Parent_Identifiers_Based_On_Loaded()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateFirstTypeHierarchy(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            // Act.
            for (int i = 0; i < Count; i++)
            {
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);
            }

            // Assert.
            for (int i = 0; i < Count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                Assert.NotEqual(Identifier.Empty, loadedEntry.Id);
                Assert.Equal(loadedEntries[i + 1].Parent.Id, loadedEntry.Id);
            }
        }


        [Fact]
        public async Task EntryRepository_Store_Sequence_Check_Next_Identifiers_Based_On_Loaded()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateSequence(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            // Act.
            for (int i = 0; i < Count; i++)
            {
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);
            }

            // Assert.
            for (int i = 0; i < Count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                var next = loadedEntry.Next.Id;
                Assert.NotEqual(Identifier.Empty, next);
                Assert.Equal(loadedEntries[i + 1].Id, next);
            }
        }

        [Fact]
        public async Task EntryRepository_Store_Hierarchy_Check_Child_Identifiers_Based_On_Loaded()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateFirstTypeHierarchy(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            // Act.
            for (int i = 0; i < Count; i++)
            {
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);
            }

            // Assert.
            for (int i = 0; i < Count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                var child = ((Entry)loadedEntry).Children.First().Id;
                Assert.NotEqual(Identifier.Empty, child);
                Assert.Equal(loadedEntries[i + 1].Id, child);
            }
        }


        [Fact]
        public async Task EntryRepository_Store_Sequence_Check_Next_Previous_Identifiers_Based_On_Loaded()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateSequence(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            // Act.
            for (int i = 0; i < Count; i++)
            {
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);
            }

            // Assert.
            for (int i = 0; i < Count - 1; i++)
            {
                var previousEntry = loadedEntries[i];
                var nextId = previousEntry.Next.Id;
                var nextEntry = loadedEntries[i + 1];
                var previousId = nextEntry.Previous.Id;
                Assert.NotEqual(Identifier.Empty, previousEntry.Id);
                Assert.Equal(previousId, previousEntry.Id);
                Assert.NotEqual(Identifier.Empty, nextEntry.Id);
                Assert.Equal(nextId, nextEntry.Id);
            }
        }

        [Fact]
        public async Task EntryRepository_Store_Hierarchy_Check_Child_Parent_Identifiers_Based_On_Loaded()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var createdEntries = await InfrastructureTestHelper.CreateFirstTypeHierarchy(Count, context.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[Count];

            // Act.
            for (int i = 0; i < Count; i++)
            {
                loadedEntries[i] = context.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);
            }

            // Assert.
            for (int i = 0; i < Count - 1; i++)
            {
                var parentEntry = loadedEntries[i];
                var childId = ((Entry)parentEntry).Children.First().Id;
                var childEntry = loadedEntries[i + 1];
                var parentId = childEntry.Parent.Id;
                Assert.NotEqual(Identifier.Empty, parentEntry.Id);
                Assert.Equal(parentId, parentEntry.Id);
                Assert.NotEqual(Identifier.Empty, childEntry.Id);
                Assert.Equal(childId, childEntry.Id);
            }
        }

        [Fact]
        public async Task EntryRepository_Store_Already_Existing_Entry()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var entry = context.Host.Infrastructure.Entries.Prepare(space.Id);
            var containerId = context.Host.Storage.ContainerProvider.FromIdentifier(entry.Id);
            var folder = context.Host.Storage.PathBuilder.GetFolder(containerId);
            Assert.True(context.Host.Storage.FolderManager.Exists(folder));

            // Act.
            entry = context.Host.Infrastructure.Entries.Store(entry);

            // Assert.
            var fileName = String.Format(context.Host.Storage.StorageSerializer.FileNameFormat, "Identifier");
            var file = Path.Combine(folder, fileName);
            Assert.True(context.Host.Storage.FileManager.Exists(file));

            // Act.
            entry = context.Host.Infrastructure.Entries.Store(entry);
        }
    }
}
