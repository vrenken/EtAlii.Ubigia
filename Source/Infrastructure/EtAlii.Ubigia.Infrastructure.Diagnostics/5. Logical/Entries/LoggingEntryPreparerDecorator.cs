// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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

        public Task<Entry> Prepare(Identifier id)
        {
            _logger.Verbose("Preparing entry for storage {StorageId} and  space {SpaceId} (Id: {Identifier})", id.Storage, id.Space, id);
            return _entryPreparer.Prepare(id.Storage, id.Space);
        }

        public Task<Entry> Prepare(Guid storageId, Guid spaceId)
        {
            _logger.Verbose("Preparing entry for storage {StorageId} and  space {SpaceId}", storageId, spaceId);
            return _entryPreparer.Prepare(storageId, spaceId);
        }
    }
}
