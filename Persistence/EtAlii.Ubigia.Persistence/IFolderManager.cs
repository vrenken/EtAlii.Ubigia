namespace EtAlii.Ubigia.Persistence
{
    public interface IFolderManager : IImmutableFolderManager
    {
        void Delete(string folderName);
    }
}
