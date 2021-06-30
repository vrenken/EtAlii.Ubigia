// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingEntryDataClient : IEntryDataClient
    {
        private readonly IEntryDataClient _decoree;
        private readonly IProfiler _profiler;
        public ProfilingEntryDataClient(
            IEntryDataClient decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Transport.EntryDataClient);
        }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            dynamic profile = _profiler.Begin("Connect");

            await _decoree.Connect(spaceConnection).ConfigureAwait(false);

            _profiler.End(profile);
        }

        public async Task Disconnect()
        {
            dynamic profile = _profiler.Begin("Disconnect");

            await _decoree.Disconnect().ConfigureAwait(false);

            _profiler.End(profile);
        }

        public async Task<IEditableEntry> Prepare()
        {
            dynamic profile = _profiler.Begin("Prepare");

            var result = await _decoree.Prepare().ConfigureAwait(false);

            profile.Result = result;
            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Change");

            var result = await _decoree.Change(entry, scope).ConfigureAwait(false);

            profile.Result = result;
            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
        {
            dynamic profile = _profiler.Begin("Get by root: " + root.Name);

            var result = await _decoree.Get(root, scope, entryRelations).ConfigureAwait(false);

            profile.Result = result;
            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
        {
            dynamic profile = _profiler.Begin("Get by id: " + entryIdentifier.ToTimeString());
            profile.EntryIdentifier = entryIdentifier;
            profile.EntryRelations = entryRelations;

            var result = await _decoree.Get(entryIdentifier, scope, entryRelations).ConfigureAwait(false);

            profile.Result = result;
            _profiler.End(profile);

            return result;
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelations entryRelations = EntryRelations.None)
        {
            dynamic profile = _profiler.Begin("Get multiple by ids: " + string.Join(", ", entryIdentifiers.Select(e => e.ToTimeString())));
            profile.EntryIdentifiers = entryIdentifiers;
            profile.EntryRelations = entryRelations;

            var result = _decoree
                .Get(entryIdentifiers, scope, entryRelations)
                .ConfigureAwait(false);
            await foreach (var item in result)
            {
                yield return item;
            }

            profile.Result = result;
            _profiler.End(profile);
        }

        public async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelations entriesWithRelation, ExecutionScope scope,
            EntryRelations entryRelations = EntryRelations.None)
        {
            dynamic profile = _profiler.Begin("Get related: " + entryIdentifier + " (relation: " + entriesWithRelation + ")");
            profile.EntryIdentifier = entryIdentifier;
            profile.EntriesWithRelation = entriesWithRelation;
            profile.EntryRelations = entryRelations;

            var result = _decoree
                .GetRelated(entryIdentifier, entriesWithRelation, scope, entryRelations)
                .ConfigureAwait(false);
            await foreach (var item in result)
            {
                yield return item;
            }

            profile.Result = result;
            _profiler.End(profile);
        }
    }
}
