// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// A stubbed data client that can be used to manage roots.
    /// </summary>
    public sealed class RootDataClientStub : IRootDataClient
    {
        /// <inheritdoc />
        public Task<Root> Add(string name, RootType rootType)
        {
            return Task.FromResult<Root>(null);
        }

        /// <inheritdoc />
        public Task Remove(Guid id)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<Root> Change(Guid rootId, string rootName, RootType rootType)
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
        public IAsyncEnumerable<Root> GetAll()
        {
            return AsyncEnumerable.Empty<Root>();
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
