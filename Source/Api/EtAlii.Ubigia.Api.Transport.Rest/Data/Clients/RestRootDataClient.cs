// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class RestRootDataClient : RestClientBase, IRootDataClient<IRestSpaceTransport>
    {
        public async Task<Root> Add(string name)
        {
            var root = new Root
            {
                Name = name,
            };

            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString());
            root = await Connection.Client.Post(address, root).ConfigureAwait(false);
            return root;
        }

        public async Task Remove(Guid id)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootId, id.ToString());
            await Connection.Client.Delete(address).ConfigureAwait(false);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            var root = new Root
            {
                Id = rootId,
                Name = rootName,
            };

            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootId, rootId.ToString());
            root = await Connection.Client.Put(address, root).ConfigureAwait(false);
            return root;
        }

        public async Task<Root> Get(string rootName)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootId, rootName, UriParameter.IdType, UriParameter.RootName);
            var root = await Connection.Client.Get<Root>(address).ConfigureAwait(false);
            return root;
        }

        public async Task<Root> Get(Guid rootId)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootId, rootId.ToString(), UriParameter.IdType, UriParameter.RootId);
            var root = await Connection.Client.Get<Root>(address).ConfigureAwait(false);
            return root;
        }

        public async IAsyncEnumerable<Root> GetAll()
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString());
            var result = await Connection.Client.Get<IEnumerable<Root>>(address).ConfigureAwait(false);
            foreach (var item in result)
            {
                yield return item;
            }
        }
    }
}
