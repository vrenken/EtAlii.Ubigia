namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Persistence;
    using EtAlii.xTechnology.Hosting;

    public interface IStorageService : IService
    {
        IStorage Storage { get; }
    }
}