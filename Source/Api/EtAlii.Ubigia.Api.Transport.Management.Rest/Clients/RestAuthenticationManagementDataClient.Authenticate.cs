namespace EtAlii.Ubigia.Api.Transport.Management.Rest
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Rest;

    public partial class RestAuthenticationManagementDataClient
    {
        public async Task Authenticate(IStorageConnection storageConnection, string accountName, string password)
        {
            var restConnection = (IRestStorageConnection)storageConnection;

            var authenticationToken = await GetAuthenticationToken(
                restConnection.Client,
                restConnection.AddressFactory,
                accountName,
                password,
                storageConnection.Transport.Address).ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(authenticationToken))
            {
                restConnection.Client.AuthenticationToken = authenticationToken;
            }
            else
            {
                var message = $"Unable to authenticate on the specified storage ({storageConnection.Transport.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        [SuppressMessage("Sonar Code Smell", "S2068:Credentials should not be hard-coded", Justification = "Needed to make the downscale from admin/system to user account based authentication tokens")]
        private static async Task<string> GetAuthenticationToken(IRestInfrastructureClient client, IAddressFactory addressFactory, string accountName, string password, Uri address)
        {
            string authenticationToken;
            if (password == null && client.AuthenticationToken != null)
            {
				// These lines are needed to make the downscale from admin/system to user account based authentication tokens.
	            var credentials = new NetworkCredential(accountName, (string)null);
	            var localAddress = addressFactory.Create(address, RelativeUri.Authenticate, UriParameter.AccountName, accountName, UriParameter.AuthenticationToken);
	            authenticationToken = await client.Get<string>(localAddress, credentials).ConfigureAwait(false);
            }
            else
            {
                var credentials = new NetworkCredential(accountName, password);
                var localAddress = addressFactory.Create(address, RelativeUri.Authenticate);
                authenticationToken = await client.Get<string>(localAddress, credentials).ConfigureAwait(false);
            }

            if (string.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToAthorize);
            }
            return authenticationToken;
        }
    }
}
