namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class GrpcAuthenticationDataClient : GrpcClientBase, IAuthenticationDataClient<IGrpcSpaceTransport>
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
            // TODO: GRPC
            var space = await Task.FromResult<Api.Space>(null);
            //var space = await _invoker.Invoke<Space>(_spaceConnection, GrpcHub.Space, "GetForAuthenticationToken", spaceName);
			if (space == null)
			{
				string message = $"Unable to connect to the the specified space ({spaceName})";
				throw new UnauthorizedInfrastructureOperationException(message);
			}
			return space;// s.FirstOrDefault(s => s.Name == spaceName);
        }
    }
}
