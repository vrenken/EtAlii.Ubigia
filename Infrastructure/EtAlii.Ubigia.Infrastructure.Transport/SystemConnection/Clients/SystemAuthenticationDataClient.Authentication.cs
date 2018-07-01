namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal partial class SystemAuthenticationDataClient
    {
        private string _authenticationToken;

        public async Task Authenticate(ISpaceConnection connection, string accountName, string password)
        {
            string authenticationToken = await GetAuthenticationToken(accountName, password);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                _authenticationToken = authenticationToken;
            }
            else
            {
                string message = $"Unable to authenticate on the specified storage ({connection.Transport.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        public async Task Authenticate(IStorageConnection connection, string accountName, string password)
        {
            string authenticationToken = await GetAuthenticationToken(accountName, password);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                _authenticationToken = authenticationToken;
            }
            else
            {
                string message = $"Unable to authenticate on the specified storage ({connection.Transport.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        private async Task<string> GetAuthenticationToken(string accountName, string password)
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
