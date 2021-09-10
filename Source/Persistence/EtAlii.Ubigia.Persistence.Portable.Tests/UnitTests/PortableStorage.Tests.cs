// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Persistence.Portable;
    using EtAlii.Ubigia.Serialization;
    using PCLStorage;
    using Xunit;
    using EtAlii.Ubigia.Tests;
    using Microsoft.Extensions.Configuration;

    [CorrelateUnitTests]
    public class PortableStorageTests
    {
        [Fact]
        public void PortableStorage_Create()
        {
            // Arrange.
            var configurationRoot = new ConfigurationBuilder().Build();
            var folderName = $"C:\\Temp\\{Guid.NewGuid()}";
            var serializer = new Serializer();
            var bsonItemSerializer = new BsonItemSerializer(serializer);
            var bsonPropertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageOptions = new StorageOptions(configurationRoot)
                .Use("Test");
            var folderStorage = new FileSystemFolder(folderName);
            var storageSerializer = new PortableStorageSerializer(bsonItemSerializer, bsonPropertiesSerializer, folderStorage);
            var pathBuilder = new PortablePathBuilder(storageOptions, storageSerializer);
            var folderManager = new PortableFolderManager(storageSerializer, folderStorage);
            var fileManager = new PortableFileManager(storageSerializer, folderManager, pathBuilder, folderStorage);
            var azureContainerProvider = new PortableContainerProvider();

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
            var storage = new DefaultStorage(storageOptions, pathBuilder, fileManager, folderManager, storageSerializer, itemStorage, componentStorage, blobStorage, azureContainerProvider, propertiesStorage);

            // Assert.
            Assert.NotNull(storage);

            // Assure.
            if (Directory.Exists(folderName))
            {
                Directory.Delete(folderName, true);
            }
        }
    }
}
