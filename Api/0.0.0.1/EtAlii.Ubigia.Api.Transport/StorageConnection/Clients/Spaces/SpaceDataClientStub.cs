namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class SpaceDataClientStub : ISpaceDataClient 
    {
        public async Task <Space> Add(Guid accountId, string spaceName, SpaceTemplate template)
        {
            return await Task.FromResult<Space>(null);
        }

        public async Task Remove(Guid spaceId)
        {
            await Task.CompletedTask;
        }

        public async Task<Space> Change(Guid spaceId, string spaceName)
        {
            return await Task.FromResult<Space>(null);
        }

        public async Task<Space> Get(Guid accountId, string spaceName)
        {
            return await Task.FromResult<Space>(null);
        }

        public async Task<Space> Get(Guid spaceId)
        {
            return await Task.FromResult<Space>(null);
        }

        public async Task<IEnumerable<Space>> GetAll(Guid accountId)
        {
            return await Task.FromResult<IEnumerable<Space>>(null);
        }

        public async Task Connect(IStorageConnection connection)
        {
            await Task.CompletedTask;
        }

        public async Task Disconnect(IStorageConnection connection)
        {
            await Task.CompletedTask;
        }
    }
}
