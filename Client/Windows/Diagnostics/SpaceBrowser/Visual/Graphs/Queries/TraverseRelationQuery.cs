
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
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
