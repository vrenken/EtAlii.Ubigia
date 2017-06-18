namespace EtAlii.Ubigia.Storage.Ntfs.IntegrationTests
{
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using System;
    using TestAssembly = EtAlii.Ubigia.Storage.Ntfs.TestAssembly;

    
    public class NtfsFolderManager_Tests : NtfsStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsFolderManager_Exists()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);

            // Act.
            var exists = Storage.FolderManager.Exists(folder);

            // Assert.
            Assert.False(exists);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsFolderManager_Create()
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

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsFolderManager_SaveToFolder_No_Folder_Exists()
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
            ExceptionAssert.Throws<InvalidOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsFolderManager_Delete()
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
