namespace EtAlii.Ubigia.Api.Management
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public sealed class SpaceDataClientStub : ISpaceDataClient 
    {
        public async Task <Space> Add(Guid accountId, string spaceName, SpaceTemplate template)
        {
            return await Task.FromResult<Space>(null);
        }

        public async Task Remove(Guid spaceId)
        {
            await Task.Run(() => { });
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

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Task.Run(() => { });
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Task.Run(() => { });
        }
    }
}
