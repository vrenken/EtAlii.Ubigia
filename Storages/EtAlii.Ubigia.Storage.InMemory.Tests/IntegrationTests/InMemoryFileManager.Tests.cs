namespace EtAlii.Ubigia.Storage.InMemory.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using System;

    
    public class InMemoryFileManager_Tests : InMemoryStorageTestBase
    {
        [Fact]
        public void InMemoryFileManager_Exists()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);

            // Act.
            var exists = Storage.FileManager.Exists(file);

            // Assert.
            Assert.False(exists);
        }

        [Fact]
        public void InMemoryFileManager_SaveToFile_String()
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


        [Fact]
        public void InMemoryFileManager_SaveToFile_Ulong()
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

        [Fact]
        public void InMemoryFileManager_LoadFromFile_String()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(Storage.FileManager.Exists(file));
            Storage.FileManager.SaveToFile(file, file);
            Assert.True(Storage.FileManager.Exists(file));

            // Act.
            var loadedFile = Storage.FileManager.LoadFromFile<string>(file);

            // Assert.
            Assert.Equal(file, loadedFile);
        }

        [Fact]
        public void InMemoryFileManager_LoadFromFile_Ulong()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(Storage.FileManager.Exists(file));
            var startPackage = new TestPackage<ulong> { Value = ulong.MaxValue - 1 };
            Storage.FileManager.SaveToFile(file, startPackage);
            Assert.True(Storage.FileManager.Exists(file));

            // Act.
            var resultPackage = Storage.FileManager.LoadFromFile<TestPackage<ulong>>(file);

            // Assert.
            Assert.Equal(startPackage.Value, resultPackage.Value);
        }

        [Fact]
        public void InMemoryFileManager_Delete()
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
        public void InMemoryFileManager_SaveToFile_Ulong_LongFilename()
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
        public void InMemoryFileManager_LoadFromFile_Ulong_LongFilename()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateLongContainerIndentifier(Storage.ContainerProvider);
            var file = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(Storage.FileManager.Exists(file));
            var startPackage = new TestPackage<ulong> { Value = ulong.MaxValue - 1 };
            Storage.FileManager.SaveToFile(file, startPackage);
            Assert.True(Storage.FileManager.Exists(file));

            // Act.
            var resultPackage = Storage.FileManager.LoadFromFile<TestPackage<ulong>>(file);

            // Assert.
            Assert.Equal(startPackage.Value, resultPackage.Value);
        }

    }
}
