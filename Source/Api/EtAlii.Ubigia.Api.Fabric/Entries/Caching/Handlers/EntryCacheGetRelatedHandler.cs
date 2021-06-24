// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class EntryCacheGetRelatedHandler : IEntryCacheGetRelatedHandler
    {
        private readonly IEntryCacheHelper _cacheHelper;
        private readonly IEntryCacheGetHandler _entryGetHandler;
        private readonly IEntryCacheContextProvider _contextProvider;

        public EntryCacheGetRelatedHandler(
            IEntryCacheHelper cacheHelper,
            IEntryCacheGetHandler entryGetHandler,
            IEntryCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _entryGetHandler = entryGetHandler;
            _contextProvider = contextProvider;
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Handle(Identifier identifier, EntryRelation relations, ExecutionScope scope)
        {

            IAsyncEnumerable<IReadOnlyEntry> result;

            var entry = _cacheHelper.Get(identifier);
            if (entry == null)
            {
                entry = await _contextProvider.Context.Get(identifier, scope).ConfigureAwait(false);
                if (_cacheHelper.ShouldStore(entry))
                {
                    _cacheHelper.Store(entry);
                }
            }

            if (relations.HasFlag(EntryRelation.Child))
            {
                result = Add(entry.Children, scope);
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }
                result = Add(entry.Children2, scope);
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }

            }
            if (relations.HasFlag(EntryRelation.Downdate))
            {
                result = Add(entry.Downdate, scope);
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }
            }
            if (relations.HasFlag(EntryRelation.Index))
            {
                result = Add(entry.Indexes, scope);
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }
            }
            if (relations.HasFlag(EntryRelation.Indexed))
            {
                result = Add(entry.Indexed, scope);
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }
            }
            if (relations.HasFlag(EntryRelation.Next))
            {
                result = Add(entry.Next, scope);
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }
            }
            if (relations.HasFlag(EntryRelation.Parent))
            {
                result = Add(entry.Parent, scope);
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }
                result = Add(entry.Parent2, scope);
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }
            }
            if (relations.HasFlag(EntryRelation.Previous))
            {
                result = Add(entry.Previous, scope);
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }
            }
            if (relations.HasFlag(EntryRelation.Update))
            {
                result = Add(entry.Updates, scope);
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }
            }
        }

        private async IAsyncEnumerable<IReadOnlyEntry> Add(IEnumerable<Relation> relations, ExecutionScope scope)
        {
            foreach(var relation in relations)
            {
                var result = Add(relation, scope);
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }
            }
        }

        private async IAsyncEnumerable<IReadOnlyEntry> Add(Relation relation, ExecutionScope scope)
        {
            if (relation.Id != Identifier.Empty)
            {
                var entry = await _entryGetHandler.Handle(relation.Id, scope).ConfigureAwait(false);
                yield return entry;
            }
        }
    }
}
