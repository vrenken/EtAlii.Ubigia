namespace EtAlii.Servus.Storage
{
    using System;
    using System.IO;
    using System.Linq;

    public class ItemStorage : IItemStorage
    {
        private readonly IFileManager _fileManager;
        private readonly IFolderManager _folderManager;
        private readonly IPathBuilder _pathBuilder;
        private readonly IItemSerializer _serializer;

        public ItemStorage(IItemSerializer serializer, IFileManager fileManager, IFolderManager folderManager, IPathBuilder pathBuilder)
        {
            _serializer = serializer;
            _pathBuilder = pathBuilder;
            _fileManager = fileManager;
            _folderManager = folderManager;
        }

        public void Remove(Guid id, ContainerIdentifier container)
        {
            var idName = id.ToString();
            var fileName = _pathBuilder.GetFileName(idName, container);

            // Ensure that the requested folder exists.
            var folder = Path.GetDirectoryName(fileName);
            _folderManager.Create(folder);

            _fileManager.Delete(fileName);
        }

        public void Store<T>(T item, Guid id, ContainerIdentifier container)
            where T : class
        {
            var idName = id.ToString();
            var fileName = _pathBuilder.GetFileName(idName, container);
            _fileManager.SaveToFile<T>(fileName, item);
        }

        public bool Has(Guid id, ContainerIdentifier container)
        {
            var idName = id.ToString();
            var fileName = _pathBuilder.GetFileName(idName, container);
            return _fileManager.Exists(fileName);
        }

        public T Retrieve<T>(Guid id, ContainerIdentifier container) 
            where T : class
        {
            var idName = id.ToString();
            var fileName = _pathBuilder.GetFileName(idName, container);
            return _fileManager.LoadFromFile<T>(fileName);
        }

        public Guid[] Get(ContainerIdentifier container)
        {
            var folder = _pathBuilder.GetFolder(container);
            // Ensure that the requested folder exists.
            _folderManager.Create(folder);

            var searchPattern = String.Format(_serializer.FileNameFormat, "*");
            var fileNames = _folderManager.EnumerateFiles(folder, searchPattern)
                                          .Select(fileName => Path.GetFileNameWithoutExtension(fileName))
                                          .ToArray();

            return fileNames.Select(fileName => Guid.Parse(fileName))
                            .ToArray();
        }
    }
}
