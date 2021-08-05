// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal partial class SystemAuthenticationDataClient
    {
        /// <inheritdoc />
        public async Task<Space> GetSpace(ISpaceConnection connection)
        {
            if (connection.Space != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var space = await GetSpace(connection.Account, connection.Options.Space).ConfigureAwait(false);
            if (space == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToSpace);
            }

            return space;
        }

        private Task<Space> GetSpace(Account currentAccount, string spaceName)
        {
            var space = _infrastructure.Spaces.Get(currentAccount.Id, spaceName);
            return Task.FromResult(space);
        }
    }
}
