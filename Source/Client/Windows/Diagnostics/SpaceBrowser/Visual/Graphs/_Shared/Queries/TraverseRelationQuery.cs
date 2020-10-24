
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using EtAlii.xTechnology.Workflow;

    public class TraverseRelationQuery : QueryBase<TraverseRelationQueryHandler, Identifier>
    {
        public IReadOnlyEntry Entry { get; }
        public Func<IReadOnlyEntry, Relation> Selector { get; }

        public TraverseRelationQuery(IReadOnlyEntry entry, Func<IReadOnlyEntry, Relation> selector)
        {
            Entry = entry;
            Selector = selector;
        }
    }
}
