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
        private readonly IFunctionalContext _functionalContext;

        public SystemRootDataClient(IFunctionalContext functionalContext)
        {
            _functionalContext = functionalContext;
        }

        /// <inheritdoc />
        public async Task<Root> Add(string name, RootType rootType)
        {
            var root = new Root
            {
                Name = name,
                Type = rootType,
            };
            var result = await _functionalContext.Roots
                .Add(Connection.Space.Id, root)
                .ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public Task Remove(Guid id)
        {
            return _functionalContext.Roots.Remove(Connection.Space.Id, id);
        }

        /// <inheritdoc />
        public async Task<Root> Change(Guid rootId, string rootName)
        {
            var root = new Root
            {
                Id = rootId,
                Name = rootName,
            };

            var result = await _functionalContext.Roots
                .Update(Connection.Space.Id, rootId, root)
                .ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<Root> Get(string rootName)
        {
            var result = await _functionalContext.Roots
                .Get(Connection.Space.Id, rootName)
                .ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<Root> Get(Guid rootId)
        {
            var result = await _functionalContext.Roots
                .Get(Connection.Space.Id, rootId)
                .ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Root> GetAll()
        {
            return _functionalContext.Roots
                .GetAll(Connection.Space.Id);
        }
    }
}
