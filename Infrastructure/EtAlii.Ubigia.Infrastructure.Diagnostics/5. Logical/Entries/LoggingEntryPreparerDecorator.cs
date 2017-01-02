namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Logging;

    internal class LoggingEntryPreparerDecorator : IEntryPreparer
    {
        private readonly ILogger _logger;
        private readonly IEntryPreparer _entryPreparer;

        public LoggingEntryPreparerDecorator(ILogger logger, IEntryPreparer entryPreparer)
        {
            _logger = logger;
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