namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;

    internal class EntryPreparer : IEntryPreparer
    {
        private readonly ILogicalContext _context;

        public EntryPreparer(ILogicalContext context)
        {
            _context = context;
        }

        public Entry Prepare(Guid spaceId)
        {
            // ReSharper disable once NotAccessedVariable
            Identifier previousHeadIdentifier; // We don't seem to wire up the head in our preparation. This feels incorrect.
            var currentHeadIdentifier = _context.Identifiers.GetNextHead(spaceId, out previousHeadIdentifier);

            //var relation = Relation.NewRelation(previousHeadIdentifier)
            var entry = Entry.NewEntry(currentHeadIdentifier);//, relation)

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