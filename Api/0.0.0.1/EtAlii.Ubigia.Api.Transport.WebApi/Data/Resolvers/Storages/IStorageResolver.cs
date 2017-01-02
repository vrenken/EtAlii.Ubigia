namespace EtAlii.Ubigia.Api.Management
{
    using System.Threading.Tasks;

    public interface IStorageResolver
    {
        Task<Storage> Get(IStorageInfoProvider storageInfoProvider, Storage currentStorage, bool useCurrentStorage = true);
    }
}