// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System.Threading.Tasks;

public sealed class AuthenticationContextStub : IAuthenticationContext
{
    /// <inheritdoc />
    public IAuthenticationDataClient Data { get; }

    /// <summary>
    /// Create a new <see cref="AuthenticationContextStub"/> instance.
    /// </summary>
    public AuthenticationContextStub()
    {
        Data = new AuthenticationDataClientStub();
    }

    /// <inheritdoc />
    public Task Open(ISpaceConnection spaceConnection)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Close(ISpaceConnection spaceConnection)
    {
        return Task.CompletedTask;
    }

}
