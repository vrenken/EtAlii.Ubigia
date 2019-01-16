
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;

    public class FindEntryOnGraphQuery : QueryBase<IFindEntryOnGraphQueryHandler, IReadOnlyEntry>
    {
        public Identifier Identifier { get; }

        public FindEntryOnGraphQuery(Identifier identifier)
        {
            Identifier = identifier;
        }

        public FindEntryOnGraphQuery(Relation relation)
        {
            Identifier = relation.Id;
        }
    }
}
