// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory
{
    public class InMemoryStorage : IStorage
    {
        public IStorageConfiguration Configuration { get; }

        public IPathBuilder PathBuilder { get; }

        public IImmutableFileManager FileManager { get; }

        public IImmutableFolderManager FolderManager { get; }

        public IStorageSerializer StorageSerializer { get; }

        public IItemStorage Items { get; }

        public IPropertiesStorage Properties { get; }

        public IComponentStorage Components { get; }

        public IBlobStorage Blobs { get; }

        public IInMemoryItems InMemoryItems { get; }

        public IInMemoryItemsHelper InMemoryItemsHelper { get; }

        public IContainerProvider ContainerProvider { get; }

        // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
        // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
        // specified by SonarQube. The current setup here is already some kind of facade that hides away many storage specific variations. Therefore refactoring to facades won't work.
        // Therefore this pragma warning disable of S107.
#pragma warning disable S107
        public InMemoryStorage(
            IStorageConfiguration configuration,
            IPathBuilder pathBuilder,
            IImmutableFileManager fileManager,
            IImmutableFolderManager folderManager,
            IStorageSerializer storageSerializer,
            IItemStorage items,
            IComponentStorage components,
            IBlobStorage blobs,
            IInMemoryItems inMemoryItems,
            IInMemoryItemsHelper inMemoryItemsHelper,
            IContainerProvider containerProvider,
            IPropertiesStorage properties)
#pragma warning restore S107
        {
            Configuration = configuration;
            PathBuilder = pathBuilder;
            FileManager = fileManager;
            FolderManager = folderManager;
            StorageSerializer = storageSerializer;
            Items = items;
            Components = components;
            Blobs = blobs;
            InMemoryItems = inMemoryItems;
            ContainerProvider = containerProvider;
            Properties = properties;
            InMemoryItemsHelper = inMemoryItemsHelper;
        }
    }
}
