namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Structure.Workflow;

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
