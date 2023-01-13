// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Tests;
using Xunit;

[CorrelateUnitTests]
public class FolderManagerTests : IAsyncLifetime
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
    public void FolderManager_Exists()
    {
        // Arrange.
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);

        // Act.
        var exists = _testContext.Storage.FolderManager.Exists(folder);

        // Assert.
        Assert.False(exists);
    }

    [Fact]
    public void FolderManager_Create()
    {
        // Arrange.
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
        Assert.False(_testContext.Storage.FolderManager.Exists(folder));

        // Act.
        _testContext.Storage.FolderManager.Create(folder);

        // Assert.
        Assert.True(_testContext.Storage.FolderManager.Exists(folder));
    }

    [Fact]
    public void FolderManager_SaveToFolder_No_Folder_Exists()
    {
        // Arrange.
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
        Assert.False(_testContext.Storage.FolderManager.Exists(folder));

        // Act.
        var act = new Action(() =>
        {
            _testContext.Storage.FolderManager.SaveToFolder(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), folder);
        });

        // Assert.
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void FolderManager_Delete()
    {
        // Arrange.
        var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
        Assert.False(_testContext.Storage.FolderManager.Exists(folder));
        _testContext.Storage.FolderManager.Create(folder);
        Assert.True(_testContext.Storage.FolderManager.Exists(folder));

        // Act.
        ((IFolderManager)_testContext.Storage.FolderManager).Delete(folder);

        // Assert.
        Assert.False(_testContext.Storage.FolderManager.Exists(folder));
    }
}
