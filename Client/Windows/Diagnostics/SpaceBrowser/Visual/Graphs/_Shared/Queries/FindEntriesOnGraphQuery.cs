
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;
    using System.Linq;
    using System.Collections.Generic;

    public class FindEntriesOnGraphQuery : QueryBase<IFindEntriesOnGraphQueryHandler, IReadOnlyEntry>
    {
        public IEnumerable<Identifier> Identifiers { get; }

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
