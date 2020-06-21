namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IStorageInfoProvider 
    {
        string StorageName { get; }
        Storage Storage { get; }
        Guid StorageId { get; }
    }
}
