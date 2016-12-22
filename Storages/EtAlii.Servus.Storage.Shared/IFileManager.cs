namespace EtAlii.Servus.Storage
{
    public interface IFileManager : IImmutableFileManager
    {
        void Delete(string path);
    }
}
