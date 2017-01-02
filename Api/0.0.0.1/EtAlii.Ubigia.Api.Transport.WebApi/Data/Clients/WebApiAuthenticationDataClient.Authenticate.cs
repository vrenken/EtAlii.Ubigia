namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    public partial class WebApiAuthenticationDataClient : IAuthenticationDataClient
    {

        public async Task Authenticate(ISpaceConnection connection)
        {
            var webApiConnection = (IWebApiSpaceConnection)connection;

            string authenticationToken = await GetAuthenticationToken(
                webApiConnection.Client,
                webApiConnection.AddressFactory,
                webApiConnection.Configuration.AccountName,
                webApiConnection.Configuration.Password,
                webApiConnection.Configuration.Address);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                webApiConnection.Client.AuthenticationToken = authenticationToken;
            }
            else
            {
                string message = String.Format("Unable to authenticate on the specified storage ({0})", webApiConnection.Configuration.Address);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        public async Task Authenticate(IStorageConnection connection)
        {
            var webApiConnection = (IWebApiStorageConnection)connection;

            string authenticationToken = await GetAuthenticationToken(
                webApiConnection.Client, 
                webApiConnection.AddressFactory, 
                webApiConnection.Configuration.AccountName, 
                webApiConnection.Configuration.Password, 
                webApiConnection.Configuration.Address);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                webApiConnection.Client.AuthenticationToken = authenticationToken;
            }
            else
            {
                string message = String.Format("Unable to authenticate on the specified storage ({0})", webApiConnection.Configuration.Address);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        public static async Task<string> GetAuthenticationToken(IInfrastructureClient client, IAddressFactory addressFactory, string accountName, string password, string address)
        {
            string authenticationToken;
            if (password == null && client.AuthenticationToken != null)
            {
                authenticationToken = client.AuthenticationToken;
            }
            else
            {
                var credentials = new NetworkCredential(accountName, password);
                var localAddress = addressFactory.CreateFullAddress(address, RelativeUri.Authenticate);
                authenticationToken = await client.Get<string>(localAddress, credentials);
            }

            if (String.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new UnableToAuthorizeInfrastructureOperationException(InvalidInfrastructureOperation.UnableToAthorize);
            }
            return authenticationToken;
        }
    }
}
