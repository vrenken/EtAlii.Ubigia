namespace EtAlii.Servus.Storage.Portable.Tests.IntegrationTests
{
    using EtAlii.Servus.Storage.Portable.Tests;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.Servus.Tests;
    using Xunit;
    using System;
    using TestAssembly = EtAlii.Servus.Storage.Tests.TestAssembly;

    
    public class PortableFolderManager_Tests : PortableStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableFolderManager_Exists()
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
        public void PortableFolderManager_Create()
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
        public void PortableFolderManager_SaveToFolder_No_Folder_Exists()
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
        public void PortableFolderManager_Delete()
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
