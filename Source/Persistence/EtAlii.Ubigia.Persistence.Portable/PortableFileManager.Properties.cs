// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Portable
{
    using PCLStorage;

    internal partial class PortableFileManager
    {
        public void SaveToFile(string path, PropertyDictionary item)
        {
            // Ensure that the requested folder exists.
            var folder = _pathBuilder.GetDirectoryName(path);
            _folderManager.Create(folder);

            var temporaryFileName = System.IO.Path.ChangeExtension(path, ".tmp");
            var backupFileName = System.IO.Path.ChangeExtension(path, ".bak");

            _serializer.Serialize(temporaryFileName, item);

            var checkExistsAsyncTask = _storage.CheckExistsAsync(path);
            checkExistsAsyncTask.Wait();

            var shouldReplace = checkExistsAsyncTask.Result == ExistenceCheckResult.FileExists;
            if (shouldReplace)
            {
                Move(path, backupFileName);
                Move(temporaryFileName, path);
                Delete(backupFileName);
            }
            else
            {
                Copy(temporaryFileName, path);
                Delete(temporaryFileName);
            }
        }

        public PropertyDictionary LoadFromFile(string path)
        {
            PropertyDictionary item = null;

            var checkExistsAsyncTask = _storage.CheckExistsAsync(path);
            checkExistsAsyncTask.Wait();

            var exists = checkExistsAsyncTask.Result == ExistenceCheckResult.FileExists;
            if (exists)
            {
                item = _serializer.Deserialize(path);
            }

            return item;
        }
    }
}
