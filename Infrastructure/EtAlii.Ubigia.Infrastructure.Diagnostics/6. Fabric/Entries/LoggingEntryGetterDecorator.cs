namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using Serilog;

    internal class LoggingEntryGetterDecorator : IEntryGetter
    {
        private readonly ILogger _logger = Log.ForContext<IEntryGetter>();
        private readonly IEntryGetter _decoree;

        public LoggingEntryGetterDecorator(IEntryGetter decoree)
        {
            _decoree = decoree;
        }

        public IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations)
        {
            return _decoree.Get(identifiers, entryRelations);
        }

        public Entry Get(Identifier identifier, EntryRelation entryRelations)
        {
            _logger.Verbose("Getting entry: {identifier}", identifier.ToTimeString());

            return _decoree.Get(identifier, entryRelations);
        }


        public IEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations)
        {
            _logger.Verbose("Getting entries for: {identifier}", identifier.ToTimeString());

            return _decoree.GetRelated(identifier, entriesWithRelation, entryRelations);
        }
    }
}