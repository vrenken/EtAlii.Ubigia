namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using Serilog;

    internal class LoggingEntryPreparerDecorator : IEntryPreparer
    {
        private readonly ILogger _logger = Log.ForContext<IEntryPreparer>();
        private readonly IEntryPreparer _entryPreparer;

        public LoggingEntryPreparerDecorator(IEntryPreparer entryPreparer)
        {
            _entryPreparer = entryPreparer;
        }

        public Task<Entry> Prepare(Guid spaceId, Identifier id)
        {
            _logger.Verbose("Preparing entry for space: {spaceId} (Id: {identifier})", spaceId, id);
            return _entryPreparer.Prepare(spaceId);
        }

        public Task<Entry> Prepare(Guid spaceId)
        {
            _logger.Verbose("Preparing entry for space: {spaceId}", spaceId);
            return _entryPreparer.Prepare(spaceId);
        }
    }
}