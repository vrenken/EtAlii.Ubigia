// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
        IAsyncEnumerable<Storage> GetAll();
    }

    public interface IStorageDataClient<in TTransport> : IStorageDataClient, IStorageTransportClient<TTransport>
        where TTransport : IStorageTransport
    {
    }
}
