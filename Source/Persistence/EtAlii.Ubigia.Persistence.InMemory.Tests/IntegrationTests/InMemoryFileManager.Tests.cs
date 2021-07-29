// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class InMemoryFileManagerTests : IDisposable
    {
        private readonly InMemoryStorageUnitTestContext _testContext;
        public InMemoryFileManagerTests()
        {
            _testContext = new InMemoryStorageUnitTestContext();
        }

        public void Dispose()
        {
            _testContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void InMemoryFileManager_Exists()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var file = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);

            // Act.
            var exists = _testContext.Storage.FileManager.Exists(file);

            // Assert.
            Assert.False(exists);
        }

        [Fact]
        public void InMemoryFileManager_SaveToFile_String()
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


        [Fact]
        public void InMemoryFileManager_SaveToFile_Ulong()
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

        [Fact]
        public async Task InMemoryFileManager_LoadFromFile_String()
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

        [Fact]
        public async Task InMemoryFileManager_LoadFromFile_Ulong()
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

        [Fact]
        public void InMemoryFileManager_Delete()
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
        public void InMemoryFileManager_SaveToFile_Ulong_LongFilename()
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
        public async Task InMemoryFileManager_LoadFromFile_Ulong_LongFilename()
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
