
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Structure.Workflow;

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
