
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Workflow;

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
