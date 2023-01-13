// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR;

using System.Threading.Tasks;

public partial class SignalRAuthenticationDataClient
{
    public async Task<Account> GetAccount(ISpaceConnection connection, string accountName)
    {
        if (connection.Account != null)
        {
            throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
        }

        var account = await GetAccount(accountName).ConfigureAwait(false);
        if (account == null)
        {
            throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectUsingAccount);
        }
        return account;
    }

    private async Task<Account> GetAccount(string accountName)
    {
        var account = await _invoker.Invoke<Account>(_accountConnection, SignalRHub.Account, "GetForAuthenticationToken").ConfigureAwait(false);
        if (account == null)
        {
            var message = $"Unable to connect using the specified account ({accountName})";
            throw new UnauthorizedInfrastructureOperationException(message);
        }
        return account;
    }

}
