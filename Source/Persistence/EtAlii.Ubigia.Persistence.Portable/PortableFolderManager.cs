// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Portable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using PCLStorage;

    internal class PortableFolderManager : IFolderManager
    {
        private readonly IStorageSerializer _serializer;
        private readonly IFolder _storage;

        public PortableFolderManager(
            IStorageSerializer serializer,
            IFolder storage)
        {
            _serializer = serializer;
            _storage = storage;
        }

        public void SaveToFolder<T>(T item, string itemName, string folder)
            where T : class
        {
            var getFolderEntryTask = _storage.CheckExistsAsync(folder);
            getFolderEntryTask.Wait();
            var folderExists = getFolderEntryTask.Result == ExistenceCheckResult.FolderExists;

            if (!folderExists)
            {
                throw new InvalidOperationException($"The provided entry has not been prepared by the {nameof(PortableFolderManager)}.");
            }

            var fileName = string.Format(_serializer.FileNameFormat, itemName);
            fileName = PortablePath.Combine(folder, fileName);

            _serializer.Serialize(fileName, item);
        }

        public async Task<T> LoadFromFolder<T>(string folderName, string itemName)
            where T : class
        {
            T item = null;

            var folderEntry = await _storage.GetFolderAsync(folderName).ConfigureAwait(false);
            if (folderEntry != null)
            {
                var fileName = string.Format(_serializer.FileNameFormat, itemName);
                var checkFileExists = await folderEntry.CheckExistsAsync(fileName).ConfigureAwait(false);
                var exists = checkFileExists == ExistenceCheckResult.FileExists;
                if (exists)
                {
                    fileName = PortablePath.Combine(folderName, fileName);
                    item = await _serializer.Deserialize<T>(fileName).ConfigureAwait(false);
                }
            }

            return item;
        }


        public IEnumerable<string> EnumerateFiles(string folderName)
        {
            var getFolderTask = _storage.GetFolderAsync(folderName);
            getFolderTask.Wait();
            var folderEntry = getFolderTask.Result;
            var getFilesTask = folderEntry.GetFilesAsync();
            getFilesTask.Wait();
            return getFilesTask.Result
                .Select(f => PortablePath.Combine(folderName,f.Name))
                .AsEnumerable();
        }

        public IEnumerable<string> EnumerateFiles(string folderName, string searchPattern)
        {
            var files = EnumerateFiles(folderName);

            var parts = searchPattern.Split('*');
            if (parts.Length == 1)
            {
                return files.AsEnumerable();
            }

            if (parts.Length == 2)
            {
                var start = parts[0];
                var end = parts[1];
                return files.Where(name => name.StartsWith(start) && name.EndsWith(end))
                    .AsEnumerable();
            }

            throw new NotSupportedException("Unable to enumerate the files using the specified search pattern: " + searchPattern);
        }

        public IEnumerable<string> EnumerateDirectories(string folderName)
        {
            var getFolderTask = _storage.GetFolderAsync(folderName);
            getFolderTask.Wait();
            var folderEntry = getFolderTask.Result;
            var getFoldersTask = folderEntry.GetFoldersAsync();
            getFoldersTask.Wait();
            return getFoldersTask.Result
                .Select(f => f.Name)
                .AsEnumerable();
        }


        public bool Exists(string folderName)
        {
            var checkFolderTask = _storage.CheckExistsAsync(folderName);
            checkFolderTask.Wait();
            return checkFolderTask.Result == ExistenceCheckResult.FolderExists;
        }

        public void Create(string folderName)
        {
            var createFolderTask = _storage.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            createFolderTask.Wait();
            //var parts = folderName.Split[PortablePath.DirectorySeparatorChar]

            //var parent = _storage
            //foreach [var part in parts]
            //[
            //    var createFolderTask = parent.CreateFolderAsync[part, CreationCollisionOption.OpenIfExists]
            //    createFolderTask.Wait[]
            //    parent = createFolderTask.Result
            //]
        }

        public void Delete(string folderName)
        {
            var getFolderTask = _storage.GetFolderAsync(folderName);
            getFolderTask.Wait();
            var folder = getFolderTask.Result;
            var deleteFolderTask = folder.DeleteAsync();
            deleteFolderTask.Wait();
        }
    }
}
