namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    internal sealed class WebApiSpaceDataClient : WebApiClientBase, ISpaceDataClient
    {
        public async Task<Space> Add(Guid accountId, string spaceName, SpaceTemplate template)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.ApiRest + RelativeUri.Data.Spaces, UriParameter.SpaceTemplate, template.Name);

            var space = new Space
            {
                Name = spaceName,
                AccountId = accountId,
            };

            space = await Connection.Client.Post(address, space);
            return space;
        }

        public async Task Remove(Guid spaceId)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.ApiRest + RelativeUri.Data.Spaces, UriParameter.SpaceId, spaceId.ToString());
            await Connection.Client.Delete(address);
        }

        public async Task<Space> Change(Guid spaceId, string spaceName)
        {
            var space = new Space
            {
                Id = spaceId,
//              AccountId = accountId,
                Name = spaceName,
            };

            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.ApiRest + RelativeUri.Data.Spaces, UriParameter.SpaceId, spaceId.ToString());
            space = await Connection.Client.Put(address, space);
            return space;
        }

        public async Task<Space> Get(Guid accountId, string spaceName)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.ApiRest + RelativeUri.Data.Spaces, UriParameter.AccountId, accountId.ToString(), UriParameter.SpaceName, spaceName);
            var space = await Connection.Client.Get<Space>(address);
            return space;
        }

        public async Task<Space> Get(Guid spaceId)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.ApiRest + RelativeUri.Data.Spaces, UriParameter.SpaceId, spaceId.ToString());
            var space = await Connection.Client.Get<Space>(address);
            return space;
        }

        public async Task<IEnumerable<Space>> GetAll(Guid accountId)
        {
            var address = Connection.AddressFactory.Create(Connection.Storage, RelativeUri.ApiRest + RelativeUri.Data.Spaces, UriParameter.AccountId, accountId.ToString());
            var spaces = await Connection.Client.Get<IEnumerable<Space>>(address);
            return spaces;
        }
    }
}
