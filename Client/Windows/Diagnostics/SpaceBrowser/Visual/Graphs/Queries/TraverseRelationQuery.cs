
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System;

    public class TraverseRelationQuery : QueryBase<TraverseRelationQueryHandler, Identifier>
    {
        public IReadOnlyEntry Entry { get; private set; }
        public Func<IReadOnlyEntry, Relation> Selector { get; private set; }

        public TraverseRelationQuery(IReadOnlyEntry entry, Func<IReadOnlyEntry, Relation> selector)
        {
            Entry = entry;
            Selector = selector;
        }
    }
}
