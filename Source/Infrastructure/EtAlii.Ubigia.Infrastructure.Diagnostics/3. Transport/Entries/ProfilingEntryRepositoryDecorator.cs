// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;

    internal class ProfilingEntryRepositoryDecorator : IEntryRepository
    {
        private readonly IEntryRepository _repository;
        private readonly IProfiler _profiler;

        private const string GetByIdCounter = "EntryRepository.Get.ById";
        private const string GetRelatedCounter = "EntryRepository.Get.Related";

        private const string PrepareCounter = "EntryRepository.Prepare";
        private const string StoreCounter = "EntryRepository.Store";

        public ProfilingEntryRepositoryDecorator(IEntryRepository entryRepository, IProfiler profiler)
        {
            _repository = entryRepository;
            _profiler = profiler;

            profiler.Register(GetByIdCounter, SamplingType.RawCount, "Milliseconds", "Get entry by id", "The time it takes for the Get method to execute");
            profiler.Register(GetRelatedCounter, SamplingType.RawCount, "Milliseconds", "Get related entries", "The time it takes for the GetRelated method to execute");

            profiler.Register(PrepareCounter, SamplingType.RawCount, "Milliseconds", "Prepare entry", "The time it takes for the Prepare method to execute");
            profiler.Register(StoreCounter, SamplingType.RawCount, "Milliseconds", "Store entry", "The time it takes for the Store method to execute");
        }

        public async IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelations entriesWithRelation, EntryRelations entryRelations = EntryRelations.None)
        {
            var start = Environment.TickCount;
            var items = _repository
                .GetRelated(identifier, entriesWithRelation, entryRelations)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }
            _profiler.WriteSample(GetRelatedCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelations entryRelations = EntryRelations.None)
        {
            var start = Environment.TickCount;
            var items = _repository
                .Get(identifiers, entryRelations)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }
            _profiler.WriteSample(GetByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task<Entry> Get(Identifier identifier, EntryRelations entryRelations = EntryRelations.None)
        {
            var start = Environment.TickCount;
            var entry = await _repository.Get(identifier, entryRelations).ConfigureAwait(false);
            _profiler.WriteSample(GetByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entry;
        }

        public async Task<Entry> Prepare(Guid spaceId)
        {
            var start = Environment.TickCount;
            var entry = await _repository.Prepare(spaceId).ConfigureAwait(false);
            _profiler.WriteSample(PrepareCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entry;
        }

        public async Task<Entry> Prepare(Guid spaceId, Identifier identifier)
        {
            var start = Environment.TickCount;
            var entry = await _repository.Prepare(spaceId, identifier).ConfigureAwait(false);
            _profiler.WriteSample(PrepareCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entry;
        }

        public async Task<Entry> Store(Entry entry)
        {
            var start = Environment.TickCount;
            var storedEntry = await _repository.Store(entry).ConfigureAwait(false);
            _profiler.WriteSample(StoreCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storedEntry;
        }

        public async Task<Entry> Store(IEditableEntry entry)
        {
            var start = Environment.TickCount;
            var storedEntry = await _repository.Store(entry).ConfigureAwait(false);
            _profiler.WriteSample(StoreCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storedEntry;
        }
    }
}
