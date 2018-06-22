﻿namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IAuthenticationManagementDataClient : IStorageTransportClient
    {
        Task Authenticate(IStorageConnection connection);

        Task<Storage> GetConnectedStorage(IStorageConnection connection);

    }

    public interface IAuthenticationManagementDataClient<in TTransport> : IAuthenticationManagementDataClient, IStorageTransportClient<TTransport>
        where TTransport : IStorageTransport
    {
    }
}
