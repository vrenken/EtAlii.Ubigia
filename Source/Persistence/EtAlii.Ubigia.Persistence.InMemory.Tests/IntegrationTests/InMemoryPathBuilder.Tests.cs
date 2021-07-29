// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class InMemoryPathBuilderTests : IDisposable
    {
        private readonly InMemoryStorageUnitTestContext _testContext;
        public InMemoryPathBuilderTests()
        {
            _testContext = new InMemoryStorageUnitTestContext();
        }

        public void Dispose()
        {
            _testContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void InMemoryPathBuilder_GetFileNameWithoutExtension()
        {
            // Arrange.
            var fileName = "File";
            var fullFileName = $"{fileName}.Extension";

            // Act.
            var fileNameWithoutExtension = _testContext.Storage.PathBuilder.GetFileNameWithoutExtension(fullFileName);

            // Assert.
            Assert.Equal(fileName, fileNameWithoutExtension);
        }

        [Fact]
        public void InMemoryPathBuilder_GetFolder()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);

            // Assert.
            var expectedFolder = Path.Combine(_testContext.Storage.PathBuilder.BaseFolder, Path.Combine(containerId.Paths));
            Assert.Equal(expectedFolder, folder);
        }

        [Fact]
        public void InMemoryPathBuilder_GetDirectoryName()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var directoryName = _testContext.Storage.PathBuilder.GetDirectoryName(Path.Combine(containerId.Paths));

            // Assert.
            var expectedDirectoryName = Path.GetDirectoryName(Path.Combine(containerId.Paths));
            Assert.Equal(expectedDirectoryName, directoryName);
        }
    }
}
