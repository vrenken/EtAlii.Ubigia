namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;

    public interface IStorageInfoProvider 
    {
        string StorageName { get; }
        Storage Storage { get; }
        Guid StorageId { get; }
    }
}
