namespace EtAlii.Ubigia.Storage
{
    public interface IFolderManager : IImmutableFolderManager
    {
        void Delete(string folderName);
    }
}
