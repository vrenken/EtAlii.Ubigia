// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class FileManagerTests : IAsyncLifetime
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

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FileManager_Exists()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);

            // Act.
            var exists = _testContext.Storage.FileManager.Exists(file);

            // Assert.
            Assert.False(exists);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FileManager_SaveToFile_String()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(_testContext.Storage.FileManager.Exists(file));

            // Act.
            _testContext.Storage.FileManager.SaveToFile(file, file);

            // Assert.
            Assert.True(_testContext.Storage.FileManager.Exists(file));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FileManager_SaveToFile_Ulong()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(_testContext.Storage.FileManager.Exists(file));
            var startPackage = new TestPackage<ulong> { Value = ulong.MaxValue - 1 };

            // Act.
            _testContext.Storage.FileManager.SaveToFile(file, startPackage);

            // Assert.
            Assert.True(_testContext.Storage.FileManager.Exists(file));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FileManager_LoadFromFile_String()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(_testContext.Storage.FileManager.Exists(file));
            _testContext.Storage.FileManager.SaveToFile(file, file);
            Assert.True(_testContext.Storage.FileManager.Exists(file));

            // Act.
            var loadedFile = await _testContext.Storage.FileManager.LoadFromFile<string>(file).ConfigureAwait(false);

            // Assert.
            Assert.Equal(file, loadedFile);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FileManager_LoadFromFile_Ulong()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(_testContext.Storage.FileManager.Exists(file));
            var startPackage = new TestPackage<ulong> { Value = ulong.MaxValue - 1 };
            _testContext.Storage.FileManager.SaveToFile(file, startPackage);
            Assert.True(_testContext.Storage.FileManager.Exists(file));

            // Act.
            var resultPackage = await _testContext.Storage.FileManager.LoadFromFile<TestPackage<ulong>>(file).ConfigureAwait(false);

            // Assert.
            Assert.Equal(startPackage.Value, resultPackage.Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FileManager_Delete()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(_testContext.Storage.FileManager.Exists(file));
            _testContext.Storage.FileManager.SaveToFile(file, file);
            Assert.True(_testContext.Storage.FileManager.Exists(file));

            // Act.
            ((IFileManager)_testContext.Storage.FileManager).Delete(file);

            // Assert.
            Assert.False(_testContext.Storage.FileManager.Exists(file));
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void FileManager_SaveToFile_Ulong_LongFilename()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateLongContainerIndentifier(_testContext.Storage.ContainerProvider);
            var file = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(_testContext.Storage.FileManager.Exists(file));
            var startPackage = new TestPackage<ulong> { Value = ulong.MaxValue - 1 };

            // Act.
            _testContext.Storage.FileManager.SaveToFile(file, startPackage);

            // Assert.
            Assert.True(_testContext.Storage.FileManager.Exists(file));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FileManager_LoadFromFile_Ulong_LongFilename()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateLongContainerIndentifier(_testContext.Storage.ContainerProvider);
            var file = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            Assert.False(_testContext.Storage.FileManager.Exists(file));
            var startPackage = new TestPackage<ulong> { Value = ulong.MaxValue - 1 };
            _testContext.Storage.FileManager.SaveToFile(file, startPackage);
            Assert.True(_testContext.Storage.FileManager.Exists(file));

            // Act.
            var resultPackage = await _testContext.Storage.FileManager.LoadFromFile<TestPackage<ulong>>(file).ConfigureAwait(false);

            // Assert.
            Assert.Equal(startPackage.Value, resultPackage.Value);
        }


    }
}
