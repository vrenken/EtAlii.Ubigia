namespace EtAlii.Servus.Infrastructure.Functional
{
    using EtAlii.Servus.Api;

    public interface ILocalStorageInitializer
    {
        void Initialize(Storage localStorage);
    }
}