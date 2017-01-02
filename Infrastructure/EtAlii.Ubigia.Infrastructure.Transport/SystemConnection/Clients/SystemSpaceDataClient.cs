namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
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
            // TODO: This is where the template functionality should continue.
            var space = new Space
            {
                Name = spaceName,
                AccountId = accountId,
            };

            space = _infrastructure.Spaces.Add(space, template);
            return await Task.FromResult(space);
        }

        public async Task Remove(Guid spaceId)
        {
            await Task.Run(() =>
            {
                _infrastructure.Spaces.Remove(spaceId);
            });
        }

        public async Task<Space> Change(Guid spaceId, string spaceName)
        {
            var space = new Space
            {
                Id = spaceId,
                Name = spaceName,
            };

            space = _infrastructure.Spaces.Update(spaceId, space);
            return await Task.FromResult(space);
        }

        public async Task<Space> Get(Guid accountId, string spaceName)
        {
            var space = _infrastructure.Spaces.Get(accountId, spaceName);
            return await Task.FromResult(space);
        }

        public async Task<Space> Get(Guid spaceId)
        {
            var space = _infrastructure.Spaces.Get(spaceId);
            return await Task.FromResult(space);
        }

        public async Task<IEnumerable<Space>> GetAll(Guid accountId)
        {
            var spaces = _infrastructure.Spaces.GetAll(accountId);
            return await Task.FromResult(spaces);
        }
    }
}
