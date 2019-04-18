namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;

    internal class EntryPreparer : IEntryPreparer
    {
        private readonly ILogicalContext _context;

        public EntryPreparer(ILogicalContext context)
        {
            _context = context;
        }

        public Entry Prepare(Guid spaceId)
        {
            Identifier previousHeadIdentifier;
            var currentHeadidentifier = _context.Identifiers.GetNextHead(spaceId, out previousHeadIdentifier);

            //var relation = Relation.NewRelation(previousHeadIdentifier)
            var entry = Entry.NewEntry(currentHeadidentifier);//, relation)

            _context.Entries.Store(entry);

            return entry;
        }

        public Entry Prepare(Guid spaceId, Identifier id)
        {
            var entry = Entry.NewEntry(id, Relation.None);

            _context.Entries.Store(entry);

            return entry;
        }
    }
}