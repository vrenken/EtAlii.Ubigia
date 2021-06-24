// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory
{
    using System.IO;

    public partial class InMemoryFileManager
    {
        public void SaveToFile(string path, PropertyDictionary item)
        {
            // Ensure that the requested folder exists.
            var folder = _pathBuilder.GetDirectoryName(path);
            _folderManager.Create(folder);

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

        public PropertyDictionary LoadFromFile(string path)
        {
            PropertyDictionary item = null;

            // Ensure that the requested folder exists.
            var folder = _pathBuilder.GetDirectoryName(path);
            _folderManager.Create(folder);

            if (_inMemoryItems.Exists(path))
            {
                item = _serializer.Deserialize(path);
            }

            return item;
        }
    }
}
