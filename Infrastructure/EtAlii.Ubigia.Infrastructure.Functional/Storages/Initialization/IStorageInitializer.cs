namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    public interface IStorageInitializer
    {
        Task Initialize(Storage storage);
    }
}