// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class EntryRepositoryTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;
    private const int Count = 10;

    private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

    public EntryRepositoryTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task EntryRepository_Prepare()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);

        // Act.
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);

        var containerId = _testContext.Storage.ContainerProvider.FromIdentifier(entry.Id);
        var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
        Assert.True(_testContext.Storage.FolderManager.Exists(folder));
    }

    [Fact]
    public async Task EntryRepository_Prepare_Timed_01()
    {
        // Arrange.
        var start = Environment.TickCount;
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);

        // Act.
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(entry);
        Assert.True(1500 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
    }

    [Fact]
    public async Task EntryRepository_Prepare_Timed_02()
    {
        // Arrange.
        var start = Environment.TickCount;
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);

        // Act.
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(entry);
        Assert.True(1500 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
    }

    [Fact]
    public async Task EntryRepository_Prepare_Timed_03()
    {
        // Arrange.
        var start = Environment.TickCount;
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);

        // Act.
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(entry);
        Assert.True(1500 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
    }

    [Fact]
    public async Task EntryRepository_Store_Only_Previous()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);

        // Act.
        entry = await _testContext.Functional.Entries.Store(entry).ConfigureAwait(false);

        var containerId = _testContext.Storage.ContainerProvider.FromIdentifier(entry.Id);
        var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
        Assert.True(_testContext.Storage.FolderManager.Exists(folder));
        var fileName = string.Format(_testContext.Storage.StorageSerializer.FileNameFormat, "Identifier");
        var file = Path.Combine(folder, fileName);
        var exists = _testContext.Storage.FileManager.Exists(file);
        Assert.True(exists);
    }

    [Fact]
    public async Task EntryRepository_Store_Previous_And_Next()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry1 = (IEditableEntry)await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        var entry2 = (IEditableEntry)await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        entry2 = await _testContext.Functional.Entries.Store(entry2).ConfigureAwait(false);

        // Act.
        entry2.Previous = Relation.NewRelation(entry1.Id);
        entry2 = await _testContext.Functional.Entries.Store(entry2).ConfigureAwait(false);

        // Arrange.
        var containerId = _testContext.Storage.ContainerProvider.FromIdentifier(entry1.Id);
        var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
        Assert.True(_testContext.Storage.FolderManager.Exists(folder));
        var fileName = string.Format(_testContext.Storage.StorageSerializer.FileNameFormat, "Identifier");
        var file = Path.Combine(folder, fileName);
        var exists = _testContext.Storage.FileManager.Exists(file);
        Assert.True(exists);
        fileName = string.Format(_testContext.Storage.StorageSerializer.FileNameFormat, "Next");
        file = Path.Combine(folder, fileName);
        exists = _testContext.Storage.FileManager.Exists(file);
        Assert.True(exists);
        Assert.NotNull(entry2);
    }

    [Fact]
    public async Task EntryRepository_Store_Sequence_Same_Identifiers()
    {
        // Arrange.
        var createdEntries = await _infrastructureTestHelper.CreateSequence(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        for (var i = 0; i < Count; i++)
        {
            // Act.
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id).ConfigureAwait(false);

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
        var createdEntries = await _infrastructureTestHelper.CreateSequence(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        for (var i = 0; i < Count - 1; i++)
        {
            // Act.
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Previous | EntryRelations.Next).ConfigureAwait(false);

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
        var createdEntries = await _infrastructureTestHelper.CreateFirstTypeHierarchy(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        for (var i = 0; i < Count - 1; i++)
        {
            // Act.
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Parent | EntryRelations.Child).ConfigureAwait(false);

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
        var createdEntries = await _infrastructureTestHelper.CreateSecondTypeHierarchy(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        for (var i = 0; i < Count - 1; i++)
        {
            // Act.
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Parent | EntryRelations.Child).ConfigureAwait(false);

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
        var createdEntries = await _infrastructureTestHelper.CreateSequence(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        for (var i = 1; i < Count; i++)
        {
            // Act.
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Previous | EntryRelations.Next).ConfigureAwait(false);

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
        var createdEntries = await _infrastructureTestHelper.CreateFirstTypeHierarchy(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        for (var i = 1; i < Count; i++)
        {
            // Act.
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Parent | EntryRelations.Child).ConfigureAwait(false);

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
        var createdEntries = await _infrastructureTestHelper.CreateSecondTypeHierarchy(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        for (var i = 1; i < Count; i++)
        {
            // Act.
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Parent | EntryRelations.Child).ConfigureAwait(false);

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
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var parentEntry = (IEditableEntry)await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        parentEntry = await _testContext.Functional.Entries.Store(parentEntry).ConfigureAwait(false);
        var childEntry = (IEditableEntry)await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);

        // Act.
        childEntry.Parent = Relation.NewRelation(parentEntry.Id);
        childEntry = await _testContext.Functional.Entries.Store(childEntry).ConfigureAwait(false);

        // Assert.
        parentEntry = await _testContext.Functional.Entries.Get(parentEntry.Id, EntryRelations.Parent | EntryRelations.Child).ConfigureAwait(false);
        Assert.True(parentEntry.Children.Count == 1);
        var childId = parentEntry.Children.First().Relations.First().Id;
        Assert.Equal(childEntry.Id, childId);
    }

    [Fact]
    public async Task EntryRepository_Store_Second_Type_Hierarchy_Child_With_Parent()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var parent2Entry = (IEditableEntry)await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        parent2Entry = await _testContext.Functional.Entries.Store(parent2Entry).ConfigureAwait(false);
        var child2Entry = (IEditableEntry)await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);

        // Act.
        child2Entry.Parent2 = Relation.NewRelation(parent2Entry.Id);
        child2Entry = await _testContext.Functional.Entries.Store(child2Entry).ConfigureAwait(false);

        // Assert.
        parent2Entry = await _testContext.Functional.Entries.Get(parent2Entry.Id, EntryRelations.Parent | EntryRelations.Child).ConfigureAwait(false);
        Assert.True(parent2Entry.Children2.Count == 1);
        var child2Id = parent2Entry.Children2.First().Relations.First().Id;
        Assert.Equal(child2Entry.Id, child2Id);
    }

    [Fact]
    public async Task EntryRepository_Relate_Indexed_Entry_Using_Indexes()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var index = (IEditableEntry)await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        index = await _testContext.Functional.Entries.Store(index).ConfigureAwait(false);
        var entry = (IEditableEntry)await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);

        // Act.
        entry.AddIndex(index.Id);
        entry = await _testContext.Functional.Entries.Store(entry).ConfigureAwait(false);

        // Assert.
        index = await _testContext.Functional.Entries.Get(index.Id, EntryRelations.Index | EntryRelations.Indexed).ConfigureAwait(false);
        Assert.NotEqual(Relation.None, index.Indexed);
        Assert.Equal(index.Indexed.Id, entry.Id);
    }


    [Fact]
    public async Task EntryRepository_Relate_Indexed_Entry_Using_Indexed()
    {
        // Arrange.
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = (IEditableEntry)await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        entry = await _testContext.Functional.Entries.Store(entry).ConfigureAwait(false);
        var index = (IEditableEntry)await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);

        // Act.
        index.Indexed = Relation.NewRelation(entry.Id);
        index = await _testContext.Functional.Entries.Store(index).ConfigureAwait(false);

        // Assert.
        entry = await _testContext.Functional.Entries.Get(entry.Id, EntryRelations.Index | EntryRelations.Indexed).ConfigureAwait(false);
        Assert.True(entry.Indexes.Contains(index.Id));
    }

    [Fact]
    public async Task EntryRepository_Store_Sequence_Check_Previous_Identifiers_Based_On_Loaded()
    {
        // Arrange.
        var createdEntries = await _infrastructureTestHelper.CreateSequence(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        // Act.
        for (var i = 0; i < Count; i++)
        {
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Previous | EntryRelations.Next).ConfigureAwait(false);
        }

        // Assert.
        for (var i = 0; i < Count - 1; i++)
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
        var createdEntries = await _infrastructureTestHelper.CreateFirstTypeHierarchy(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        // Act.
        for (var i = 0; i < Count; i++)
        {
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Parent | EntryRelations.Child).ConfigureAwait(false);
        }

        // Assert.
        for (var i = 0; i < Count - 1; i++)
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
        var createdEntries = await _infrastructureTestHelper.CreateSequence(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        // Act.
        for (var i = 0; i < Count; i++)
        {
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Previous | EntryRelations.Next).ConfigureAwait(false);
        }

        // Assert.
        for (var i = 0; i < Count - 1; i++)
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
        var createdEntries = await _infrastructureTestHelper.CreateFirstTypeHierarchy(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        // Act.
        for (var i = 0; i < Count; i++)
        {
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Parent | EntryRelations.Child).ConfigureAwait(false);
        }

        // Assert.
        for (var i = 0; i < Count - 1; i++)
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
        var createdEntries = await _infrastructureTestHelper.CreateSequence(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        // Act.
        for (var i = 0; i < Count; i++)
        {
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Previous | EntryRelations.Next).ConfigureAwait(false);
        }

        // Assert.
        for (var i = 0; i < Count - 1; i++)
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
        var createdEntries = await _infrastructureTestHelper.CreateFirstTypeHierarchy(Count, _testContext.Functional).ConfigureAwait(false);
        var loadedEntries = new IEditableEntry[Count];

        // Act.
        for (var i = 0; i < Count; i++)
        {
            loadedEntries[i] = await _testContext.Functional.Entries.Get(createdEntries[i].Id, EntryRelations.Parent | EntryRelations.Child).ConfigureAwait(false);
        }

        // Assert.
        for (var i = 0; i < Count - 1; i++)
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
        var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
        var entry = await _testContext.Functional.Entries.Prepare(space.Id).ConfigureAwait(false);
        var containerId = _testContext.Storage.ContainerProvider.FromIdentifier(entry.Id);
        var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
        Assert.True(_testContext.Storage.FolderManager.Exists(folder));

        // Act.
        entry = await _testContext.Functional.Entries.Store(entry).ConfigureAwait(false);

        // Assert.
        var fileName = string.Format(_testContext.Storage.StorageSerializer.FileNameFormat, "Identifier");
        var file = Path.Combine(folder, fileName);
        var exists = _testContext.Storage.FileManager.Exists(file);
        Assert.True(exists);

        // Act.
        entry = await _testContext.Functional.Entries.Store(entry).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(entry);
    }
}
