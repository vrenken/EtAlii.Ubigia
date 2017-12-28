namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.Ubigia.Storage;

    public interface IStorageService : IService
    {
        IStorage Storage { get; }
    }
}