﻿namespace EtAlii.Ubigia.Persistence.NetCoreApp
{
    using System.IO;

    internal partial class NetCoreAppFileManager : IFileManager
    {
        private readonly IStorageSerializer _serializer;
        private readonly IFolderManager _folderManager;
        private readonly IPathBuilder _pathBuilder;

        public NetCoreAppFileManager(
            IStorageSerializer serializer, 
            IFolderManager folderManager, 
            IPathBuilder pathBuilder)
        {
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;
            _serializer = serializer;
        }

        public void SaveToFile<T>(string path, T item)
            where T : class
        {
            // Ensure that the requested folder exists.
            var folder = _pathBuilder.GetDirectoryName(path);
            _folderManager.Create(folder);

            var temporaryFileName = Path.ChangeExtension(path, ".tmp");
            var backupFileName = Path.ChangeExtension(path, ".bak");

            _serializer.Serialize(temporaryFileName, item);

            var shouldReplace = File.Exists(path);
            if (shouldReplace)
            {
	            File.Move(path, backupFileName);
	            File.Move(temporaryFileName, path);
	            File.Delete(backupFileName);
            }
            else
            {
	            File.Copy(temporaryFileName, path, false);
	            File.Delete(temporaryFileName);
            }
        }

        public T LoadFromFile<T>(string path)
            where T : class
        {
            T item = null;

            // Ensure that the requested folder exists.
            var folder = _pathBuilder.GetDirectoryName(path);
            _folderManager.Create(folder);

            if (File.Exists(path))
            {
                item = _serializer.Deserialize<T>(path);
            }

            return item;
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    
        public void Delete(string path)
        {
	        File.Delete(path);
        }
    }
}