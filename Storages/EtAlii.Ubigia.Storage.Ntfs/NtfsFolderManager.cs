namespace EtAlii.Ubigia.Storage.Ntfs
{
    using Microsoft.Experimental.IO;
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class NtfsFolderManager : IFolderManager
    {
        private readonly IStorageSerializer _serializer;

        public NtfsFolderManager(IStorageSerializer serializer)
        {
            _serializer = serializer;
        }

        public void SaveToFolder<T>(T item, string itemName, string folder)
            where T : class
        {
            if (!LongPathDirectory.Exists(folder))
            {
                throw new InvalidOperationException("The provided entry has not been prepared.");
            }

            var fileName = String.Format(_serializer.FileNameFormat, itemName);
            fileName = Path.Combine(folder, fileName);

            _serializer.Serialize(fileName, item);

            if (GetLength(fileName) == 0)
            {
                throw new InvalidOperationException("An empty file has been stored.");
            }
        }

        private long GetLength(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return fileInfo.Length;
        }

        public T LoadFromFolder<T>(string folderName, string itemName)
            where T : class
        {
            T item = null;

            if (LongPathDirectory.Exists(folderName))
            {
                var fileName = String.Format(_serializer.FileNameFormat, itemName);
                fileName = Path.Combine(folderName, fileName);

                if (LongPathFile.Exists(fileName))
                {
                    item = _serializer.Deserialize<T>(fileName);
                }
            }
            return item;
        }


        public IEnumerable<string> EnumerateFiles(string folderName)
        {
            return LongPathDirectory.EnumerateFiles(folderName);
        }

        public IEnumerable<string> EnumerateFiles(string folderName, string searchPattern)
        {
            return LongPathDirectory.EnumerateFiles(folderName, searchPattern);
        }

        public IEnumerable<string> EnumerateDirectories(string folderName)
        {
            return LongPathDirectory.EnumerateDirectories(folderName);
        }


        public bool Exists(string folderName)
        {
            return LongPathDirectory.Exists(folderName);
        }

        public void Create(string folderName)
        {
            var parentFolder = Path.GetDirectoryName(folderName);
            if (!LongPathDirectory.Exists(parentFolder))
            {
                Create(parentFolder);
            }
            LongPathDirectory.Create(folderName);
        }

        public void Delete(string folderName)
        {
            var subFolders = LongPathDirectory.EnumerateDirectories(folderName);
            foreach (var subFolder in subFolders)
            {
                Delete(subFolder);
            }

            var files = LongPathDirectory.EnumerateFiles(folderName);
            foreach (var file in files)
            {
                LongPathFile.Delete(file);
            }

            LongPathDirectory.Delete(folderName);
        }
    }
}
