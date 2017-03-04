namespace EtAlii.Ubigia.Storage.Portable
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using PCLStorage;

    public partial class PortableStorageSerializer : IStorageSerializer
    {
        private readonly IInternalItemSerializer _itemSerializer;
        private readonly IInternalPropertiesSerializer _propertiesSerializer;
        private readonly IFolder _storage;

        public string FileNameFormat => _fileNameFormat;
        private const string _fileNameFormat = "{0}.bson";

        public PortableStorageSerializer(
            IInternalItemSerializer itemSerializer, 
            IInternalPropertiesSerializer propertiesSerializer,
            IFolder storage)
        {
            _itemSerializer = itemSerializer;
            _propertiesSerializer = propertiesSerializer;
            _storage = storage;
        }

        public void Serialize<T>(string fileName, T item)
            where T: class
        {
            var parts = fileName.Split(PortablePath.DirectorySeparatorChar);
            fileName = parts.Length > 1 ? parts.Skip(parts.Length - 1).Single() : parts.First();
            parts = parts.Length > 1 ? parts.Take(parts.Length - 1).ToArray() : parts;
            var folderName = String.Join(PortablePath.DirectorySeparatorChar.ToString(), parts);

            var createFolderTask = _storage.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            createFolderTask.Wait();
            var folder = createFolderTask.Result;

            var createFileTask = folder.CreateFileAsync(fileName, CreationCollisionOption.FailIfExists);
            createFileTask.Wait();

            var openFileTask = createFileTask.Result.OpenAsync(FileAccess.ReadAndWrite);
            openFileTask.Wait();

            using (var stream = openFileTask.Result)
            {
                _itemSerializer.Serialize<T>(stream, item);
            }
        }

        public T Deserialize<T>(string fileName)
            where T : class
        {
            T item = null;

            var parts = fileName.Split(PortablePath.DirectorySeparatorChar);
            fileName = parts.Length > 1 ? parts.Skip(parts.Length - 1).Single() : parts.First();
            parts = parts.Length > 1 ? parts.Take(parts.Length - 1).ToArray() : parts;
            var folderName = String.Join(PortablePath.DirectorySeparatorChar.ToString(), parts);

            var getFolderTask = _storage.GetFolderAsync(folderName);
            getFolderTask.Wait();
            var folder = getFolderTask.Result;

            var getFileTask = folder.GetFileAsync(fileName);
            getFileTask.Wait();

            var openFileTask = getFileTask.Result.OpenAsync(FileAccess.Read);
            openFileTask.Wait();

            using (var stream = openFileTask.Result)
            {
                item = _itemSerializer.Deserialize<T>(stream);
            }

            return item;
        }

        public void Serialize(string fileName, PropertyDictionary item)
        {
            var parts = fileName.Split(PortablePath.DirectorySeparatorChar);
            fileName = parts.Length > 1 ? parts.Skip(parts.Length - 1).Single() : parts.First();
            parts = parts.Length > 1 ? parts.Take(parts.Length - 1).ToArray() : parts;
            var folderName = String.Join(PortablePath.DirectorySeparatorChar.ToString(), parts);

            var createFolderTask = _storage.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            createFolderTask.Wait();
            var folder = createFolderTask.Result;

            var createFileTask = folder.CreateFileAsync(fileName, CreationCollisionOption.FailIfExists);
            createFileTask.Wait();

            var openFileTask = createFileTask.Result.OpenAsync(FileAccess.ReadAndWrite);
            openFileTask.Wait();

            using (var stream = openFileTask.Result)
            {
                _propertiesSerializer.Serialize(stream, item);
            }
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            var parts = fileName.Split(PortablePath.DirectorySeparatorChar);
            fileName = parts.Length > 1 ? parts.Skip(parts.Length - 1).Single() : parts.First();
            parts = parts.Length > 1 ? parts.Take(parts.Length - 1).ToArray() : parts;
            var folderName = String.Join(PortablePath.DirectorySeparatorChar.ToString(), parts);

            var getFolderTask = _storage.GetFolderAsync(folderName);
            getFolderTask.Wait();
            var folder = getFolderTask.Result;

            var getFileTask = folder.GetFileAsync(fileName);
            getFileTask.Wait();

            var openFileTask = getFileTask.Result.OpenAsync(FileAccess.Read);
            openFileTask.Wait();

            using (var stream = openFileTask.Result)
            {
                return _propertiesSerializer.Deserialize(stream);
            }
        }

    }
}
