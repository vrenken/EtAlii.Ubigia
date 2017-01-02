namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;

    public class StorageInfoProvider : IStorageInfoProvider 
    {
        public string StorageName { get; set; }
        public Storage Storage { get; set; }
        public Guid StorageId { get; set; }
    }
}
