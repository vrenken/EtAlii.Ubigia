namespace EtAlii.Ubigia.Persistence.InMemory
{
    public interface IInMemoryItems
    {
        Item Find(string path);
        Item Find(string path, Folder folder);

        bool Exists(string path);
        void Move(string sourcePath, string targetPath);
        void Delete(string path);
    }
}