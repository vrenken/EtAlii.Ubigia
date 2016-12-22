namespace EtAlii.Servus.Storage.Ntfs.Tests.IntegrationTests
{
    using EtAlii.Servus.Storage.Ntfs.Tests;
    using EtAlii.Servus.Storage.Tests;
    using Xunit;
    using System;
    using System.IO;
    using TestAssembly = EtAlii.Servus.Storage.Tests.TestAssembly;

    
    public class NtfsPathBuilderTests : NtfsStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsPathBuilder_GetFileNameWithoutExtension()
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
        public void NtfsPathBuilder_GetFolder()
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
        public void NtfsPathBuilder_GetDirectoryName()
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
