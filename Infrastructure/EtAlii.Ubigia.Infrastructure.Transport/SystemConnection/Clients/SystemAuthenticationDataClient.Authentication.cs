namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal partial class SystemAuthenticationDataClient : IAuthenticationDataClient
    {
        private string _authenticationToken;

        public async Task Authenticate(ISpaceConnection connection)
        {
            string authenticationToken = await GetAuthenticationToken(connection.Configuration.AccountName, connection.Configuration.Password, connection.Configuration.Address);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                _authenticationToken = authenticationToken;
            }
            else
            {
                string message = String.Format("Unable to authenticate on the specified storage ({0})", connection.Configuration.Address);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        public async Task Authenticate(IStorageConnection connection)
        {
            string authenticationToken = await GetAuthenticationToken(connection.Configuration.AccountName, connection.Configuration.Password, connection.Configuration.Address);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                _authenticationToken = authenticationToken;
            }
            else
            {
                string message = String.Format("Unable to authenticate on the specified storage ({0})", connection.Configuration.Address);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        private async Task<string> GetAuthenticationToken(string accountName, string password, string address)
        {
            string authenticationToken;
            if (password == null && _authenticationToken != null)
            {
                authenticationToken = _authenticationToken;
            }
            else
            {
                authenticationToken = "System_" + Guid.NewGuid().ToString().Replace("-", "");
            }

            if (String.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new UnableToAuthorizeInfrastructureOperationException(InvalidInfrastructureOperation.UnableToAthorize);
            }
            return await Task.FromResult(authenticationToken);
        }
    }
}
