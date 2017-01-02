namespace EtAlii.Servus.PowerShell.Storages
{
    using EtAlii.Servus.Client.Model;
    using System;

    public interface IStorageInfoProvider 
    {
        string StorageName { get; }
        Storage Storage { get; }
        Guid StorageId { get; }
    }
}
