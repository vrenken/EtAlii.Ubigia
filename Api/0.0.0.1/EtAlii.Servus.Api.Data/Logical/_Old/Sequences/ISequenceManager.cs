namespace EtAlii.Servus.Api.Helpers
{
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public interface ISequenceManager
    {
        Identifier Create(Identifier parentIdentifier, string sequenceName);
        void Add(Identifier sequenceIdentifier, Identifier itemIdentifier);
        void Add(Identifier sequenceIdentifier, IEnumerable<Identifier> itemIdentifiers);
    }
}
