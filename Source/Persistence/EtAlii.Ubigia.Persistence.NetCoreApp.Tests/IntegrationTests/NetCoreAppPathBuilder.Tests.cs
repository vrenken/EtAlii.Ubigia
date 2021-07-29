// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.NetCoreApp.Tests
{
    using System.IO;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class NetCoreAppPathBuilderTests : NetCoreAppStorageUnitTestContext
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppPathBuilder_GetFileNameWithoutExtension()
        {
            // Arrange.
            var fileName = "File";
            var fullFileName = $"{fileName}.Extension";

            // Act.
            var fileNameWithoutExtension = Storage.PathBuilder.GetFileNameWithoutExtension(fullFileName);

            // Assert.
            Assert.Equal(fileName, fileNameWithoutExtension);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppPathBuilder_GetFolder()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var folder = Storage.PathBuilder.GetFolder(containerId);

            // Assert.
            var expectedFolder = Path.Combine(Storage.PathBuilder.BaseFolder, Path.Combine(containerId.Paths));
            Assert.Equal(expectedFolder, folder);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppPathBuilder_GetDirectoryName()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var directoryName = Storage.PathBuilder.GetDirectoryName(Path.Combine(containerId.Paths));

            // Assert.
            var expectedDirectoryName = Path.GetDirectoryName(Path.Combine(containerId.Paths));
            Assert.Equal(expectedDirectoryName, directoryName);
        }
    }
}
