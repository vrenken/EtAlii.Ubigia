namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    public interface ILocalStorageInitializer
    {
        Task Initialize(Storage localStorage);
    }
}