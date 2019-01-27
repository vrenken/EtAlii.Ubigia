namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;

    public partial class GrpcAuthenticationDataClient
    {
        public async Task<Api.Account> GetAccount(ISpaceConnection connection, string accountName)
        {
            if (connection.Account != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var account = await GetAccount(accountName, ((IGrpcSpaceConnection)connection).Transport);
            if (account == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectUsingAccount);
            }
            return account;
        }

        private Task<Api.Account> GetAccount(string accountName, IGrpcSpaceTransport transport)
        {
            var account = _account;
            if (account == null)
            {
                string message = $"Unable to connect using the specified account ({accountName})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return Task.FromResult(account);
        }

    }
}
