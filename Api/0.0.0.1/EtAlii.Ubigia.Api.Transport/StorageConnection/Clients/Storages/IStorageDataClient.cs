namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStorageDataClient : IStorageTransportClient
    {
        Task<Storage> Add(string storageName, string storageAddress);
        Task Remove(Guid storageId);
        Task<Storage> Change(Guid storageId, string storageName, string storageAddress);
        Task<Storage> Get(string storageName);
        Task<Storage> Get(Guid storageId);
        Task<IEnumerable<Storage>> GetAll();
    }

    public interface IStorageDataClient<TTransport> : IStorageDataClient, IStorageTransportClient<TTransport>
        where TTransport : IStorageTransport
    {
    }
}
