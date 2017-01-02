namespace EtAlii.Ubigia.Storage.Azure
{
    using System;
    using EtAlii.Ubigia.Api;

    public partial class AzureFileManager : IFileManager
    {
        public void SaveToFile(string path, PropertyDictionary item)
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

        public PropertyDictionary LoadFromFile(string path)
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
    }
}
