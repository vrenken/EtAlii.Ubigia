// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.NetCoreApp.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class NetCoreAppFileManagerTests : NetCoreAppStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppFileManager_Exists()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);

            // Act.
            var exists = Storage.FileManager.Exists(file);

            // Assert.
            Assert.False(exists);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppFileManager_SaveToFile_String()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(Storage.FileManager.Exists(file));

            // Act.
            Storage.FileManager.SaveToFile(file, file);

            // Assert.
            Assert.True(Storage.FileManager.Exists(file));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppFileManager_SaveToFile_Ulong()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(Storage.FileManager.Exists(file));
            var startPackage = new TestPackage<ulong> { Value = ulong.MaxValue - 1 };

            // Act.
            Storage.FileManager.SaveToFile(file, startPackage);

            // Assert.
            Assert.True(Storage.FileManager.Exists(file));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task NetCoreAppFileManager_LoadFromFile_String()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(Storage.FileManager.Exists(file));
            Storage.FileManager.SaveToFile(file, file);
            Assert.True(Storage.FileManager.Exists(file));

            // Act.
            var loadedFile = await Storage.FileManager.LoadFromFile<string>(file).ConfigureAwait(false);

            // Assert.
            Assert.Equal(file, loadedFile);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task NetCoreAppFileManager_LoadFromFile_Ulong()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(Storage.FileManager.Exists(file));
            var startPackage = new TestPackage<ulong> { Value = ulong.MaxValue - 1 };
            Storage.FileManager.SaveToFile(file, startPackage);
            Assert.True(Storage.FileManager.Exists(file));

            // Act.
            var resultPackage = await Storage.FileManager.LoadFromFile<TestPackage<ulong>>(file).ConfigureAwait(false);

            // Assert.
            Assert.Equal(startPackage.Value, resultPackage.Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppFileManager_Delete()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(Storage.FileManager.Exists(file));
            Storage.FileManager.SaveToFile(file, file);
            Assert.True(Storage.FileManager.Exists(file));

            // Act.
            ((IFileManager)Storage.FileManager).Delete(file);

            // Assert.
            Assert.False(Storage.FileManager.Exists(file));
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppFileManager_SaveToFile_Ulong_LongFilename()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateLongContainerIndentifier(Storage.ContainerProvider);
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(Storage.FileManager.Exists(file));
            var startPackage = new TestPackage<ulong> { Value = ulong.MaxValue - 1 };

            // Act.
            Storage.FileManager.SaveToFile(file, startPackage);

            // Assert.
            Assert.True(Storage.FileManager.Exists(file));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task NetCoreAppFileManager_LoadFromFile_Ulong_LongFilename()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateLongContainerIndentifier(Storage.ContainerProvider);
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(Storage.FileManager.Exists(file));
            var startPackage = new TestPackage<ulong> { Value = ulong.MaxValue - 1 };
            Storage.FileManager.SaveToFile(file, startPackage);
            Assert.True(Storage.FileManager.Exists(file));

            // Act.
            var resultPackage = await Storage.FileManager.LoadFromFile<TestPackage<ulong>>(file).ConfigureAwait(false);

            // Assert.
            Assert.Equal(startPackage.Value, resultPackage.Value);
        }


    }
}
