namespace EtAlii.Servus.Api.Helpers
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class SequenceManager : ISequenceManager
    {
        private readonly IDataConnection _connection;

        public SequenceManager(IDataConnection connection)
        {
            _connection = connection;
        }

        public Identifier Create(Identifier parentIdentifier, string sequenceName)
        {
            throw new NotImplementedException();
        }

        public void Add(Identifier sequenceIdentifier, Identifier itemIdentifier)
        {
            Add(sequenceIdentifier, new Identifier[] { itemIdentifier });
        }

        public void Add(Identifier sequenceIdentifier, IEnumerable<Identifier> itemIdentifiers)
        {
            throw new NotImplementedException();
        }
    }
}
