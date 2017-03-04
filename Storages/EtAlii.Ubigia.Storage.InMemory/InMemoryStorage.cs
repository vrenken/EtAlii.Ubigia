namespace EtAlii.Ubigia.Storage.InMemory
{
    public class InMemoryStorage : IStorage
    {
        public IStorageConfiguration Configuration => _configuration;
        private readonly IStorageConfiguration _configuration;

        public IPathBuilder PathBuilder => _pathBuilder;
        private readonly IPathBuilder _pathBuilder;

        public IImmutableFileManager FileManager => _fileManager;
        private readonly IImmutableFileManager _fileManager;

        public IImmutableFolderManager FolderManager => _folderManager;
        private readonly IImmutableFolderManager _folderManager;

        public IStorageSerializer StorageSerializer => _storageSerializer;
        private readonly IStorageSerializer _storageSerializer;

        public IItemStorage Items => _items;
        private readonly IItemStorage _items;

        public IPropertiesStorage Properties => _properties;
        private readonly IPropertiesStorage _properties;

        public IComponentStorage Components => _components;
        private readonly IComponentStorage _components;

        public IBlobStorage Blobs => _blobs;
        private readonly IBlobStorage _blobs;

        public IInMemoryItems InMemoryItems => _inMemoryItems;
        private readonly IInMemoryItems _inMemoryItems;

        public IInMemoryItemsHelper InMemoryItemsHelper => _inMemoryItemsHelper;
        private readonly IInMemoryItemsHelper _inMemoryItemsHelper;

        public IContainerProvider ContainerProvider => _containerProvider;
        private readonly IContainerProvider _containerProvider;

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
        {
            _configuration = configuration;
            _pathBuilder = pathBuilder;
            _fileManager = fileManager;
            _folderManager = folderManager;
            _storageSerializer = storageSerializer;
            _items = items;
            _components = components;
            _blobs = blobs;
            _inMemoryItems = inMemoryItems;
            _containerProvider = containerProvider;
            _properties = properties;
            _inMemoryItemsHelper = inMemoryItemsHelper;
        }
    }
}
