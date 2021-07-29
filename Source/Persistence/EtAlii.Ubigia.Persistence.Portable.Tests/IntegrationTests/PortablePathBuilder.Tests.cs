// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Portable.Tests
{
    using System.IO;
    using System.Linq;
    using EtAlii.Ubigia.Persistence.Tests;
    using PCLStorage;
    using Xunit;

    public class PortablePathBuilderTests : PortableStorageUnitTestContext
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortablePathBuilder_GetFileNameWithoutExtension()
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
        public void PortablePathBuilder_GetFolder()
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
        public void PortablePathBuilder_GetDirectoryName_Simple()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var directoryName = Storage.PathBuilder.GetDirectoryName(string.Join(PortablePath.DirectorySeparatorChar.ToString(), containerId.Paths));

            // Assert.
            var paths = containerId.Paths.Take(containerId.Paths.Length == 1 ? 1 : containerId.Paths.Length - 1);
            var expectedDirectoryName = string.Join(PortablePath.DirectorySeparatorChar.ToString(), paths);
            Assert.Equal(expectedDirectoryName, directoryName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortablePathBuilder_GetDirectoryName_Complex()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier("First");

            // Act.
            var directoryName = Storage.PathBuilder.GetDirectoryName(string.Join(PortablePath.DirectorySeparatorChar.ToString(), containerId.Paths));

            // Assert.
            var expectedDirectoryName = Path.GetDirectoryName(string.Join(PortablePath.DirectorySeparatorChar.ToString(), containerId.Paths));
            Assert.Equal(expectedDirectoryName, directoryName);
        }
    }
}
