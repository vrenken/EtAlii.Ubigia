// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IAuthenticationManagementDataClient : IStorageTransportClient
    {
        Task Authenticate(IStorageConnection storageConnection, string accountName, string password);
    }

    public interface IAuthenticationManagementDataClient<in TTransport> : IAuthenticationManagementDataClient, IStorageTransportClient<TTransport>
        where TTransport : IStorageTransport
    {
    }
}
