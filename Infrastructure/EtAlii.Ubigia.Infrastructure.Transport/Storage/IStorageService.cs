namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.Ubigia.Storage;

    public interface IStorageService : IService
    {
        IStorage Storage { get; }
    }
}