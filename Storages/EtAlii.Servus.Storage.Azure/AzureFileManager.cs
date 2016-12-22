namespace EtAlii.Servus.Storage.Azure
{
    using System;

    public partial class AzureFileManager : IFileManager
    {
        private readonly IStorageSerializer _serializer;
        private readonly IFolderManager _folderManager;

        public AzureFileManager(IStorageSerializer serializer, IFolderManager folderManager)
        {
            _folderManager = folderManager;
            _serializer = serializer;
        }

        public void SaveToFile<T>(string path, T item)
            where T : class
        {
            throw new NotImplementedException();
            //// Ensure that the requested folder exists.
            //var folder = _pathBuilder.GetDirectoryName(path);
            //_folderManager.Create(folder);

            ////var fileName = GetFileName(instance);
            //var temporaryFileName = Path.ChangeExtension(path, ".tmp");
            //var backupFileName = Path.ChangeExtension(path, ".bak");

            //_serializer.Serialize(temporaryFileName, item);

            //var shouldReplace = LongPathFile.Exists(path);
            //if (shouldReplace)
            //{
            //    LongPathFile.Move(path, backupFileName);
            //    LongPathFile.Move(temporaryFileName, path);
            //    LongPathFile.Delete(backupFileName);
            //}
            //else
            //{
            //    LongPathFile.Copy(temporaryFileName, path, false);
            //    LongPathFile.Delete(temporaryFileName);
            //}
        }

        public T LoadFromFile<T>(string path)
            where T : class
        {
            throw new NotImplementedException();
            //T item = null;

            //// Ensure that the requested folder exists.
            //var folder = _pathBuilder.GetDirectoryName(path);
            //_folderManager.Create(folder);

            //if (LongPathFile.Exists(path))
            //{
            //    item = _serializer.Deserialize<T>(path);
            //}

            //return item;
        }

        public bool Exists(string path)
        {
            throw new NotImplementedException();
            //return LongPathFile.Exists(path);
        }
    
        public void Delete(string path)
        {
            throw new NotImplementedException();
            //LongPathFile.Delete(path);
        }
    }
}
