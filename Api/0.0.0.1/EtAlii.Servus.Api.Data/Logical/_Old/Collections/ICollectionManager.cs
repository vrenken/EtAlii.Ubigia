namespace EtAlii.Servus.Api.Helpers
{
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public interface ICollectionManager
    {
        Identifier Create(Identifier parentIdentifier, string collectionName);
        void Add(Identifier collectionIdentifier, Identifier itemIdentifier);
        void Add(Identifier collectionIdentifier, IEnumerable<Identifier> itemIdentifiers);
        void Remove(Identifier collectionIdentifier, Identifier itemIdentifier);
        void Remove(Identifier collectionIdentifier, IEnumerable<Identifier> itemIdentifiers);
    }
}
