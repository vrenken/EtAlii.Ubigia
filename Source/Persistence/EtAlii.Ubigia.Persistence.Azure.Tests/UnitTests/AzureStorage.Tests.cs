﻿namespace EtAlii.Ubigia.Persistence.Azure.Tests
{
    using EtAlii.Ubigia.Serialization;
    using Xunit;

    public class AzureStorageTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void AzureStorage_Create()
        {
            // Arrange.
            var serializer = new Serializer();
            var bsonItemSerializer = new BsonItemSerializer(serializer);
            var bsonPropertiesSerializer = new BsonPropertiesSerializer(serializer);

            var storageConfiguration = new StorageConfiguration()
                .Use("Test");
            var storageSerializer = new AzureStorageSerializer();
            var pathBuilder = new AzurePathBuilder(storageConfiguration);
            var folderManager = new AzureFolderManager();
            var fileManager = new AzureFileManager();
            var azureContainerProvider = new DefaultContainerProvider();

            var itemStorage = new ItemStorage(storageSerializer, fileManager, folderManager, pathBuilder);
            var componentStorer = new ComponentStorer(folderManager, pathBuilder);
            var nextCompositeComponentIdAlgorithm = new NextCompositeComponentIdAlgorithm();
            var compositeComponentStorer = new CompositeComponentStorer(folderManager, pathBuilder, nextCompositeComponentIdAlgorithm);
            var componentRetriever = new ComponentRetriever(pathBuilder, folderManager, fileManager);
            var latestEntryGetter = new LatestEntryGetter(folderManager);
            var nextContainerIdentifierFromFolderAlgorithm = new NextContainerIdentifierFromFolderAlgorithm(pathBuilder, latestEntryGetter, azureContainerProvider);
            var nextContainerIdentifierFromTimeAlgorithm = new NextContainerIdentifierFromTimeAlgorithm(nextContainerIdentifierFromFolderAlgorithm, azureContainerProvider);
            var containerCreator = new ContainerCreator(folderManager, pathBuilder);
            var componentStorage = new ComponentStorage(componentStorer, compositeComponentStorer, componentRetriever, nextContainerIdentifierFromTimeAlgorithm, containerCreator);
            var blobStorer = new BlobStorer(folderManager, pathBuilder);
            var blobSummaryCalculator = new BlobSummaryCalculator(pathBuilder, fileManager);
            var blobRetriever = new BlobRetriever(folderManager, pathBuilder, blobSummaryCalculator);
            var blobPartStorer = new BlobPartStorer(folderManager, pathBuilder);
            var blobPartRetriever = new BlobPartRetriever(fileManager, pathBuilder);
            var blobStorage = new BlobStorage(blobStorer, blobRetriever, blobPartStorer, blobPartRetriever);
            var propertiesRetriever = new PropertiesRetriever(pathBuilder, fileManager);
            var propertiesStorer = new PropertiesStorer(pathBuilder, fileManager);
            var propertiesStorage = new PropertiesStorage(propertiesRetriever, propertiesStorer);

            // Act.
            var storage = new AzureStorage(storageConfiguration, pathBuilder, fileManager, folderManager, storageSerializer, itemStorage, componentStorage, blobStorage, azureContainerProvider, propertiesStorage);

            // Assert.
            Assert.NotNull(storage);
            Assert.NotNull(bsonItemSerializer);
            Assert.NotNull(bsonPropertiesSerializer);
        }
    }
}
