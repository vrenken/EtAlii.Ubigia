namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public interface IStorageFactory
    {
        IStorage Create();
        IStorage Create(IStorageConfiguration configuration);
    }
}