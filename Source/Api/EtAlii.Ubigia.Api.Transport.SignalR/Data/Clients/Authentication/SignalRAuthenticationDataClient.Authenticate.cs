// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR;

using System.Threading.Tasks;

public partial class SignalRAuthenticationDataClient
{
    public async Task Authenticate(ISpaceConnection connection, string accountName, string password)
    {
        var signalRConnection = (ISignalRSpaceConnection)connection;
        var authenticationToken = await _signalRAuthenticationTokenGetter
            .GetAuthenticationToken(signalRConnection.Transport, accountName, password, signalRConnection.Transport.AuthenticationToken)
            .ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(authenticationToken))
        {
            signalRConnection.Transport.AuthenticationToken = authenticationToken;
        }
        else
        {
            var message = $"Unable to authenticate on the specified storage ({connection.Transport.Address})";
            throw new UnauthorizedInfrastructureOperationException(message);
        }
    }

    public async Task Authenticate(IStorageConnection connection, string accountName, string password)
    {
        var signalRConnection = (ISignalRStorageConnection)connection;
        var authenticationToken = await _signalRAuthenticationTokenGetter
            .GetAuthenticationToken(signalRConnection.Transport, accountName, password, signalRConnection.Transport.AuthenticationToken)
            .ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(authenticationToken))
        {
            signalRConnection.Transport.AuthenticationToken = authenticationToken;
        }
        else
        {
            var message = $"Unable to authenticate on the specified storage ({connection.Transport.Address})";
            throw new UnauthorizedInfrastructureOperationException(message);
        }
    }
}
