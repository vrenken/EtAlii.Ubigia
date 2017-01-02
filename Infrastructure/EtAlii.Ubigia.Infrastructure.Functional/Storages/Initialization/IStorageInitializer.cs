namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.Ubigia.Api;

    public interface IStorageInitializer
    {
        void Initialize(Storage storage);
    }
}