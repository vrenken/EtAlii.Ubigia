namespace EtAlii.Ubigia.Storage
{
    public interface IFileManager : IImmutableFileManager
    {
        void Delete(string path);
    }
}
