namespace EtAlii.Ubigia.Persistence
{
    public interface IFileManager : IImmutableFileManager
    {
        void Delete(string path);
    }
}
