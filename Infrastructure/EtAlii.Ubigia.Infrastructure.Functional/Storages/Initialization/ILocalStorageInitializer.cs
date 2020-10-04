namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;

    public interface ILocalStorageInitializer
    {
        Task Initialize(Storage localStorage);
    }
}