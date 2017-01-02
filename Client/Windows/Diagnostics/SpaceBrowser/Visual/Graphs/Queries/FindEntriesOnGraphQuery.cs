
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System.Linq;
    using System.Collections.Generic;

    public class FindEntriesOnGraphQuery : QueryBase<FindEntriesOnGraphQueryHandler, IReadOnlyEntry>
    {
        public IEnumerable<Identifier> Identifiers { get; private set; }

        public FindEntriesOnGraphQuery(IEnumerable<Identifier> identifiers)
        {
            Identifiers = identifiers;
        }

        public FindEntriesOnGraphQuery(IEnumerable<Relation> relations)
        {
            Identifiers = relations.Select(relation => relation.Id);
        }
    }
}
