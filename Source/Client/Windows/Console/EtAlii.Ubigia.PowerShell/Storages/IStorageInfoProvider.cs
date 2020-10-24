namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;

    public interface IStorageInfoProvider 
    {
        string StorageName { get; }
        Storage Storage { get; }
        Guid StorageId { get; }
    }
}
