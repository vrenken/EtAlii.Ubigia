// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class InMemoryFolderManagerTests : InMemoryStorageTestBase
    {
        [Fact]
        public void InMemoryFolderManager_Exists()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);

            // Act.
            var exists = Storage.FolderManager.Exists(folder);

            // Assert.
            Assert.False(exists);
        }

        [Fact]
        public void InMemoryFolderManager_Create()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Assert.False(Storage.FolderManager.Exists(folder));

            // Act.
            Storage.FolderManager.Create(folder);

            // Assert.
            Assert.True(Storage.FolderManager.Exists(folder));
        }


        [Fact]
        public void InMemoryFolderManager_SaveToFolder_No_Folder_Exists()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Assert.False(Storage.FolderManager.Exists(folder));

            // Act.
            var act = new Action(() =>
            {
                Storage.FolderManager.SaveToFolder(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), folder);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void InMemoryFolderManager_Delete()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Assert.False(Storage.FolderManager.Exists(folder));
            Storage.FolderManager.Create(folder);
            Assert.True(Storage.FolderManager.Exists(folder));

            // Act.
            ((IFolderManager)Storage.FolderManager).Delete(folder);

            // Assert.
            Assert.False(Storage.FolderManager.Exists(folder));
        }
    }
}
