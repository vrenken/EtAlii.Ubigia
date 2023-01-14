namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;
    using EtAlii.xTechnology.MicroContainer;
    using System;
    using System.Collections.Generic;

    public class LocalRootDataClient : LocalDataClientBase<IDataConnection>, IRootDataClient 
    {
        public LocalRootDataClient(Container container)
            : base(container)
        {
        }

        public Root Add(string name)
        {
            throw new System.NotImplementedException();

            //var root = new Root
            //[
            //    Name = name,
            //]
            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString())
            //root = Infrastructure.Post<Root>(address, root)
            //return root
        }
        
        public void Remove(Guid id)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootId, id.ToString())
            //Infrastructure.Delete(address)
        }

        public Root Change(Guid rootId, string rootName)
        {
            throw new System.NotImplementedException();

            //var root = new Root
            //[
            //    Id = rootId,
            //    Name = rootName,
            //]
            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootId, rootId.ToString())
            //root = Infrastructure.Put(address, root)
            //return root
        }

        public Root Get(string rootName)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootName, rootName.ToString())
            //var root = Infrastructure.Get<Root>(address)
            //return root
        }

        public Root Get(Guid rootId)
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString(), UriParameter.RootId, rootId.ToString())
            //var root = Infrastructure.Get<Root>(address)
            //return root
        }

        public IEnumerable<Root> GetAll()
        {
            throw new System.NotImplementedException();

            //var address = AddressFactory.Create(Connection.Storage, RelativeUri.Roots, UriParameter.SpaceId, Connection.Space.Id.ToString())
            //var roots = Infrastructure.Get<IEnumerable<Root>>(address)
            //return roots
        }
    }
}
