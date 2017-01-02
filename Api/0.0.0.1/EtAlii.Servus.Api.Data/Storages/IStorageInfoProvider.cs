namespace EtAlii.Servus.Api
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
