namespace EtAlii.Ubigia.Storage.InMemory.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Storage.Tests;
    using Xunit;
    using System;
    using System.IO;

    
    public class InMemoryPathBuilderTests : InMemoryStorageTestBase
    {
        [Fact]
        public void InMemoryPathBuilder_GetFileNameWithoutExtension()
        {
            // Arrange.
            var fileName = "File";
            var fullFileName = $"{fileName}.Extension";
            
            // Act.
            var fileNameWithoutExtension = Storage.PathBuilder.GetFileNameWithoutExtension(fullFileName);

            // Assert.
            Assert.Equal(fileName, fileNameWithoutExtension);
        }

        [Fact]
        public void InMemoryPathBuilder_GetFolder()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var folder = Storage.PathBuilder.GetFolder(containerId);

            // Assert.
            var expectedFolder = Path.Combine(Storage.PathBuilder.BaseFolder, Path.Combine(containerId.Paths));
            Assert.Equal(expectedFolder, folder);
        }

        [Fact]
        public void InMemoryPathBuilder_GetDirectoryName()
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
