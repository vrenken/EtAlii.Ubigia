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

        public async IAsyncEnumerable<IReadOnlyEntry> Handle(Identifier identifier, EntryRelations relations, ExecutionScope scope)
        {
            var entry = _cacheHelper.Get(identifier);
            if (entry == null)
            {
                entry = await _contextProvider.Context.Get(identifier, scope).ConfigureAwait(false);
                if (_cacheHelper.ShouldStore(entry))
                {
                    _cacheHelper.Store(entry);
                }
            }

            // Child
            var result = Add(entry.Children, scope, relations, EntryRelations.Child);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }
            result = Add(entry.Children2, scope, relations, EntryRelations.Child);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            // Downdate
            result = Add(entry.Downdate, scope, relations, EntryRelations.Downdate);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            // Index
            result = Add(entry.Indexes, scope, relations, EntryRelations.Index);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            // Indexed
            result = Add(entry.Indexed, scope, relations, EntryRelations.Indexed);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            // Next
            result = Add(entry.Next, scope, relations, EntryRelations.Next);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            // Parent
            result = Add(entry.Parent, scope, relations, EntryRelations.Parent);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }
            result = Add(entry.Parent2, scope, relations, EntryRelations.Parent);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            // Previous
            result = Add(entry.Previous, scope, relations, EntryRelations.Previous);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            // Update
            result = Add(entry.Updates, scope, relations, EntryRelations.Update);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<IReadOnlyEntry> Add(IEnumerable<Relation> relations, ExecutionScope scope, EntryRelations entryRelations, EntryRelations entryRelationToMatch)
        {
            if (entryRelations.HasFlag(entryRelationToMatch))
            {
                foreach (var relation in relations)
                {
                    var result = Add(relation, scope);
                    await foreach (var item in result.ConfigureAwait(false))
                    {
                        yield return item;
                    }
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

        private async IAsyncEnumerable<IReadOnlyEntry> Add(Relation relation, ExecutionScope scope, EntryRelations entryRelations, EntryRelations entryRelationToMatch)
        {
            if (entryRelations.HasFlag(entryRelationToMatch) && relation.Id != Identifier.Empty)
            {
                var entry = await _entryGetHandler.Handle(relation.Id, scope).ConfigureAwait(false);
                yield return entry;
            }
        }
    }
}
