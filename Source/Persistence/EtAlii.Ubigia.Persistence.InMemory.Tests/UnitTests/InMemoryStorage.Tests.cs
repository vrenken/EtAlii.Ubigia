namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using EtAlii.Ubigia.Serialization;
    using Xunit;

    public class InMemoryStorageTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void InMemoryStorage_Create()
        {
            // Arrange.
            var serializer = new Serializer();
            var internalBsonItemSerializer = new InternalBsonItemSerializer(serializer);
            var internalBsonPropertiesSerializer = new InternalBsonPropertiesSerializer(serializer);

            var storageConfiguration = new StorageConfiguration()
                .Use("Test");
            var inMemoryItems = new InMemoryItems();
            var inMemoryItemsHelper = new InMemoryItemsHelper(inMemoryItems);
            var storageSerializer = new InMemoryStorageSerializer(internalBsonItemSerializer,internalBsonPropertiesSerializer, inMemoryItemsHelper);
            var pathBuilder = new InMemoryPathBuilder( storageSerializer);
            var folderManager = new InMemoryFolderManager(storageSerializer, inMemoryItems, inMemoryItemsHelper);
            var fileManager = new InMemoryFileManager(storageSerializer, folderManager, pathBuilder, inMemoryItems, inMemoryItemsHelper);
            var inMemoryContainerProvider = new InMemoryContainerProvider();

            var itemStorage = new ItemStorage(storageSerializer, fileManager, folderManager, pathBuilder);
            var componentStorer = new ComponentStorer(folderManager, pathBuilder);
            var nextCompositeComponentIdAlgorithm = new NextCompositeComponentIdAlgorithm();
            var compositeComponentStorer = new CompositeComponentStorer(folderManager, pathBuilder, nextCompositeComponentIdAlgorithm);
            var componentRetriever = new ComponentRetriever(pathBuilder, folderManager, fileManager);
            var latestEntryGetter = new LatestEntryGetter(folderManager);
            var nextContainerIdentifierFromFolderAlgorithm = new NextContainerIdentifierFromFolderAlgorithm(pathBuilder, latestEntryGetter, inMemoryContainerProvider);
            var nextContainerIdentifierFromTimeAlgorithm = new NextContainerIdentifierFromTimeAlgorithm(nextContainerIdentifierFromFolderAlgorithm, inMemoryContainerProvider);
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
            var storage = new InMemoryStorage(storageConfiguration, pathBuilder, fileManager, folderManager, storageSerializer, itemStorage, componentStorage, blobStorage, inMemoryItems, inMemoryItemsHelper, inMemoryContainerProvider, propertiesStorage);

            // Assert.
            Assert.NotNull(storage);
        }
    }
}
