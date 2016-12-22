namespace EtAlii.Servus.Infrastructure.Functional
{
    using EtAlii.Servus.Api;

    public interface IStorageInitializer
    {
        void Initialize(Storage storage);
    }
}