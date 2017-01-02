namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class WebApiAuthenticationDataClient : IAuthenticationDataClient
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
            var address = _connection.AddressFactory.Create(_connection.Storage, RelativeUri.Data.Spaces, UriParameter.AccountId, currentAccount.Id.ToString());
            var spaces = await _connection.Client.Get<IEnumerable<Space>>(address);
            return spaces.FirstOrDefault(s => s.Name == spaceName);
        }
    }
}
