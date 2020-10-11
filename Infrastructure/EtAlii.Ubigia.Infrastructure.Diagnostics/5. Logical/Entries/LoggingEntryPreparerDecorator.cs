namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
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

        public Entry Prepare(Guid spaceId, Identifier id)
        {
            _logger.Verbose("Preparing entry for space: {0} (Id: {1})", spaceId, id);
            return _entryPreparer.Prepare(spaceId);
        }

        public Entry Prepare(Guid spaceId)
        {
            _logger.Verbose("Preparing entry for space: {0}", spaceId);
            return _entryPreparer.Prepare(spaceId);
        }
    }
}