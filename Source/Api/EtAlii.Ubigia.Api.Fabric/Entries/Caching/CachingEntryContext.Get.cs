// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal partial class CachingEntryContext
    {
        public Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope)
        {
            return Get(root.Identifier, scope);
        }

        public async Task<IReadOnlyEntry> Get(Identifier identifier, ExecutionScope scope)
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
            return entry;
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope)
        {
            var missingIdentifiers = new List<Identifier>();

            foreach (var identifier in identifiers)
            {
                if (scope.EntryCache.TryGetValue(identifier, out var entry))
                {
                    yield return entry;
                }
                else
                {
                    missingIdentifiers.Add(identifier);
                }
            }
            if (missingIdentifiers.Count > 0)
            {
                var missingEntries = _decoree
                    .Get(missingIdentifiers, scope)
                    .ConfigureAwait(false);
                await foreach (var missingEntry in missingEntries)
                {
                    if (ShouldStore(missingEntry))
                    {
                        scope.EntryCache[missingEntry.Id] = missingEntry;
                    }

                    yield return missingEntry;
                }
            }
        }
    }
}
