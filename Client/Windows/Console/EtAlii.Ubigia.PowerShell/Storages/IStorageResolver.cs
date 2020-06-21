namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    public interface IStorageResolver
    {
        Task<Storage> Get(IStorageInfoProvider storageInfoProvider, Storage currentStorage, Uri currentStorageApiAddress, bool useCurrentStorage = true);
    }
}