// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System.Threading.Tasks;

public interface IAuthenticationDataClient : ISpaceTransportClient
{
    Task Authenticate(ISpaceConnection connection, string accountName, string password);

    Task<Storage> GetConnectedStorage(ISpaceConnection connection);

    Task<Account> GetAccount(ISpaceConnection connection, string accountName);
    Task<Space> GetSpace(ISpaceConnection connection);
}

public interface IAuthenticationDataClient<in TTransport> : IAuthenticationDataClient, ISpaceTransportClient<TTransport>
    where TTransport : ISpaceTransport
{
}
