namespace EtAlii.Servus.Storage
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;

    public class InMemoryItemsHelper : IInMemoryItemsHelper
    {
        private readonly IInMemoryItems _inMemoryItems;

        public InMemoryItemsHelper(IInMemoryItems inMemoryItems)
        {
            _inMemoryItems = inMemoryItems;
        }

        public void Copy(string sourcePath, string targetPath)
        {
            var sourceFolderName = Path.GetDirectoryName(sourcePath);
            var sourceFileName = Path.GetFileName(sourcePath);
            var sourceFolder = (Folder)_inMemoryItems.Find(sourceFolderName);

            var targetFolderName = Path.GetDirectoryName(targetPath);
            var targetFileName = Path.GetFileName(targetPath);
            var targetFolder = (Folder)_inMemoryItems.Find(targetFolderName);

            var sourceFile = (File)sourceFolder.Items.Single(i => String.Compare(i.Name, sourceFileName, StringComparison.OrdinalIgnoreCase) == 0);
            var targetFile = new File(targetFileName);
            targetFile.Content = sourceFile.Content;

            targetFolder.Items.Add(targetFile);
        }

        public IEnumerable<string> EnumerateFiles(string folderName)
        {
            var result = new List<string>();
            var folder = (Folder)_inMemoryItems.Find(folderName);
            if (folder != null)
            {
                foreach (var item in folder.Items)
                {
                    if (item is File)
                    {
                        result.Add(Path.Combine(folderName, item.Name));
                    }
                }
            }
            return result;
        }

        public IEnumerable<string> EnumerateFiles(string folderName, string searchPattern)
        {
            var result = new List<string>();
            var pieces = searchPattern.Split('*');
            var prefix = pieces.First();
            var postFix = pieces.Last();

            var folder = (Folder)_inMemoryItems.Find(folderName);
            if (folder != null)
            {
                foreach (var item in folder.Items)
                {
                    if (item is File && item.Name.StartsWith(prefix) && item.Name.EndsWith(postFix))
                    {
                        result.Add(Path.Combine(folderName, item.Name));
                    }
                }
            }
            return result;
        }

        public IEnumerable<string> EnumerateDirectories(string folderName)
        {
            var result = new List<string>();
            var folder = (Folder)_inMemoryItems.Find(folderName);
            if (folder != null)
            {
                foreach (var item in folder.Items)
                {
                    if (item is Folder)
                    {
                        result.Add(Path.Combine(folderName, item.Name));
                    }
                }
            }
            return result;
        }

        public void CreateFolder(string path)
        {
            var folderName = Path.GetDirectoryName(path);
            var itemName = Path.GetFileName(path);
            var folder = (Folder)_inMemoryItems.Find(folderName);

            if (!folder.Items.Any(i => String.Compare(i.Name, itemName, StringComparison.OrdinalIgnoreCase) == 0))
            {
                folder.Items.Add(new Folder(itemName));
            }
        }

        public MemoryStream CreateFile(string path, out File file)
        {
            var folderName = Path.GetDirectoryName(path);
            var itemName = Path.GetFileName(path);
            var folder = (Folder)_inMemoryItems.Find(folderName);

            if(folder.Items.Any(item => item.Name == itemName))
            {
                throw new StorageException("Unable to add file: Another file with the same name already exists in the specified folder");
            }
            else
            {
                file = new File(itemName);
                folder.Items.Add(file);
            }

            return new MemoryStream();
        }

        public Stream OpenFile(string fileName)
        {
            var result = default(Stream);
            var file = _inMemoryItems.Find(fileName) as File;
            if(file != null)
            {
                result = new MemoryStream(file.Content, true);
            }
            return result;
        }

        public long GetLenght(string fileName)
        {
            var result = default(long);
            var file = _inMemoryItems.Find(fileName) as File;
            if (file != null)
            {
                result = file.Content.Length;
            }
            return result;
        }
    }
}
