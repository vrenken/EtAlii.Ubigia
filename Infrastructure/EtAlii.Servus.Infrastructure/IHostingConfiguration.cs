using EtAlii.Servus.Storage;

namespace EtAlii.Servus.Infrastructure
{
    public interface IHostingConfiguration : IStorageConfiguration
    {
        string Account { get; }
        string Password { get; }
        string Address { get; }
    }
}