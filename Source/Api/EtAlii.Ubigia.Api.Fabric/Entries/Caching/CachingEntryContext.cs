// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class CachingEntryContext : IEntryContext
    {
        private readonly IEntryCacheChangeHandler _changeHandler;
        private readonly IEntryCacheGetHandler _getHandler;
        private readonly IEntryCacheGetRelatedHandler _getRelatedHandler;
        private readonly IEntryCacheContextProvider _contextProvider;

        public CachingEntryContext(
            IEntryCacheContextProvider contextProvider,
            IEntryCacheChangeHandler changeHandler,
            IEntryCacheGetHandler getHandler,
            IEntryCacheGetRelatedHandler getRelatedHandler)
        {
            _changeHandler = changeHandler;
            _getHandler = getHandler;
            _getRelatedHandler = getRelatedHandler;
            _contextProvider = contextProvider;
        }

        public async Task<IEditableEntry> Prepare()
        {
            return await _contextProvider.Context
                .Prepare()
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            return await _changeHandler
                .Handle(entry, scope)
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope)
        {
            return await _getHandler
                .Handle(root.Identifier, scope)
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Get(Identifier identifier, ExecutionScope scope)
        {
            return await _getHandler
                .Handle(identifier, scope)
                .ConfigureAwait(false);
        }

        public IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope)
        {
            return _getHandler
                .Handle(identifiers, scope);
        }

        public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelations relations, ExecutionScope scope)
        {
            return _getRelatedHandler
                .Handle(identifier, relations, scope);
        }
    }
}
