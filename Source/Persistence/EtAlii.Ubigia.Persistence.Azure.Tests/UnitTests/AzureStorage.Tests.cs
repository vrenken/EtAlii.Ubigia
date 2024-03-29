﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests;

using EtAlii.Ubigia.Persistence.Azure;
using Xunit;
using EtAlii.Ubigia.Tests;
using Microsoft.Extensions.Configuration;

[CorrelateUnitTests]
public class AzureStorageTests
{
    [Fact]
    public void AzureStorage_Create()
    {
        // Arrange.
        var itemSerializer = new ItemSerializer();
        var propertiesSerializer = new PropertiesSerializer();
        var configurationRoot = new ConfigurationBuilder().Build();

        var storageOptions = new StorageOptions(configurationRoot)
            .Use("Test");
        var storageSerializer = new AzureStorageSerializer();
        var pathBuilder = new AzurePathBuilder(storageOptions);
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
        var storage = new AzureStorage(storageOptions, pathBuilder, fileManager, folderManager, storageSerializer, itemStorage, componentStorage, blobStorage, azureContainerProvider, propertiesStorage);

        // Assert.
        Assert.NotNull(storage);
        Assert.NotNull(itemSerializer);
        Assert.NotNull(propertiesSerializer);
    }
}
