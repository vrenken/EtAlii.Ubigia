namespace EtAlii.Servus.Storage
{
    using System.IO;

    public partial class InMemoryFileManager : IFileManager
    {
        private readonly IStorageSerializer _serializer;
        private readonly IFolderManager _folderManager;
        private readonly IInMemoryItems _inMemoryItems;
        private readonly IInMemoryItemsHelper _inMemoryItemsHelper;
        private readonly IPathBuilder _pathBuilder;

        public InMemoryFileManager(
            IStorageSerializer serializer, 
            IFolderManager folderManager, 
            IPathBuilder pathBuilder,
            IInMemoryItems inMemoryItems,
            IInMemoryItemsHelper inMemoryItemsHelper)

        {
            _folderManager = folderManager;
            _serializer = serializer;
            _pathBuilder = pathBuilder;
            _inMemoryItems = inMemoryItems;
            _inMemoryItemsHelper = inMemoryItemsHelper;
        }

        public void SaveToFile<T>(string path, T item)
            where T : class
        {
            // Ensure that the requested folder exists.
            var folder = _pathBuilder.GetDirectoryName(path);
            _folderManager.Create(folder);

            //var fileName = GetFileName(instance);
            var temporaryFileName = Path.ChangeExtension(path, ".tmp");
            var backupFileName = Path.ChangeExtension(path, ".bak");

            _serializer.Serialize(temporaryFileName, item);

            var shouldReplace = _inMemoryItems.Exists(path);
            if (shouldReplace)
            {
                _inMemoryItems.Move(path, backupFileName);
                _inMemoryItems.Move(temporaryFileName, path);
                _inMemoryItems.Delete(backupFileName);
            }
            else
            {
                _inMemoryItemsHelper.Copy(temporaryFileName, path);
                _inMemoryItems.Delete(temporaryFileName);
            }
        }

        public T LoadFromFile<T>(string path)
            where T : class
        {
            T item = null;

            // Ensure that the requested folder exists.
            var folder = _pathBuilder.GetDirectoryName(path);
            _folderManager.Create(folder);

            if (_inMemoryItems.Exists(path))
            {
                item = _serializer.Deserialize<T>(path);
            }

            return item;
        }

        public bool Exists(string path)
        {
            return _inMemoryItems.Exists(path);
        }
    
        public void Delete(string path)
        {
            _inMemoryItems.Delete(path);
        }
    }
}
