namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class SignalRAuthenticationDataClient : SignalRClientBase, IAuthenticationDataClient<ISignalRSpaceTransport>
    {
        public async Task<Space> GetSpace(ISpaceConnection connection)
        {
            if (connection.Space != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var space = await GetSpace(connection.Account, connection.Configuration.Space);
            if (space == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToSpace);
            }

            return space;
        }

        private async Task<Space> GetSpace(Account currentAccount, string spaceName)
        {
            var spaces = await _invoker.Invoke<IEnumerable<Space>>(_spaceProxy, SignalRHub.Space, "GetAllForAccount", currentAccount.Id.ToString());
            return spaces.FirstOrDefault(s => s.Name == spaceName);
        }
    }
}
