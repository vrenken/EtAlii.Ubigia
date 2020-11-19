﻿namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;

    internal class EntryPreparer : IEntryPreparer
    {
        private readonly ILogicalContext _context;

        public EntryPreparer(ILogicalContext context)
        {
            _context = context;
        }

        public async Task<Entry> Prepare(Guid spaceId)
        {
            // ReSharper disable once NotAccessedVariable
            var head = await _context.Identifiers.GetNextHead(spaceId);
            // ReSharper disable once UnusedVariable
            var previousHeadIdentifier = head.PreviousHeadIdentifier; // We don't seem to wire up the head in our preparation. This feels incorrect.

            //var relation = Relation.NewRelation(previousHeadIdentifier)
            var entry = Entry.NewEntry(head.NextHeadIdentifier);//, relation)

            _context.Entries.Store(entry);

            return entry;
        }

        public Task<Entry> Prepare(Guid spaceId, Identifier id)
        {
            var entry = Entry.NewEntry(id, Relation.None);

            _context.Entries.Store(entry);

            return Task.FromResult(entry);
        }
    }
}