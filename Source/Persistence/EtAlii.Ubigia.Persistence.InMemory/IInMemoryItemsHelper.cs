namespace EtAlii.Ubigia.Persistence.InMemory
{
    using System.Collections.Generic;
    using System.IO;

    public interface IInMemoryItemsHelper
    {
        void Copy(string sourcePath, string targetPath);
        IEnumerable<string> EnumerateFiles(string folderName);
        IEnumerable<string> EnumerateFiles(string folderName, string searchPattern);
        IEnumerable<string> EnumerateDirectories(string folderName);
        void CreateFolder(string path);
        MemoryStream CreateFile(string path, out File file);
        Stream OpenFile(string fileName);
        long GetLenght(string fileName);
    }
}