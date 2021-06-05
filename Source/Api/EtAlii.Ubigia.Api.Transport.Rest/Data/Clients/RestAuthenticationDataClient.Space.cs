namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System.Threading.Tasks;

    public partial class RestAuthenticationDataClient
    {
        public async Task<Space> GetSpace(ISpaceConnection connection)
        {
            if (connection.Space != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var space = await GetSpace(connection.Configuration.Space).ConfigureAwait(false);
            if (space == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToSpace);
            }

            return space;
        }

        private Task<Space> GetSpace(string spaceName)
        {
	        //var address = _connection.AddressFactory.Create(_connection.Storage, RelativeUri.Data.Spaces, UriParameter.AccountId, currentAccount.Id.ToString())
	        //var spaces = await _connection.Client.Get<IEnumerable<Space>>(address)
	        //return spaces.FirstOrDefault(s => s.Name == spaceName)

	        var address = _connection.AddressFactory.Create(_connection.Transport, RelativeDataUri.Spaces, UriParameter.SpaceName, spaceName, UriParameter.AuthenticationToken);
	        return _connection.Client.Get<Space>(address);
        }
	}
}
