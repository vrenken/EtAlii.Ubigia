// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class InMemoryFolderManagerTests : IDisposable
    {
        private readonly InMemoryStorageUnitTestContext _testContext;
        public InMemoryFolderManagerTests()
        {
            _testContext = new InMemoryStorageUnitTestContext();
        }

        public void Dispose()
        {
            _testContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void InMemoryFolderManager_Exists()
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
        public void InMemoryFolderManager_Create()
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
        public void InMemoryFolderManager_SaveToFolder_No_Folder_Exists()
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
        public void InMemoryFolderManager_Delete()
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
}
