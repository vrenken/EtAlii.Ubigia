﻿namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;

    public partial class GrpcAuthenticationDataClient
    {
        public async Task<Account> GetAccount(ISpaceConnection connection, string accountName)
        {
            if (connection.Account != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var account = await GetAccount(accountName);
            if (account == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectUsingAccount);
            }
            return account;
        }

        private Task<Account> GetAccount(string accountName)
        {
            var account = _account;
            //var account = await _invoker.Invoke<Account>(_accountConnection, GrpcHub.Account, "GetForAuthenticationToken")
            if (account == null)
            {
                var message = $"Unable to connect using the specified account ({accountName})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return Task.FromResult(account);
        }

    }
}
