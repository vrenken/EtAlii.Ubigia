﻿namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
    using System.Collections.Generic;
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

        public async IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            var start = Environment.TickCount;
            var result = _repository.GetRelated(identifier, entriesWithRelation, entryRelations);
            await foreach (var item in result)
            {
                yield return item;
            }
            _profiler.WriteSample(GetRelatedCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations = EntryRelation.None)
        {
            var start = Environment.TickCount;
            var entries = _repository.Get(identifiers, entryRelations);
            _profiler.WriteSample(GetByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entries;
        }

        public Entry Get(Identifier identifier, EntryRelation entryRelations = EntryRelation.None)
        {
            var start = Environment.TickCount;
            var entry = _repository.Get(identifier, entryRelations);
            _profiler.WriteSample(GetByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entry;
        }

        public Entry Prepare(Guid spaceId)
        {
            var start = Environment.TickCount;
            var entry = _repository.Prepare(spaceId);
            _profiler.WriteSample(PrepareCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entry;
        }

        public Entry Prepare(Guid spaceId, Identifier identifier)
        {
            var start = Environment.TickCount;
            var entry = _repository.Prepare(spaceId, identifier);
            _profiler.WriteSample(PrepareCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entry;
        }

        public Entry Store(Entry entry)
        {
            var start = Environment.TickCount;
            var storedEntry = _repository.Store(entry);
            _profiler.WriteSample(StoreCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storedEntry;
        }
        
        public Entry Store(IEditableEntry entry)
        {
            var start = Environment.TickCount;
            var storedEntry = _repository.Store(entry);
            _profiler.WriteSample(StoreCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storedEntry;
        }
    }
}