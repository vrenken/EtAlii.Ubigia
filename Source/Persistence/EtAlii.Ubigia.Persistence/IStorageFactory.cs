namespace EtAlii.Ubigia.Persistence
{
    public interface IStorageFactory
    {
        IStorage Create();
        IStorage Create(IStorageConfiguration configuration);
    }
}