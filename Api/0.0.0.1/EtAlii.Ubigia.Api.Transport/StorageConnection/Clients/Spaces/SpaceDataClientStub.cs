namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class SpaceDataClientStub : ISpaceDataClient 
    {
        public Task <Space> Add(Guid accountId, string spaceName, SpaceTemplate template)
        {
            return Task.FromResult<Space>(null);
        }

        public Task Remove(Guid spaceId)
        {
            return Task.CompletedTask;
        }

        public Task<Space> Change(Guid spaceId, string spaceName)
        {
            return Task.FromResult<Space>(null);
        }

        public Task<Space> Get(Guid accountId, string spaceName)
        {
            return Task.FromResult<Space>(null);
        }

        public Task<Space> Get(Guid spaceId)
        {
            return Task.FromResult<Space>(null);
        }

        public Task<IEnumerable<Space>> GetAll(Guid accountId)
        {
            return Task.FromResult<IEnumerable<Space>>(null);
        }

        public Task Connect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }
    }
}
