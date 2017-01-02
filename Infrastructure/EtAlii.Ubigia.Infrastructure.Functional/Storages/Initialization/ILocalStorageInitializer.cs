namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.Ubigia.Api;

    public interface ILocalStorageInitializer
    {
        void Initialize(Storage localStorage);
    }
}