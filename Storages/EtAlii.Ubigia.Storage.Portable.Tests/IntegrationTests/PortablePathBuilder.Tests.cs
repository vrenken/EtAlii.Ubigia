namespace EtAlii.Ubigia.Storage.Portable.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Storage.Portable.Tests;
    using EtAlii.Ubigia.Storage.Tests;
    using Xunit;
    using System;
    using System.IO;
    using System.Linq;
    using PCLStorage;
    using TestAssembly = EtAlii.Ubigia.Storage.Portable.Tests.TestAssembly;

    
    public class PortablePathBuilderTests : PortableStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortablePathBuilder_GetFileNameWithoutExtension()
        {
            // Arrange.
            var fileName = "File";
            var fullFileName = String.Format("{0}.Extension", fileName);
            
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
            var directoryName = Storage.PathBuilder.GetDirectoryName(String.Join(PortablePath.DirectorySeparatorChar.ToString(), containerId.Paths));

            // Assert.
            var paths = containerId.Paths.Take(containerId.Paths.Length == 1 ? 1 : containerId.Paths.Length - 1);
            var expectedDirectoryName = String.Join(PortablePath.DirectorySeparatorChar.ToString(), paths);
            Assert.Equal(expectedDirectoryName, directoryName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortablePathBuilder_GetDirectoryName_Complex()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier("First");

            // Act.
            var directoryName = Storage.PathBuilder.GetDirectoryName(String.Join(PortablePath.DirectorySeparatorChar.ToString(), containerId.Paths));

            // Assert.
            var expectedDirectoryName = Path.GetDirectoryName(String.Join(PortablePath.DirectorySeparatorChar.ToString(), containerId.Paths));
            Assert.Equal(expectedDirectoryName, directoryName);
        }
    }
}
