namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;

    public interface IStorageInitializer
    {
        Task Initialize(Storage storage);
    }
}