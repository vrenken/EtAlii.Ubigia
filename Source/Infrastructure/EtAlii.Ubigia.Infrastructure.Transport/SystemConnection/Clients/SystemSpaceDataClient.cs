// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal sealed class SystemSpaceDataClient : SystemStorageClientBase, ISpaceDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemSpaceDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public async Task<Space> Add(Guid accountId, string spaceName, SpaceTemplate template)
        {
            // Improve the space templating functionality by converting initialization to a script based approach.
            // This is where the template functionality should continue.
            // More details can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/95
            var space = new Space
            {
                Name = spaceName,
                AccountId = accountId,
            };

            space = await _infrastructure.Spaces
                .Add(space, template)
                .ConfigureAwait(false);
            return space;
        }

        public Task Remove(Guid spaceId)
        {
            _infrastructure.Spaces.Remove(spaceId);
            return Task.CompletedTask;
        }

        public Task<Space> Change(Guid spaceId, string spaceName)
        {
            var space = new Space
            {
                Id = spaceId,
                Name = spaceName,
            };

            space = _infrastructure.Spaces.Update(spaceId, space);
            return Task.FromResult(space);
        }

        public Task<Space> Get(Guid accountId, string spaceName)
        {
            var space = _infrastructure.Spaces.Get(accountId, spaceName);
            return Task.FromResult(space);
        }

        public Task<Space> Get(Guid spaceId)
        {
            var space = _infrastructure.Spaces.Get(spaceId);
            return Task.FromResult(space);
        }

        public IAsyncEnumerable<Space> GetAll(Guid accountId)
        {
            return _infrastructure.Spaces.GetAll(accountId);
        }
    }
}
