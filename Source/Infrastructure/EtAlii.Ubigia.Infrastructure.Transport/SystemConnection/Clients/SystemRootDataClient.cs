// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal class SystemRootDataClient : SystemSpaceClientBase, IRootDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemRootDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        /// <inheritdoc />
        public async Task<Root> Add(string name)
        {
            var root = new Root
            {
                Name = name,
            };
            var result = await _infrastructure.Roots
                .Add(Connection.Space.Id, root)
                .ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public Task Remove(Guid id)
        {
            return _infrastructure.Roots.Remove(Connection.Space.Id, id);
        }

        /// <inheritdoc />
        public async Task<Root> Change(Guid rootId, string rootName)
        {
            var root = new Root
            {
                Id = rootId,
                Name = rootName,
            };

            var result = await _infrastructure.Roots
                .Update(Connection.Space.Id, rootId, root)
                .ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<Root> Get(string rootName)
        {
            var result = await _infrastructure.Roots
                .Get(Connection.Space.Id, rootName)
                .ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<Root> Get(Guid rootId)
        {
            var result = await _infrastructure.Roots
                .Get(Connection.Space.Id, rootId)
                .ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Root> GetAll()
        {
            return _infrastructure.Roots
                .GetAll(Connection.Space.Id);
        }
    }
}
