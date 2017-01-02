namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.MicroContainer;
    using System;
    using System.Collections.Generic;

    public class WebApiRootDataClient : WebApiDataClientBase<IDataConnection>, IRootDataClient 
    {
        public WebApiRootDataClient(Container container, IAddressFactory addressFactory, IInfrastructureClient client)
            : base(container, addressFactory, client)
        {
        }

        public Root Add(string name)
        {
            var root = new Root
            {
                Name = name,
            };

            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString());
            root = Client.Post<Root>(address, root);
            return root;
        }
        
        public void Remove(Guid id)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootId, id.ToString());
            Client.Delete(address);
        }

        public Root Change(Guid rootId, string rootName)
        {
            var root = new Root
            {
                Id = rootId,
                Name = rootName,
            };

            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootId, rootId.ToString());
            root = Client.Put(address, root);
            return root;
        }

        public Root Get(string rootName)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootName, rootName.ToString());
            var root = Client.Get<Root>(address);
            return root;
        }

        public Root Get(Guid rootId)
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootId, rootId.ToString());
            var root = Client.Get<Root>(address);
            return root;
        }

        public IEnumerable<Root> GetAll()
        {
            var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString());
            var roots = Client.Get<IEnumerable<Root>>(address);
            return roots;
        }
    }
}
