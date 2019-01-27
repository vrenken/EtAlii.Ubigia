﻿namespace EtAlii.Ubigia.Api.Transport.Grpc
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

            var transport = ((IGrpcSpaceConnection) connection).Transport;
            var space = await GetSpace(connection.Configuration.Space, transport);
            if (space == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToSpace);
            }

            return space;
        }

        private async Task<Api.Space> GetSpace(string spaceName, IGrpcTransport transport)
        {
            var request = new SpaceSingleRequest {Name = spaceName};
            var response = await _spaceClient.GetSingleAsync(request, transport.AuthenticationHeaders);
            
            var space = response.Space.ToLocal();
            if (space == null)
			{
				var message = $"Unable to connect to the the specified space ({spaceName})";
				throw new UnauthorizedInfrastructureOperationException(message);
			}
			return space;
        }
    }
}
