// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;
    using Space = EtAlii.Ubigia.Space;

    public partial class GrpcAuthenticationDataClient
    {
        public async Task<Space> GetSpace(ISpaceConnection connection)
        {
            if (connection.Space != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var transport = ((IGrpcSpaceConnection) connection).Transport;
            var space = await GetSpace(connection.Configuration.Space, transport).ConfigureAwait(false);
            if (space == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToSpace);
            }

            return space;
        }

        private async Task<Space> GetSpace(string spaceName, IGrpcTransport transport)
        {
            var metadata = new Metadata { transport.AuthenticationHeader };
            var request = new SpaceSingleRequest {Name = spaceName};
            var response = await _spaceClient.GetSingleAsync(request, metadata);

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
