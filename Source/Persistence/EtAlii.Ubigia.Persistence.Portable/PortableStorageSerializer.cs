// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Portable
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Serialization;
    using PCLStorage;

    public class PortableStorageSerializer : IStorageSerializer
    {
        private readonly IItemSerializer _itemSerializer;
        private readonly IPropertiesSerializer _propertiesSerializer;
        private readonly IFolder _storage;

        public string FileNameFormat { get; } = "{0}.bson";

        public PortableStorageSerializer(
            IItemSerializer itemSerializer,
            IPropertiesSerializer propertiesSerializer,
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
            var folderName = string.Join(PortablePath.DirectorySeparatorChar.ToString(), parts);

            var createFolderTask = _storage.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            createFolderTask.Wait();
            var folder = createFolderTask.Result;

            var createFileTask = folder.CreateFileAsync(fileName, CreationCollisionOption.FailIfExists);
            createFileTask.Wait();

            var openFileTask = createFileTask.Result.OpenAsync(FileAccess.ReadAndWrite);
            openFileTask.Wait();

            using var stream = openFileTask.Result;

            _itemSerializer.Serialize(stream, item);
        }

        public void Serialize(string fileName, PropertyDictionary item)
        {
            var parts = fileName.Split(PortablePath.DirectorySeparatorChar);
            fileName = parts.Length > 1 ? parts.Skip(parts.Length - 1).Single() : parts.First();
            parts = parts.Length > 1 ? parts.Take(parts.Length - 1).ToArray() : parts;
            var folderName = string.Join(PortablePath.DirectorySeparatorChar.ToString(), parts);

            var createFolderTask = _storage.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            createFolderTask.Wait();
            var folder = createFolderTask.Result;

            var createFileTask = folder.CreateFileAsync(fileName, CreationCollisionOption.FailIfExists);
            createFileTask.Wait();

            var openFileTask = createFileTask.Result.OpenAsync(FileAccess.ReadAndWrite);
            openFileTask.Wait();

            using var stream = openFileTask.Result;

            _propertiesSerializer.Serialize(stream, item);
        }

        public async Task<T> Deserialize<T>(string fileName)
            where T : class
        {
            var parts = fileName.Split(PortablePath.DirectorySeparatorChar);
            fileName = parts.Length > 1 ? parts.Skip(parts.Length - 1).Single() : parts.First();
            parts = parts.Length > 1 ? parts.Take(parts.Length - 1).ToArray() : parts;
            var folderName = string.Join(PortablePath.DirectorySeparatorChar.ToString(), parts);

            var folder = await _storage.GetFolderAsync(folderName).ConfigureAwait(false);
            var file = await folder.GetFileAsync(fileName).ConfigureAwait(false);
            await using var stream = await file.OpenAsync(FileAccess.Read).ConfigureAwait(false);

            return await _itemSerializer.Deserialize<T>(stream).ConfigureAwait(false);
        }

        public PropertyDictionary Deserialize(string fileName)
        {
            var parts = fileName.Split(PortablePath.DirectorySeparatorChar);
            fileName = parts.Length > 1 ? parts.Skip(parts.Length - 1).Single() : parts.First();
            parts = parts.Length > 1 ? parts.Take(parts.Length - 1).ToArray() : parts;
            var folderName = string.Join(PortablePath.DirectorySeparatorChar.ToString(), parts);

            var getFolderTask = _storage.GetFolderAsync(folderName);
            getFolderTask.Wait();
            var folder = getFolderTask.Result;

            var getFileTask = folder.GetFileAsync(fileName);
            getFileTask.Wait();

            var openFileTask = getFileTask.Result.OpenAsync(FileAccess.Read);
            openFileTask.Wait();

            using var stream = openFileTask.Result;

            return _propertiesSerializer.Deserialize(stream);
        }

    }
}
