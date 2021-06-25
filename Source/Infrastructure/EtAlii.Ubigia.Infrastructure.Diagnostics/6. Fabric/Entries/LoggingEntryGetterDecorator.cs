// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Serilog;

    internal class LoggingEntryGetterDecorator : IEntryGetter
    {
        private readonly ILogger _logger = Log.ForContext<IEntryGetter>();
        private readonly IEntryGetter _decoree;

        public LoggingEntryGetterDecorator(IEntryGetter decoree)
        {
            _decoree = decoree;
        }

        public IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelations entryRelations)
        {
            return _decoree.Get(identifiers, entryRelations);
        }

        public Task<Entry> Get(Identifier identifier, EntryRelations entryRelations)
        {
            _logger.Verbose("Getting entry: {Identifier}", identifier.ToTimeString());

            return _decoree.Get(identifier, entryRelations);
        }


        public IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelations entriesWithRelation, EntryRelations entryRelations)
        {
            _logger.Verbose("Getting entries for: {Identifier}", identifier.ToTimeString());

            return _decoree.GetRelated(identifier, entriesWithRelation, entryRelations);
        }
    }
}
