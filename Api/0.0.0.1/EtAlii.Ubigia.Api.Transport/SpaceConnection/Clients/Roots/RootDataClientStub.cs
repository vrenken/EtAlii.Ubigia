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
        public Task<Root> Add(string name)
        {
            return Task.FromResult<Root>(null);
        }

        public Task Remove(Guid id)
        {
            return Task.CompletedTask;
        }

        public Task<Root> Change(Guid rootId, string rootName)
        {
            return Task.FromResult<Root>(null);
        }

        public Task<Root> Get(string rootName)
        {
            return Task.FromResult<Root>(null);
        }

        public Task<Root> Get(Guid rootId)
        {
            return Task.FromResult<Root>(null);
        }

        public Task<IEnumerable<Root>> GetAll()
        {
            return Task.FromResult<IEnumerable<Root>>(null);
        }

        public Task Connect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }
    }
}
