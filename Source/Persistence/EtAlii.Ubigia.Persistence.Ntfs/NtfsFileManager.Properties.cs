// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Ntfs;

using System.IO;

internal partial class NtfsFileManager
{
    public void SaveToFile(string path, PropertyDictionary item)
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

    public PropertyDictionary LoadFromFile(string path)
    {
        PropertyDictionary item = null;

        // Ensure that the requested folder exists.
        var folder = _pathBuilder.GetDirectoryName(path);
        _folderManager.Create(folder);

        if (File.Exists(path))
        {
            item = _serializer.Deserialize(path);
        }

        return item;
    }
}
