namespace EtAlii.Ubigia.Storage
{
    public interface IStorageFactory
    {
        IStorage Create();
        IStorage Create(IStorageConfiguration configuration);
    }
}