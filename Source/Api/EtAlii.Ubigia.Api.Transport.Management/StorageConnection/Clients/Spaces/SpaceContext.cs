// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class SpaceContext : StorageClientContextBase<ISpaceDataClient, ISpaceNotificationClient>, ISpaceContext
    {
        public SpaceContext(
            ISpaceNotificationClient notifications, 
            ISpaceDataClient data) 
            : base(notifications, data)
        {
        }
        public async Task<Space> Add(Guid accountId, string spaceName, SpaceTemplate spaceTemplate)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            return await Data.Add(accountId, spaceName, spaceTemplate).ConfigureAwait(false);
        }

        public async Task Remove(Guid spaceId)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            await Data.Remove(spaceId).ConfigureAwait(false);
        }

        public async Task<Space> Change(Guid spaceId, string spaceName)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Change(spaceId, spaceName).ConfigureAwait(false);
        }

        public async Task<Space> Get(Guid accountId, string spaceName)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Get(accountId, spaceName).ConfigureAwait(false);
        }

        public async Task<Space> Get(Guid spaceId)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return await Data.Get(spaceId).ConfigureAwait(false);
        }

        public IAsyncEnumerable<Space> GetAll(Guid accountId)
        {
            if (!Connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return Data.GetAll(accountId);
        }
    }
}
