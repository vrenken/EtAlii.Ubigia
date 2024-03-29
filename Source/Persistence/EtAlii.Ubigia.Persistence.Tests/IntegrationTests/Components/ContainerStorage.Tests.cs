﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Tests;
using Xunit;

[CorrelateUnitTests]
public class ComponentStorageTests : IAsyncLifetime
{
    private StorageUnitTestContext _testContext;

    public async Task InitializeAsync()
    {
        _testContext = new StorageUnitTestContext();
        await _testContext
            .InitializeAsync()
            .ConfigureAwait(false);
    }

    public async Task DisposeAsync()
    {
        await _testContext
            .DisposeAsync()
            .ConfigureAwait(false);
        _testContext = null;
    }

    [Fact]
    public void ComponentStorage_Prepare_Container()
    {
        // Arrange.
        var id = Guid.NewGuid().ToString();
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier(id);

        // Act.

        // Assert.
        Assert.NotNull(containerId.Paths);
    }

    [Fact]
    public void ComponentStorage_GetNextContainer_StorageException()
    {
        // Arrange.

        // Act.
        var act = new Action(() => _testContext.Storage.Components.GetNextContainer(ContainerIdentifier.Empty));

        // Assert.
        Assert.Throws<StorageException>(act);
    }

    [Fact]
    public async Task ComponentStorage_Retrieve_StorageException()
    {
        // Arrange.

        // Act.
        var act = new Func<Task>(async () => await _testContext.Storage.Components.Retrieve<NonCompositeComponent>(ContainerIdentifier.Empty).ConfigureAwait(false));

        // Assert.
        await Assert.ThrowsAsync<StorageException>(act).ConfigureAwait(false);
    }

    [Fact]
    public void ComponentStorage_Store_StorageException()
    {
        // Arrange.

        // Act.
        var act = new Action(() => _testContext.Storage.Components.Store<NonCompositeComponent>(ContainerIdentifier.Empty, null));

        // Assert.
        Assert.Throws<StorageException>(act);
    }

    [Fact]
    public void ComponentStorage_StoreAll_StorageException()
    {
        // Arrange.

        // Act.
        var act = new Action(() => _testContext.Storage.Components.StoreAll(ContainerIdentifier.Empty, Array.Empty<NonCompositeComponent>()));

        // Assert.
        Assert.Throws<StorageException>(act);
    }

    [Fact]
    public async Task ComponentStorage_RetrieveAll_StorageException()
    {
        // Arrange.

        // Act.
        var act = new Func<Task>(async () => await _testContext.Storage.Components.RetrieveAll<CompositeComponent>(ContainerIdentifier.Empty).ToArrayAsync().ConfigureAwait(false));

        // Assert.
        await Assert.ThrowsAsync<StorageException>(act).ConfigureAwait(false);
    }

    [Fact]
    public void ComponentStorage_Store_Entry()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var entry = StorageTestHelper.CreateEntry(storageId, accountId, spaceId, 0, 0, 0);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        var entryComponent = new IdentifierComponent { Id = entry.Id };

        // Act.
        _testContext.Storage.Components.Store(containerId, entryComponent);

        // Assert.
        Assert.True(entryComponent.Stored);
    }

    [Fact]
    public async Task ComponentStorage_Store_Retrieve_Entry()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var originalEntry = StorageTestHelper.CreateEntry(storageId, accountId, spaceId, 0, 0, 0);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        var entryComponent = new IdentifierComponent { Id = originalEntry.Id };
        _testContext.Storage.Components.Store(containerId, entryComponent);

        // Act.
        var retrievedEntry = await _testContext.Storage.Components.Retrieve<IdentifierComponent>(containerId).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(retrievedEntry);
        Assert.Equal(originalEntry.Id, retrievedEntry.Id);
    }

    [Fact]
    public async Task ComponentStorage_Store_Retrieve_ChildrenComponent()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var originalChildren = StorageTestHelper.CreateChildrenComponent(storageId, accountId, spaceId, 0, 0, 0);
        var originalName = ComponentHelper.GetName(originalChildren);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, originalChildren);

        // Act.
        var retrievedData = await _testContext.Storage.Components
            .RetrieveAll<ChildrenComponent>(containerId)
            .ToArrayAsync()
            .ConfigureAwait(false);

        // Assert.
        Assert.Single(retrievedData);
        var retrievedChildren = retrievedData.First();
        var retrievedName = ComponentHelper.GetName(retrievedChildren);
        Assert.Equal(originalName, retrievedName);
        Assert.Equal(originalChildren.Relations.First(), retrievedChildren.Relations.First());
    }

    /*
    [Fact]
    public void ComponentStorage_Store_Retrieve_ChildrenComponent_Twice()
    {
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();

        var originalChildren1 = CreateChildrenComponent(storageId, accountId, spaceId, 0, 0, 0);
        var originalName1 = ComponentBase.GetName(originalChildren1);

        var originalChildren2 = CreateChildrenComponent(storageId, accountId, spaceId, 0, 0, 0);
        var originalName2 = ComponentBase.GetName(originalChildren1);

        var containerId = CreateSimpleContainerIdentifier();
        ComponentStorage.StoreAll(originalChildren1, containerId);
        ComponentStorage.StoreAll(originalChildren2, containerId);

        var retrievedData = ComponentStorage.RetrieveAll<ChildrenComponent>(containerId);
        Assert.Equal(2, retrievedData.Count());

        var retrievedChildren = retrievedData.First();
        var retrievedName = ComponentBase.GetName(retrievedChildren);
        Assert.Equal(originalName1, retrievedName);
        Assert.Equal(originalChildren1.Children.First(), retrievedChildren.Children.First());

        retrievedChildren = retrievedData.Skip(1).First();
        retrievedName = ComponentBase.GetName(retrievedChildren);
        Assert.Equal(originalName2, retrievedName);
        Assert.Equal(originalChildren2.Children.First(), retrievedChildren.Children.First());
    }
    */

    [Fact]
    public async Task ComponentStorage_Store_Retrieve_ParentComponent()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var originalParent = StorageTestHelper.CreateParentComponent(storageId, accountId, spaceId, 0, 0, 0);
        var originalName = ComponentHelper.GetName(originalParent);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, originalParent);

        // Act.
        var retrievedData = await _testContext.Storage.Components.Retrieve<ParentComponent>(containerId).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(retrievedData);
        var retrievedParent = retrievedData;
        var retrievedName = ComponentHelper.GetName(retrievedParent);
        Assert.Equal(originalName, retrievedName);
        Assert.Equal(originalParent.Relation, retrievedParent.Relation);
    }

    [Fact]
    public async Task ComponentStorage_Store_Retrieve_NextComponent()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var originalNext = StorageTestHelper.CreateNextComponent(storageId, accountId, spaceId, 0, 0, 0);
        var originalName = ComponentHelper.GetName(originalNext);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, originalNext);

        // Act.
        var retrievedData = await _testContext.Storage.Components.Retrieve<NextComponent>(containerId).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(retrievedData);
        var retrievedNext = retrievedData;
        var retrievedName = ComponentHelper.GetName(retrievedNext);
        Assert.Equal(originalName, retrievedName);
        Assert.Equal(originalNext.Relation, retrievedNext.Relation);
    }

    [Fact]
    public async Task ComponentStorage_Store_Retrieve_PreviousComponent()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var originalPrevious = StorageTestHelper.CreatePreviousComponent(storageId, accountId, spaceId, 0, 0, 0);
        var originalName = ComponentHelper.GetName(originalPrevious);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, originalPrevious);

        // Act.
        var retrievedData = await _testContext.Storage.Components.Retrieve<PreviousComponent>(containerId).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(retrievedData);
        var retrievedPrevious = retrievedData;
        var retrievedName = ComponentHelper.GetName(retrievedPrevious);
        Assert.Equal(originalName, retrievedName);
        Assert.Equal(originalPrevious.Relation, retrievedPrevious.Relation);
    }

    [Fact]
    public async Task ComponentStorage_Store_Retrieve_UpdateComponent()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var originalUpdates = StorageTestHelper.CreateUpdatesComponent(storageId, accountId, spaceId, 0, 0, 0);
        var originalName = ComponentHelper.GetName(originalUpdates);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, originalUpdates);

        // Act.
        var retrievedData = await _testContext.Storage.Components
            .RetrieveAll<UpdatesComponent>(containerId)
            .ToArrayAsync()
            .ConfigureAwait(false);

        // Assert.
        Assert.NotNull(retrievedData);
        var retrievedUpdates = retrievedData.First();
        var retrievedName = ComponentHelper.GetName(retrievedUpdates);
        Assert.Equal(originalName, retrievedName);
        Assert.Equal(originalUpdates.Relations.First(), retrievedUpdates.Relations.First());
    }

    [Fact]
    public async Task ComponentStorage_Store_Retrieve_DowndateComponent()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var originalDowndate = StorageTestHelper.CreateDowndateComponent(storageId, accountId, spaceId, 0, 0, 0);
        var originalName = ComponentHelper.GetName(originalDowndate);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, originalDowndate);

        // Act.
        var retrievedData = await _testContext.Storage.Components.Retrieve<DowndateComponent>(containerId).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(retrievedData);
        var retrievedDowndate = retrievedData;
        var retrievedName = ComponentHelper.GetName(retrievedDowndate);
        Assert.Equal(originalName, retrievedName);
        Assert.Equal(originalDowndate.Relation, retrievedDowndate.Relation);
    }

    [Fact]
    public void ComponentStorage_Store_PreviousComponent_Twice()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var firstPrevious = StorageTestHelper.CreatePreviousComponent(storageId, accountId, spaceId, 0, 0, 0);
        var secondPrevious = StorageTestHelper.CreatePreviousComponent(storageId, accountId, spaceId, 0, 0, 0);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, firstPrevious);

        // Act.
        var act = new Action(() =>
        {
            _testContext.Storage.Components.Store(containerId, secondPrevious);
        });

        // Assert.
        Assert.Throws<StorageException>(act);
    }

    [Fact]
    public void ComponentStorage_Store_NextComponent_Twice()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var firstNext = StorageTestHelper.CreateNextComponent(storageId, accountId, spaceId, 0, 0, 0);
        var secondNext = StorageTestHelper.CreateNextComponent(storageId, accountId, spaceId, 0, 0, 0);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, firstNext);

        // Act.
        var act = new Action(() =>
        {
            _testContext.Storage.Components.Store(containerId, secondNext);
        });

        // Assert.
        Assert.Throws<StorageException>(act);
    }

    [Fact]
    public void ComponentStorage_Store_DowndateComponent_Twice()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var firstDowndate = StorageTestHelper.CreateDowndateComponent(storageId, accountId, spaceId, 0, 0, 0);
        var secondDowndate = StorageTestHelper.CreateDowndateComponent(storageId, accountId, spaceId, 0, 0, 0);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, firstDowndate);

        // Act.
        var act = new Action(() => _testContext.Storage.Components.Store(containerId, secondDowndate));

        // Assert.
        Assert.Throws<StorageException>(act);
    }

    //[Fact]
    //public void ComponentStorage_Store_UpdateComponent_Twice()
    //{
    //    // TODO: This test should fail!
    //    // Arrange.
    //    var storageId = Guid.NewGuid();
    //    var accountId = Guid.NewGuid();
    //    var spaceId = Guid.NewGuid();
    //    var firstUpdates = StorageTestHelper.CreateUpdatesComponent(storageId, accountId, spaceId, 0, 0, 0);
    //    var secondUpdates = StorageTestHelper.CreateUpdatesComponent(storageId, accountId, spaceId, 0, 0, 0);
    //    var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
    //    ComponentStorage.Store(containerId, firstUpdates);

    //    // Act.
    //    var act = new Action(() =>
    //    {
    //        ComponentStorage.Store(containerId, secondUpdates);
    //    });

    //    // Assert.
    //    Assert.Throws<StorageException>(act);
    //}

    [Fact]
    public void ComponentStorage_Store_ParentComponent_Twice()
    {
        // Arrange.
        var storageId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var spaceId = Guid.NewGuid();
        var firstParent = StorageTestHelper.CreateParentComponent(storageId, accountId, spaceId, 0, 0, 0);
        var secondParent = StorageTestHelper.CreateParentComponent(storageId, accountId, spaceId, 0, 0, 0);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, firstParent);

        // Act.
        var act = new Action(() => _testContext.Storage.Components.Store(containerId, secondParent));

        // Assert.
        Assert.Throws<StorageException>(act);
    }

    [Fact]
    public async Task ComponentStorage_Store_Retrieve_TypeComponent()
    {
        // Arrange.
        var type = Guid.NewGuid().ToString();

        var originalType = StorageTestHelper.CreateTypeComponent(type);
        var originalName = ComponentHelper.GetName(originalType);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, originalType);

        // Act.
        var retrievedData = await _testContext.Storage.Components.Retrieve<TypeComponent>(containerId).ConfigureAwait(false);

        // Assert.
        Assert.NotNull(retrievedData);
        var retrievedType = retrievedData;
        var retrievedName = ComponentHelper.GetName(retrievedType);
        Assert.Equal(originalName, retrievedName);
        Assert.Equal(originalType.Type, retrievedType.Type);
    }

    [Fact]
    public async Task ComponentStorage_Store_Retrieve_TagComponent()
    {
        // Arrange.
        var tag = Guid.NewGuid().ToString();

        var originalTag = StorageTestHelper.CreateTagComponent(tag);
        var originalName = ComponentHelper.GetName(originalTag);
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        _testContext.Storage.Components.Store(containerId, originalTag);

        // Act.
        var retrievedData = await _testContext.Storage.Components
            .Retrieve<TagComponent>(containerId)
            .ConfigureAwait(false);

        // Assert.
        Assert.NotNull(retrievedData);
        var retrievedTag = retrievedData;
        var retrievedName = ComponentHelper.GetName(retrievedTag);
        Assert.Equal(originalName, retrievedName);
        Assert.Equal(originalTag.Tag, retrievedTag.Tag);
    }
}
