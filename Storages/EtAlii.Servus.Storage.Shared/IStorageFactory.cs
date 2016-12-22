namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;

    public interface IStorageFactory
    {
        IStorage Create();
        IStorage Create(IStorageConfiguration configuration);
    }
}