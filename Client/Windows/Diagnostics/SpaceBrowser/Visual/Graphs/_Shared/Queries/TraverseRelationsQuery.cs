
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.Collections.Generic;

    public class TraverseRelationsQuery : QueryBase<ITraverseRelationsQueryHandler, Identifier>
    {
        public IReadOnlyEntry Entry { get; }
        public Func<IReadOnlyEntry, IEnumerable<Relation>> Selector { get; }

        public TraverseRelationsQuery(IReadOnlyEntry entry, Func<IReadOnlyEntry, IEnumerable<Relation>> selector)
        {
            Entry = entry;
            Selector = selector;
        }
    }
}
