// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.IO;
    using Xunit;

    public partial class PathBuilderTests : IDisposable
    {
        private readonly StorageUnitTestContext _testContext;

        public PathBuilderTests()
        {
            _testContext = new StorageUnitTestContext();
        }

        public void Dispose()
        {
            _testContext.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PathBuilder_GetFileNameWithoutExtension()
        {
            // Arrange.
            var fileName = "File";
            var fullFileName = $"{fileName}.Extension";

            // Act.
            var fileNameWithoutExtension = _testContext.Storage.PathBuilder.GetFileNameWithoutExtension(fullFileName);

            // Assert.
            Assert.Equal(fileName, fileNameWithoutExtension);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PathBuilder_GetFolder()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);

            // Assert.
            var expectedFolder = Path.Combine(_testContext.Storage.PathBuilder.BaseFolder, Path.Combine(containerId.Paths));
            Assert.Equal(expectedFolder, folder);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PathBuilder_GetDirectoryName()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var directoryName = _testContext.Storage.PathBuilder.GetDirectoryName(Path.Combine(containerId.Paths));

            // Assert.
            var expectedDirectoryName = _testContext.GetExpectedDirectoryName(containerId);
            Assert.Equal(expectedDirectoryName, directoryName);
        }
    }
}
