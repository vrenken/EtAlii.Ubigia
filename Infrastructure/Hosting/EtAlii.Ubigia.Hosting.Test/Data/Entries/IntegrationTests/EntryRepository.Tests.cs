namespace EtAlii.Ubigia.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using Xunit;
    using System;
    using System.IO;
    using System.Linq;
    using EtAlii.Ubigia.Storage;

    
    public class EntryRepository_Tests : IClassFixture<HostUnitTestContext>
    {
        private readonly HostUnitTestContext _testContext;
        private const int _count = 10;

        public EntryRepository_Tests(HostUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void EntryRepository_Prepare()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);

            // Act.
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);

            var containerId = _testContext.HostTestContext.Host.Storage.ContainerProvider.FromIdentifier(entry.Id);
            var folder = _testContext.HostTestContext.Host.Storage.PathBuilder.GetFolder(containerId);
            Assert.True(_testContext.HostTestContext.Host.Storage.FolderManager.Exists(folder));
        }

        [Fact]
        public void EntryRepository_Prepare_Timed_01()
        {
            // Arrange.
            var start = Environment.TickCount;
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);

            // Act.
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);

            // Assert.
            Assert.True(1500 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact]
        public void EntryRepository_Prepare_Timed_02()
        {
            // Arrange.
            var start = Environment.TickCount;
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);

            // Act.
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);

            // Assert.
            Assert.True(1500 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact]
        public void EntryRepository_Prepare_Timed_03()
        {
            // Arrange.
            var start = Environment.TickCount;
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);

            // Act.
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);

            // Assert.
            Assert.True(1500 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact]
        public void EntryRepository_Store_Only_Previous()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);

            // Act.
            entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(entry);

            var containerId = _testContext.HostTestContext.Host.Storage.ContainerProvider.FromIdentifier(entry.Id);
            var folder = _testContext.HostTestContext.Host.Storage.PathBuilder.GetFolder(containerId);
            Assert.True(_testContext.HostTestContext.Host.Storage.FolderManager.Exists(folder));
            var fileName = String.Format(_testContext.HostTestContext.Host.Storage.StorageSerializer.FileNameFormat, "Identifier");
            var file = Path.Combine(folder, fileName);
            Assert.True(_testContext.HostTestContext.Host.Storage.FileManager.Exists(file));
        }

        [Fact]
        public void EntryRepository_Store_Previous_And_Next()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry1 = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var entry2 = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            entry2 = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(entry2);

            // Act.
            entry2.Previous = Relation.NewRelation(entry1.Id);
            entry2 = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(entry2);

            // Arrange.
            var containerId = _testContext.HostTestContext.Host.Storage.ContainerProvider.FromIdentifier(entry1.Id);
            var folder = _testContext.HostTestContext.Host.Storage.PathBuilder.GetFolder(containerId);
            Assert.True(_testContext.HostTestContext.Host.Storage.FolderManager.Exists(folder));
            var fileName = String.Format(_testContext.HostTestContext.Host.Storage.StorageSerializer.FileNameFormat, "Identifier");
            var file = Path.Combine(folder, fileName);
            Assert.True(_testContext.HostTestContext.Host.Storage.FileManager.Exists(file));
            fileName = String.Format(_testContext.HostTestContext.Host.Storage.StorageSerializer.FileNameFormat, "Next");
            file = Path.Combine(folder, fileName);
            Assert.True(_testContext.HostTestContext.Host.Storage.FileManager.Exists(file));
        }

        [Fact]
        public void EntryRepository_Store_Sequence_Same_Identifiers()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateSequence(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 0; i < _count; i++)
            {
                // Act.
                loadedEntries[i] = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.NotEqual(Identifier.Empty, createdEntry.Id);
                Assert.Equal(loadedEntry.Id, createdEntry.Id);
            }
        }

        [Fact]
        public void EntryRepository_Store_Sequence_Check_Next_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateSequence(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 0; i < _count - 1; i++)
            {
                // Act.
                loadedEntries[i] = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.NotEqual(Relation.None, loadedEntry.Next);
                Assert.Equal(createdEntries[i + 1].Id, loadedEntry.Next.Id);
            }
        }

        [Fact]
        public void EntryRepository_Store_First_Type_Hierarchy_Check_Child_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateFirstTypeHierarchy(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 0; i < _count - 1; i++)
            {
                // Act.
                loadedEntries[i] = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                var loadedChildren = ((Entry)loadedEntry).Children;
                var createdChildren = ((Entry)createdEntry).Children;
                Assert.Equal(loadedChildren.First().Id, createdEntries[i + 1].Id);
            }
        }

        [Fact]
        public void EntryRepository_Store_Second_Type_Hierarchy_Check_Child_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateSecondTypeHierarchy(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 0; i < _count - 1; i++)
            {
                // Act.
                loadedEntries[i] = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                var loadedChildren2 = ((Entry)loadedEntry).Children2;
                var createdChildren2 = ((Entry)createdEntry).Children2;
                Assert.Equal(loadedChildren2.First().Id, createdEntries[i + 1].Id);
            }
        }

        [Fact]
        public void EntryRepository_Store_Sequence_Check_Previous_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateSequence(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 1; i < _count; i++)
            {
                // Act.
                loadedEntries[i] = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.NotEqual(Relation.None, loadedEntry.Previous);
                Assert.Equal(createdEntry.Previous, loadedEntry.Previous);
            }
        }

        [Fact]
        public void EntryRepository_Store_First_Type_Hierarchy_Parent_Previous_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateFirstTypeHierarchy(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 1; i < _count; i++)
            {
                // Act.
                loadedEntries[i] = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.NotEqual(Relation.None, loadedEntry.Parent);
                Assert.Equal(createdEntry.Parent, loadedEntry.Parent);
            }
        }

        [Fact]
        public void EntryRepository_Store_Second_Type_Hierarchy_Parent_Previous_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateSecondTypeHierarchy(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 1; i < _count; i++)
            {
                // Act.
                loadedEntries[i] = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.NotEqual(Relation.None, loadedEntry.Parent2);
                Assert.Equal(createdEntry.Parent2, loadedEntry.Parent2);
            }
        }

        [Fact]
        public void EntryRepository_Store_First_Type_Hierarchy_Child_With_Parent()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var parentEntry = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            parentEntry = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(parentEntry);
            var childEntry = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);

            // Act.
            childEntry.Parent = Relation.NewRelation(parentEntry.Id);
            childEntry = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(childEntry);

            // Assert.
            parentEntry = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(parentEntry.Id, EntryRelation.Parent | EntryRelation.Child);
            Assert.True(parentEntry.Children.Count == 1);
            var childId = parentEntry.Children.First().Relations.First().Id;
            Assert.Equal(childEntry.Id, childId);
        }

        [Fact]
        public void EntryRepository_Store_Second_Type_Hierarchy_Child_With_Parent()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var parent2Entry = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            parent2Entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(parent2Entry);
            var child2Entry = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);

            // Act.
            child2Entry.Parent2 = Relation.NewRelation(parent2Entry.Id);
            child2Entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(child2Entry);

            // Assert.
            parent2Entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(parent2Entry.Id, EntryRelation.Parent | EntryRelation.Child);
            Assert.True(parent2Entry.Children2.Count == 1);
            var child2Id = parent2Entry.Children2.First().Relations.First().Id;
            Assert.Equal(child2Entry.Id, child2Id);
        }

        [Fact]
        public void EntryRepository_Relate_Indexed_Entry_Using_Indexes()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var index = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            index = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(index);
            var entry = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);

            // Act.
            entry.Indexes.Add(index.Id);
            entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(entry);

            // Assert.
            index = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(index.Id, EntryRelation.Index | EntryRelation.Indexed);
            Assert.NotEqual(Relation.None, index.Indexed);
            Assert.Equal(index.Indexed.Id, entry.Id);
        }


        [Fact]
        public void EntryRepository_Relate_Indexed_Entry_Using_Indexed()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(entry);
            var index = (IEditableEntry)_testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);

            // Act.
            index.Indexed = Relation.NewRelation(entry.Id);
            index = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(index);

            // Assert.
            entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(entry.Id, EntryRelation.Index | EntryRelation.Indexed);
            Assert.True(entry.Indexes.Contains(index.Id));
        }

        [Fact]
        public void EntryRepository_Store_Sequence_Check_Previous_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateSequence(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                Assert.NotEqual(Identifier.Empty, loadedEntry.Id);
                Assert.Equal(loadedEntries[i + 1].Previous.Id, loadedEntry.Id);
            }
        }


        [Fact]
        public void EntryRepository_Store_Hierarchy_Check_Parent_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateFirstTypeHierarchy(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                Assert.NotEqual(Identifier.Empty, loadedEntry.Id);
                Assert.Equal(loadedEntries[i + 1].Parent.Id, loadedEntry.Id);
            }
        }


        [Fact]
        public void EntryRepository_Store_Sequence_Check_Next_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateSequence(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                var next = loadedEntry.Next.Id;
                Assert.NotEqual(Identifier.Empty, next);
                Assert.Equal(loadedEntries[i + 1].Id, next);
            }
        }

        [Fact]
        public void EntryRepository_Store_Hierarchy_Check_Child_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateFirstTypeHierarchy(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                var child = ((Entry)loadedEntry).Children.First().Id;
                Assert.NotEqual(Identifier.Empty, child);
                Assert.Equal(loadedEntries[i + 1].Id, child);
            }
        }


        [Fact]
        public void EntryRepository_Store_Sequence_Check_Next_Previous_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateSequence(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
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
        public void EntryRepository_Store_Hierarchy_Check_Child_Parent_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = InfrastructureTestHelper.CreateFirstTypeHierarchy(_count, _testContext.HostTestContext.Host.Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
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
        public void EntryRepository_Store_Already_Existing_Entry()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Prepare(space.Id);
            var containerId = _testContext.HostTestContext.Host.Storage.ContainerProvider.FromIdentifier(entry.Id);
            var folder = _testContext.HostTestContext.Host.Storage.PathBuilder.GetFolder(containerId);
            Assert.True(_testContext.HostTestContext.Host.Storage.FolderManager.Exists(folder));

            // Act.
            entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(entry);

            // Assert.
            var fileName = String.Format(_testContext.HostTestContext.Host.Storage.StorageSerializer.FileNameFormat, "Identifier");
            var file = Path.Combine(folder, fileName);
            Assert.True(_testContext.HostTestContext.Host.Storage.FileManager.Exists(file));

            // Act.
            entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Store(entry);
        }
    }
}
