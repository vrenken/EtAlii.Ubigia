
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.Collections.Generic;

    public class TraverseRelationsQuery : QueryBase<TraverseRelationsQueryHandler, Identifier>
    {
        public IReadOnlyEntry Entry { get; private set; }
        public Func<IReadOnlyEntry, IEnumerable<Relation>> Selector { get; private set; }

        public TraverseRelationsQuery(IReadOnlyEntry entry, Func<IReadOnlyEntry, IEnumerable<Relation>> selector)
        {
            Entry = entry;
            Selector = selector;
        }
    }
}
