namespace EtAlii.Ubigia.Storage
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    public class InMemoryItems : IInMemoryItems
    {
        private readonly Folder _items;

        private const char SeparatorChar = '\\';
        private const string SeparatorString = @"\";

        public InMemoryItems()
        {
            _items = new Folder("Root");
        }

        public Item Find(string path)
        {
            return Find(path, _items);
        }

        public Item Find(string path, Folder folder)
        {
            var result = default(Item);

            path = path.Trim(SeparatorChar);

            if (String.IsNullOrEmpty(path))
            {
                result = folder;
            }
            else
            {
                var items = path.Split(SeparatorChar);
                var subItemName = items.First();
                if (items.Length > 1)
                {
                    var subFolder = folder.Items.SingleOrDefault(i => String.Compare(i.Name, subItemName, StringComparison.OrdinalIgnoreCase) == 0) as Folder;
                    if (subFolder != null)
                    {
                        var subPath = String.Join(SeparatorString, items.Skip(1));
                        result = Find(subPath, subFolder);
                    }
                }
                else if (items.Length == 1)
                {
                    result = folder.Items.SingleOrDefault(i => String.Compare(i.Name, subItemName, StringComparison.OrdinalIgnoreCase) == 0);
                }
                else
                {
                    result = folder;
                }
            }
            return result;
        }

        public bool Exists(string path)
        {
            return Find(path) != null;
        }

        public void Move(string sourcePath, string targetPath)
        {
            var sourceFolderName = Path.GetDirectoryName(sourcePath);
            var sourceFileName = Path.GetFileName(sourcePath);
            var sourceFolder = (Folder)Find(sourceFolderName);

            var targetFolderName = Path.GetDirectoryName(targetPath);
            var targetFileName = Path.GetFileName(targetPath);
            var targetFolder = (Folder)Find(targetFolderName);

            var sourceFile = (File)sourceFolder.Items.Single(i => String.Compare(i.Name, sourceFileName, StringComparison.OrdinalIgnoreCase) == 0);
            var targetFile = new File(targetFileName);
            targetFile.Content = sourceFile.Content;

            sourceFolder.Items.Remove(sourceFile);
            targetFolder.Items.Add(targetFile);
        }

        public void Delete(string path)
        {
            var folderName = Path.GetDirectoryName(path);
            var itemName = Path.GetFileName(path);
            var folder = (Folder)Find(folderName);

            //if (folder.Items.Contains(null))
            //{
            //    Debugger.Break();
            //}
            var item = folder.Items.Single(i => String.Compare(i.Name, itemName, StringComparison.OrdinalIgnoreCase) == 0);
            folder.Items.Remove(item);
        }
    }
}
