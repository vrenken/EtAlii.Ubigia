namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.SignalR;

    public partial class SignalRAuthenticationManagementDataClient
    {
        public async Task Authenticate(IStorageConnection storageConnection, string accountName, string password)
        {
            var signalRConnection = (ISignalRStorageConnection)storageConnection;
            var authenticationToken = await _signalRAuthenticationTokenGetter.GetAuthenticationToken(signalRConnection.Transport, accountName, password, signalRConnection.Transport.AuthenticationToken);

            if (!string.IsNullOrWhiteSpace(authenticationToken))
            {
                signalRConnection.Transport.AuthenticationToken = authenticationToken;
            }
            else
            {
                var message = $"Unable to authenticate on the specified storage ({storageConnection.Transport.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }
    }
}
