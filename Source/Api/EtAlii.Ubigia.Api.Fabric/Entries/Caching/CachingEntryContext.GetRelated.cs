﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Collections.Generic;
using System.Threading.Tasks;

internal partial class CachingEntryContext
{
    public async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelations relations, ExecutionScope scope)
    {
        if (!scope.EntryCache.TryGetValue(identifier, out var entry))
        {
            entry = await _decoree
                .Get(identifier, scope)
                .ConfigureAwait(false);
            if (ShouldStore(entry))
            {
                scope.EntryCache[entry.Id] = entry;
            }
        }

        // Child
        var result = Add(entry.Children, scope, relations, EntryRelations.Child)
            .ConfigureAwait(false);
        await foreach (var item in result)
        {
            yield return item;
        }
        result = Add(entry.Children2, scope, relations, EntryRelations.Child)
            .ConfigureAwait(false);
        await foreach (var item in result)
        {
            yield return item;
        }

        // Downdate
        result = Add(entry.Downdate, scope, relations, EntryRelations.Downdate)
            .ConfigureAwait(false);
        await foreach (var item in result)
        {
            yield return item;
        }

        // Index
        result = Add(entry.Indexes, scope, relations, EntryRelations.Index)
            .ConfigureAwait(false);
        await foreach (var item in result)
        {
            yield return item;
        }

        // Indexed
        result = Add(entry.Indexed, scope, relations, EntryRelations.Indexed)
            .ConfigureAwait(false);
        await foreach (var item in result)
        {
            yield return item;
        }

        // Next
        result = Add(entry.Next, scope, relations, EntryRelations.Next)
            .ConfigureAwait(false);
        await foreach (var item in result)
        {
            yield return item;
        }

        // Parent
        result = Add(entry.Parent, scope, relations, EntryRelations.Parent)
            .ConfigureAwait(false);
        await foreach (var item in result)
        {
            yield return item;
        }
        result = Add(entry.Parent2, scope, relations, EntryRelations.Parent)
            .ConfigureAwait(false);
        await foreach (var item in result)
        {
            yield return item;
        }

        // Previous
        result = Add(entry.Previous, scope, relations, EntryRelations.Previous)
            .ConfigureAwait(false);
        await foreach (var item in result)
        {
            yield return item;
        }

        // Update
        result = Add(entry.Updates, scope, relations, EntryRelations.Update)
            .ConfigureAwait(false);
        await foreach (var item in result)
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
                var result = Add(relation, scope)
                    .ConfigureAwait(false);
                await foreach (var item in result)
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
            var entry = await Get(relation.Id, scope)
                .ConfigureAwait(false);
            yield return entry;
        }
    }

    private async IAsyncEnumerable<IReadOnlyEntry> Add(Relation relation, ExecutionScope scope, EntryRelations entryRelations, EntryRelations entryRelationToMatch)
    {
        if (entryRelations.HasFlag(entryRelationToMatch) && relation.Id != Identifier.Empty)
        {
            var entry = await Get(relation.Id, scope)
                .ConfigureAwait(false);
            yield return entry;
        }
    }
}
