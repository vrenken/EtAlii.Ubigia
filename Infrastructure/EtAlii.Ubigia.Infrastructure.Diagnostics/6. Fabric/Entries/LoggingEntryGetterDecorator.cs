namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.xTechnology.Logging;

    internal class LoggingEntryGetterDecorator : IEntryGetter
    {
        private readonly ILogger _logger;
        private readonly IEntryGetter _entryGetter;

        public LoggingEntryGetterDecorator(ILogger logger, IEntryGetter entryGetter)
        {
            _logger = logger;
            _entryGetter = entryGetter;
        }

        public IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations)
        {
            return _entryGetter.Get(identifiers, entryRelations);
        }

        public Entry Get(Identifier identifier, EntryRelation entryRelations)
        {
            _logger.Verbose("Getting entry: {0}", identifier.ToTimeString());

            return _entryGetter.Get(identifier, entryRelations);
        }


        public IEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations)
        {
            _logger.Verbose("Getting entries for: {0}", identifier.ToTimeString());

            return _entryGetter.GetRelated(identifier, entriesWithRelation, entryRelations);
        }
    }
}