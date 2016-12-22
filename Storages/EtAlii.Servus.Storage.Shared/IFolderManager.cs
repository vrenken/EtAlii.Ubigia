namespace EtAlii.Servus.Storage
{
    public interface IFolderManager : IImmutableFolderManager
    {
        void Delete(string folderName);
    }
}
