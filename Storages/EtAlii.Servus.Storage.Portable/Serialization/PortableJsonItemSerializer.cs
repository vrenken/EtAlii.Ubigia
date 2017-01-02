namespace EtAlii.Servus.Storage.Portable
{
    using System;
    using System.IO;
    using System.Linq;
    using EtAlii.Servus.Api.Transport;
    using Newtonsoft.Json;
    using PCLStorage;

    public class PortableJsonItemSerializer : IItemSerializer
    {
        private readonly ISerializer _serializer;

        public string FileNameFormat { get { return _fileNameFormat; } }
        private const string _fileNameFormat = "{0}.json";
        private readonly IFolder _storage;

        public PortableJsonItemSerializer(
            ISerializer serializer,
            IFolder storage)
        {
            _serializer = serializer;
            _storage = storage;
            _serializer.Formatting = Formatting.Indented;
        }

        public void Serialize<T>(string fileName, T item)
            where T : class
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
                using (var textWriter = new StreamWriter(stream))
                {
                    using (var writer = new Newtonsoft.Json.JsonTextWriter(textWriter))
                    {
                        _serializer.Serialize(writer, item);
                    }
                }
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
                using (var textReader = new StreamReader(stream))
                {
                    using (var reader = new Newtonsoft.Json.JsonTextReader(textReader))
                    {
                        item = _serializer.Deserialize<T>(reader);
                    }
                }
            }

            return item;
        }
    }
}
