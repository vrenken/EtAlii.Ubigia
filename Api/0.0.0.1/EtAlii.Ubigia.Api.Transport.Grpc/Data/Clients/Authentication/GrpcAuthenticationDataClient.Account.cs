namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;

    public partial class GrpcAuthenticationDataClient
    {
        public async Task<Api.Account> GetAccount(ISpaceConnection connection)
        {
            if (connection.Account != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var account = await GetAccount(connection.Configuration.AccountName, ((IGrpcSpaceConnection)connection).Transport);
            if (account == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectUsingAccount);
            }
            return account;
        }

        private Task<Api.Account> GetAccount(string accountName, IGrpcSpaceTransport transport)
        {
            var account = _account;
            //var account = await _invoker.Invoke<Account>(_accountConnection, GrpcHub.Account, "GetForAuthenticationToken");
            if (account == null)
            {
                string message = $"Unable to connect using the specified account ({accountName})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return Task.FromResult(account);
        }

    }
}
