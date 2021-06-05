namespace EtAlii.Ubigia.Api.Transport.Management.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Rest;

    internal sealed class RestSpaceDataClient : RestClientBase, ISpaceDataClient
    {
        public async Task<Space> Add(Guid accountId, string spaceName, SpaceTemplate template)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Spaces, UriParameter.SpaceTemplate, template.Name);

            var space = new Space
            {
                Name = spaceName,
                AccountId = accountId,
            };

            space = await Connection.Client.Post(address, space).ConfigureAwait(false);
            return space;
        }

        public async Task Remove(Guid spaceId)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Spaces, UriParameter.SpaceId, spaceId.ToString());
            await Connection.Client.Delete(address).ConfigureAwait(false);
        }

        public async Task<Space> Change(Guid spaceId, string spaceName)
        {
            var space = new Space
            {
                Id = spaceId,
//              AccountId = accountId,
                Name = spaceName,
            };

            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Spaces, UriParameter.SpaceId, spaceId.ToString());
            space = await Connection.Client.Put(address, space).ConfigureAwait(false);
            return space;
        }

        public async Task<Space> Get(Guid accountId, string spaceName)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Spaces, UriParameter.AccountId, accountId.ToString(), UriParameter.SpaceName, spaceName);
            var space = await Connection.Client.Get<Space>(address).ConfigureAwait(false);
            return space;
        }

        public async Task<Space> Get(Guid spaceId)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Spaces, UriParameter.SpaceId, spaceId.ToString());
            var space = await Connection.Client.Get<Space>(address).ConfigureAwait(false);
            return space;
        }

        public async IAsyncEnumerable<Space> GetAll(Guid accountId)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Spaces, UriParameter.AccountId, accountId.ToString());
            var result = await Connection.Client.Get<IEnumerable<Space>>(address).ConfigureAwait(false);
            foreach (var item in result)
            {
                yield return item;
            }
        }
    }
}
