// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Portable
{
    using System.Linq;
    using System.Threading.Tasks;
    using PCLStorage;

    internal partial class PortableFileManager : IFileManager
    {
        private readonly IStorageSerializer _serializer;
        private readonly IFolderManager _folderManager;
        private readonly IPathBuilder _pathBuilder;
        private readonly IFolder _storage;

        public PortableFileManager(
            IStorageSerializer serializer,
            IFolderManager folderManager,
            IPathBuilder pathBuilder,
            IFolder storage)
        {
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;
            _storage = storage;
            _serializer = serializer;
        }

        public void SaveToFile<T>(string path, T item)
            where T : class
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

        public async Task<T> LoadFromFile<T>(string path)
            where T : class
        {
            T item = null;

            var checkExists= await _storage.CheckExistsAsync(path).ConfigureAwait(false);
            var exists = checkExists == ExistenceCheckResult.FileExists;
            if (exists)
            {
                item = await _serializer.Deserialize<T>(path).ConfigureAwait(false);
            }

            return item;
        }

        public bool Exists(string path)
        {
            var checkExistsAsyncTask = _storage.CheckExistsAsync(path);
            checkExistsAsyncTask.Wait();

            return checkExistsAsyncTask.Result == ExistenceCheckResult.FileExists;
        }

        public void Delete(string path)
        {
            var getFileTask = _storage.GetFileAsync(path);
            getFileTask.Wait();
            var file = getFileTask.Result;

            var deleteFileTask = file.DeleteAsync();
            deleteFileTask.Wait();
        }

        private void Move(string sourceFile, string targetFile)
        {
            Copy(sourceFile, targetFile);
            Delete(sourceFile);
        }

        private void Copy(string sourceFile, string targetFile)
        {
            var targetParts = targetFile.Split(PortablePath.DirectorySeparatorChar);
            targetFile = targetParts.Length > 1 ? targetParts.Skip(targetParts.Length - 1).Single() : targetParts.First();
            targetParts = targetParts.Length > 1 ? targetParts.Take(targetParts.Length - 1).ToArray() : targetParts;
            var targetFolderName = string.Join(PortablePath.DirectorySeparatorChar.ToString(), targetParts);

            var getSourceFileTask = _storage.GetFileAsync(sourceFile);
            getSourceFileTask.Wait();

            var openSourceFileTask = getSourceFileTask.Result.OpenAsync(FileAccess.Read);
            openSourceFileTask.Wait();

            var createFolderTask = _storage.CreateFolderAsync(targetFolderName, CreationCollisionOption.OpenIfExists);
            createFolderTask.Wait();
            var folder = createFolderTask.Result;

            var createFileTask = folder.CreateFileAsync(targetFile, CreationCollisionOption.FailIfExists);
            createFileTask.Wait();

            var openTargetFileTask = createFileTask.Result.OpenAsync(FileAccess.ReadAndWrite);
            openTargetFileTask.Wait();

            const int bytesInMegaByte = 1024 * 1024;
            var data = new byte[bytesInMegaByte];

            using var source = openSourceFileTask.Result;
            using var target = openTargetFileTask.Result;

            int bytesRead;
            do
            {
                bytesRead = source.Read(data, 0, bytesInMegaByte);
                if (bytesRead > 0)
                {
                    target.Write(data, 0, bytesRead);
                }
            } while (bytesRead > 0);
        }
    }
}
