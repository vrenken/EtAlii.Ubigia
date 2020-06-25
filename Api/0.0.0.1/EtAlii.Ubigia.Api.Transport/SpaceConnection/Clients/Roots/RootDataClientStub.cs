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
        /// <inheritdoc />
        public Task<Root> Add(string name)
        {
            return Task.FromResult<Root>(null);
        }

        /// <inheritdoc />
        public Task Remove(Guid id)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<Root> Change(Guid rootId, string rootName)
        {
            return Task.FromResult<Root>(null);
        }

        /// <inheritdoc />
        public Task<Root> Get(string rootName)
        {
            return Task.FromResult<Root>(null);
        }

        /// <inheritdoc />
        public Task<Root> Get(Guid rootId)
        {
            return Task.FromResult<Root>(null);
        }

        /// <inheritdoc />
        public Task<IEnumerable<Root>> GetAll()
        {
            return Task.FromResult<IEnumerable<Root>>(null);
        }

        /// <inheritdoc />
        public Task Connect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Disconnect()
        {
            return Task.CompletedTask;
        }
    }
}
