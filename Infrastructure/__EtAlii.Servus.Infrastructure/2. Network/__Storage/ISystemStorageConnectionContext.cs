namespace EtAlii.Servus.Infrastructure
{
    using EtAlii.Servus.Api;

    public interface ISystemStorageConnectionContext
    { 

        Storage Storage { get; }
        void Initialize(Storage storage);
    }
}
