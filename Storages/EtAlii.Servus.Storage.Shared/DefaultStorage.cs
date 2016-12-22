namespace EtAlii.Servus.Storage
{
    public class DefaultStorage : IStorage
    {
        public IStorageConfiguration Configuration { get { return _configuration; } }
        private readonly IStorageConfiguration _configuration;

        public IPathBuilder PathBuilder { get { return _pathBuilder; } }
        private readonly IPathBuilder _pathBuilder;

        public IImmutableFileManager FileManager { get { return _fileManager; } }
        private readonly IImmutableFileManager _fileManager;

        public IImmutableFolderManager FolderManager { get { return _folderManager; } }
        private readonly IImmutableFolderManager _folderManager;

        public IStorageSerializer StorageSerializer { get { return _storageSerializer; } }
        private readonly IStorageSerializer _storageSerializer;

        public IItemStorage Items { get { return _items; } }
        private readonly IItemStorage _items;

        public IPropertiesStorage Properties { get { return _properties; } }
        private readonly IPropertiesStorage _properties;

        public IComponentStorage Components { get { return _components; } }
        private readonly IComponentStorage _components;

        public IBlobStorage Blobs { get { return _blobs; } }
        private readonly IBlobStorage _blobs;

        public IContainerProvider ContainerProvider { get { return _containerProvider; } }
        private readonly IContainerProvider _containerProvider;

        public DefaultStorage(
            IStorageConfiguration configuration,
            IPathBuilder pathBuilder,
            IImmutableFileManager fileManager,
            IImmutableFolderManager folderManager,
            IStorageSerializer storageSerializer,
            IItemStorage items,
            IComponentStorage components,
            IBlobStorage blobs, 
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
            _containerProvider = containerProvider;
            _properties = properties;
        }
    }
}
