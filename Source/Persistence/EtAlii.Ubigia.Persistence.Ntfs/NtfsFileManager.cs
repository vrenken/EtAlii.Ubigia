// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Ntfs
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Experimental.IO;

    internal partial class NtfsFileManager : IFileManager
    {
        private readonly IStorageSerializer _serializer;
        private readonly IFolderManager _folderManager;
        private readonly IPathBuilder _pathBuilder;

        public NtfsFileManager(
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

        public async Task<T> LoadFromFile<T>(string path)
            where T : class
        {
            T item = null;

            // Ensure that the requested folder exists.
            var folder = _pathBuilder.GetDirectoryName(path);
            _folderManager.Create(folder);

            if (LongPathFile.Exists(path))
            {
                item = await _serializer.Deserialize<T>(path).ConfigureAwait(false);
            }

            return item;
        }

        public bool Exists(string path)
        {
            return LongPathFile.Exists(path);
        }

        public void Delete(string path)
        {
            LongPathFile.Delete(path);
        }
    }
}
