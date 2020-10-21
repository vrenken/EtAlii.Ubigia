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

            await _decoree.Connect(spaceConnection);

            _profiler.End(profile);
        }

        public async Task Disconnect()
        {
            dynamic profile = _profiler.Begin("Disconnect");

            await _decoree.Disconnect();

            _profiler.End(profile);
        }

        public async Task<IEditableEntry> Prepare()
        {
            dynamic profile = _profiler.Begin("Prepare");

            var result = await _decoree.Prepare();

            profile.Result = result;
            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Change");

            var result = await _decoree.Change(entry, scope);

            profile.Result = result;
            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            dynamic profile = _profiler.Begin("Get by root: " + root.Name);

            var result = await _decoree.Get(root, scope, entryRelations);

            profile.Result = result;
            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            dynamic profile = _profiler.Begin("Get by id: " + entryIdentifier.ToTimeString());
            profile.EntryIdentifier = entryIdentifier;
            profile.EntryRelations = entryRelations;

            var result = await _decoree.Get(entryIdentifier, scope, entryRelations);

            profile.Result = result;
            _profiler.End(profile);

            return result;
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            dynamic profile = _profiler.Begin("Get multiple by ids: " + string.Join(", ", entryIdentifiers.Select(e => e.ToTimeString())));
            profile.EntryIdentifiers = entryIdentifiers;
            profile.EntryRelations = entryRelations;

            var result = _decoree.Get(entryIdentifiers, scope, entryRelations);
            await foreach (var item in result)
            {
                yield return item; 
            }
            
            profile.Result = result;
            _profiler.End(profile);
        }

        public async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope,
            EntryRelation entryRelations = EntryRelation.None)
        {
            dynamic profile = _profiler.Begin("Get related: " + entryIdentifier + " (relation: " + entriesWithRelation + ")");
            profile.EntryIdentifier = entryIdentifier;
            profile.EntriesWithRelation = entriesWithRelation;
            profile.EntryRelations = entryRelations;

            var result = _decoree.GetRelated(entryIdentifier, entriesWithRelation, scope, entryRelations);
            await foreach (var item in result)
            {
                yield return item; 
            }

            profile.Result = result;
            _profiler.End(profile);
        }
    }
}