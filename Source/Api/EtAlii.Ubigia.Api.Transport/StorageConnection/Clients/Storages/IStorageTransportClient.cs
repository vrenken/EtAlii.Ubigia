// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IStorageTransportClient
    {
        /// <summary>
        /// Connect the client using the provided storageConnection.
        /// </summary>
        /// <param name="storageConnection"></param>
        /// <returns></returns>
        Task Connect(IStorageConnection storageConnection);

        /// <summary>
        /// Disconnect the client using the provided storageConnection.
        /// </summary>
        /// <param name="storageConnection"></param>
        /// <returns></returns>
        Task Disconnect(IStorageConnection storageConnection);
    }

    public interface IStorageTransportClient<in TTransport> : IStorageTransportClient
        where TTransport : IStorageTransport
    {
        /// <summary>
        /// Connect the client using the provided storageConnection.
        /// </summary>
        /// <param name="storageConnection"></param>
        /// <returns></returns>
        Task Connect(IStorageConnection<TTransport> storageConnection);

        /// <summary>
        /// Disconnect the client using the provided storageConnection.
        /// </summary>
        /// <param name="storageConnection"></param>
        /// <returns></returns>
        Task Disconnect(IStorageConnection<TTransport> storageConnection);
    }
}
