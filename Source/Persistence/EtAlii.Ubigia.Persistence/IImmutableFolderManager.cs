namespace EtAlii.Ubigia.Persistence
{
    using System.Collections.Generic;

    public interface IImmutableFolderManager
    {
        void SaveToFolder<T>(T item, string itemName, string folder)
            where T : class;

        T LoadFromFolder<T>(string folderName, string itemName)
            where T : class;

        IEnumerable<string> EnumerateFiles(string folderName);
        IEnumerable<string> EnumerateFiles(string folderName, string searchPattern);

        IEnumerable<string> EnumerateDirectories(string folderName);

        bool Exists(string folderName);
        void Create(string folderName);
    }
}
