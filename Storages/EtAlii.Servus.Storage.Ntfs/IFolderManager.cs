namespace EtAlii.Servus.Storage
{
    public interface IFolderManager
    {
        void SaveToFolder<T>(T item, string itemName, string folder)
            where T : class;

        T LoadFromFolder<T>(string folderName, string itemName)
            where T : class;
    }
}
