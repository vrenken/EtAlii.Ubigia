// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class InMemoryFolderManager : IFolderManager
    {
        private readonly IStorageSerializer _serializer;
        private readonly IInMemoryItems _inMemoryItems;
        private readonly IInMemoryItemsHelper _inMemoryItemsHelper;

        public InMemoryFolderManager(
            IStorageSerializer serializer,
            IInMemoryItems inMemoryItems,
            IInMemoryItemsHelper inMemoryItemsHelper)
        {
            _serializer = serializer;
            _inMemoryItems = inMemoryItems;
            _inMemoryItemsHelper = inMemoryItemsHelper;
        }

        public void SaveToFolder<T>(T item, string itemName, string folder)
            where T : class
        {
            if (!_inMemoryItems.Exists(folder))
            {
                throw new InvalidOperationException($"The provided entry has not been prepared by the {nameof(InMemoryFolderManager)}.");
            }

            var fileName = string.Format(_serializer.FileNameFormat, itemName);
            fileName = Path.Combine(folder, fileName);

            _serializer.Serialize(fileName, item);

            if (GetLength(fileName) == 0)
            {
                throw new InvalidOperationException("An empty file has been stored.");
            }
        }

        private long GetLength(string fileName)
        {
            return _inMemoryItemsHelper.GetLenght(fileName);
        }

        public async Task<T> LoadFromFolder<T>(string folderName, string itemName)
            where T : class
        {
            T item = null;

            if (_inMemoryItems.Exists(folderName))
            {
                var fileName = string.Format(_serializer.FileNameFormat, itemName);
                fileName = Path.Combine(folderName, fileName);

                if (_inMemoryItems.Exists(fileName))
                {
                    item = await _serializer.Deserialize<T>(fileName).ConfigureAwait(false);
                }
            }
            return item;
        }


        public IEnumerable<string> EnumerateFiles(string folderName)
        {
            return _inMemoryItemsHelper.EnumerateFiles(folderName);
        }

        public IEnumerable<string> EnumerateFiles(string folderName, string searchPattern)
        {
            return _inMemoryItemsHelper.EnumerateFiles(folderName, searchPattern);
        }

        public IEnumerable<string> EnumerateDirectories(string folderName)
        {
            return _inMemoryItemsHelper.EnumerateDirectories(folderName);
        }


        public bool Exists(string folderName)
        {
            return _inMemoryItems.Exists(folderName);
        }

        public void Create(string folderName)
        {
            if (!string.IsNullOrEmpty(folderName))
            {
                var parentFolder = Path.GetDirectoryName(folderName);
                if (!_inMemoryItems.Exists(parentFolder))
                {
                    Create(parentFolder);
                }
                _inMemoryItemsHelper.CreateFolder(folderName);
            }
        }

        public void Delete(string folderName)
        {
            var subFolders = _inMemoryItemsHelper.EnumerateDirectories(folderName);
            foreach (var subFolder in subFolders)
            {
                Delete(subFolder);
            }

            var files = _inMemoryItemsHelper.EnumerateFiles(folderName);
            foreach (var file in files)
            {
                _inMemoryItems.Delete(file);
            }

            _inMemoryItems.Delete(folderName);
        }
    }
}
