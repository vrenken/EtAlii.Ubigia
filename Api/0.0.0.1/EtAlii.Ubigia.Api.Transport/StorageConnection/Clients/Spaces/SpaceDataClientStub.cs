namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class SpaceDataClientStub : ISpaceDataClient 
    {
        /// <inheritdoc />
        public Task <Space> Add(Guid accountId, string spaceName, SpaceTemplate template)
        {
            return Task.FromResult<Space>(null);
        }

        /// <inheritdoc />
        public Task Remove(Guid spaceId)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<Space> Change(Guid spaceId, string spaceName)
        {
            return Task.FromResult<Space>(null);
        }

        /// <inheritdoc />
        public Task<Space> Get(Guid accountId, string spaceName)
        {
            return Task.FromResult<Space>(null);
        }

        /// <inheritdoc />
        public Task<Space> Get(Guid spaceId)
        {
            return Task.FromResult<Space>(null);
        }

        /// <inheritdoc />
        public Task<IEnumerable<Space>> GetAll(Guid accountId)
        {
            return Task.FromResult<IEnumerable<Space>>(null);
        }

        /// <inheritdoc />
        public Task Connect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }
    }
}
