namespace EtAlii.Servus.Api.Helpers
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class CollectionManager : ICollectionManager
    {
        private readonly IDataConnection _connection;

        public CollectionManager(IDataConnection connection)
        {
            _connection = connection;
        }

        public Identifier Create(Identifier parentIdentifier, string collectionName)
        {
            throw new NotImplementedException();
            
            // Check parent for updates that already contain a child with collectionName.
            // Check parent for downdates that already contain a child with collectionName.
        }

        public void Add(Identifier collectionIdentifier, Identifier itemIdentifier)
        {
            Add(collectionIdentifier, new Identifier[] { itemIdentifier });
        }

        public void Add(Identifier collectionIdentifier, IEnumerable<Identifier> itemIdentifiers)
        {
            throw new NotImplementedException();
        }

        public void Remove(Identifier collectionIdentifier, Identifier itemIdentifier)
        {
            Remove(collectionIdentifier, new Identifier[] { itemIdentifier });
        }

        public void Remove(Identifier collectionIdentifier, IEnumerable<Identifier> itemIdentifiers)
        {
            throw new NotImplementedException();
        }
    }
}
