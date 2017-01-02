namespace EtAlii.Servus.Storage
{
    using Microsoft.Experimental.IO;
    using System;
    using System.IO;

    public abstract class NtfsStorageBase : ITestableStorage
    {
        public IItemSerializer Serializer { get { return _serializer; } }
        private readonly IItemSerializer _serializer;

        string ITestableStorage.BaseFolder { get { return this.BaseFolder; } }
        public string BaseFolder { get { return _baseFolder; } }
        private readonly string _baseFolder;

        public NtfsStorageBase(IItemSerializer serializer, IStorageConfiguration configuration)
        {
            _serializer = serializer;

            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _baseFolder = Path.Combine(folder, "EtAlii", "Servus", configuration.Name);
        }

        public void SaveToFile<T>(string path, T item)
            where T : class
        {
            // Ensure that the requested folder exists.
            var folder = Path.GetDirectoryName(path);
            LongPathHelper.Create(folder);

            //var fileName = GetFileName(instance);
            var temporaryFileName = Path.ChangeExtension(path, ".tmp");
            var backupFileName = Path.ChangeExtension(path, ".bak");

            Serializer.Serialize(temporaryFileName, item);

            var shouldReplace = LongPathFile.Exists(path);
            if (shouldReplace)
            {
                LongPathFile.Move(path, backupFileName);
                LongPathFile.Move(temporaryFileName, path);
                LongPathFile.Delete(backupFileName);
            }
            else
            {
                LongPathFile.Copy(temporaryFileName, path, false);
                LongPathFile.Delete(temporaryFileName);
            }
        }


        public T LoadFromFile<T>(string path)
            where T : class
        {
            T item = null;

            // Ensure that the requested folder exists.
            var folder = Path.GetDirectoryName(path);
            LongPathHelper.Create(folder);

            if (LongPathFile.Exists(path))
            {
                item = Serializer.Deserialize<T>(path);
            }

            return item;
        }

        public string GetFolder(ContainerIdentifier container)
        {
            var relativePath = Path.Combine(container.Paths);
            return Path.Combine(_baseFolder, relativePath);
        }

        public string GetFileName(string fileId, ContainerIdentifier container)
        {
            var folder = GetFolder(container);
            var fileName = String.Format(Serializer.FileNameFormat, fileId);
            return Path.Combine(folder, fileName);
        }
    }
}
