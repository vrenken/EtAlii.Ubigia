namespace EtAlii.Servus.Api.Data
{
    using System.Collections;
    using EtAlii.Servus.Api.Data.Model;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    internal class UpdateEntryFactory
    {
        private readonly ProcessingContext _context;

        public UpdateEntryFactory(ProcessingContext context)
        {
            _context = context;
        }

        public IEditableEntry Create(IReadOnlyEntry entry)
        {
            var newEntry = _context.Connection.Entries.Prepare();
            newEntry.Type = entry.Type;
            newEntry.Downdate = Relation.NewRelation(entry.Id);
            _context.Connection.Entries.Change(newEntry);
            return newEntry;
        }
    }
}
