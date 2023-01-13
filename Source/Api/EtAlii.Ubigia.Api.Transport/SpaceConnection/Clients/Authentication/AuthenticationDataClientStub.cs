// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System.Threading.Tasks;

/// <summary>
/// A stubbed data client that can be used to manage roots.
/// </summary>
public sealed class AuthenticationDataClientStub : IAuthenticationDataClient
{
    public Task Authenticate(ISpaceConnection connection, string accountName, string password)
    {
        return Task.CompletedTask;
    }


    public Task<Storage> GetConnectedStorage(ISpaceConnection connection)
    {
        return Task.FromResult<Storage>(null);
    }

    public Task<Account> GetAccount(ISpaceConnection connection, string accountName)
    {
        return Task.FromResult<Account>(null);
    }

    public Task<Space> GetSpace(ISpaceConnection connection)
    {
        return Task.FromResult<Space>(null);
    }

    public Task Connect(ISpaceConnection spaceConnection)
    {
        return Task.CompletedTask;
    }

    public Task Disconnect()
    {
        return Task.CompletedTask;
    }
}
