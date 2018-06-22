namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// A stubbed data client that can be used to manage roots.
    /// </summary>
    public class RootDataClientStub : IRootDataClient 
    {
        public async Task<Root> Add(string name)
        {
            return await Task.FromResult<Root>(null);
        }

        public async Task Remove(Guid id)
        {
            await Task.Run(() => { });
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            return await Task.FromResult<Root>(null);
        }

        public async Task<Root> Get(string rootName)
        {
            return await Task.FromResult<Root>(null);
        }

        public async Task<Root> Get(Guid rootId)
        {
            return await Task.FromResult<Root>(null);
        }

        public async Task<IEnumerable<Root>> GetAll()
        {
            return await Task.FromResult<IEnumerable<Root>>(null);
        }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }
    }
}
