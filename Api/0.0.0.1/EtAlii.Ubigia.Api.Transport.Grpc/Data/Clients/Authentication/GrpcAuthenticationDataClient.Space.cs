namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

    public partial class GrpcAuthenticationDataClient
    {
        public async Task<Api.Space> GetSpace(ISpaceConnection connection)
        {
            if (connection.Space != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var space = await GetSpace(connection.Configuration.Space);
            if (space == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToSpace);
            }

            return space;
        }

        private async Task<Api.Space> GetSpace(string spaceName)
        {
            var request = new SpaceSingleRequest {Name = spaceName};
            var response = await _spaceClient.GetSingleAsync(request, _connection.Transport.AuthenticationHeaders);
            //var space = await _invoker.Invoke<Space>(_spaceConnection, GrpcHub.Space, "GetForAuthenticationToken", spaceName);

            var space = response.Space.ToLocal();
            if (space == null)
			{
				var message = $"Unable to connect to the the specified space ({spaceName})";
				throw new UnauthorizedInfrastructureOperationException(message);
			}
			return space;// s.FirstOrDefault(s => s.Name == spaceName);
        }
    }
}
