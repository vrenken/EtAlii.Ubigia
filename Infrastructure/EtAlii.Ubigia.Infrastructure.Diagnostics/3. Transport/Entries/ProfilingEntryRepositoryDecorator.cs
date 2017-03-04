namespace EtAlii.Ubigia.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Logging;

    internal class ProfilingEntryRepositoryDecorator : IEntryRepository
    {
        private readonly IEntryRepository _repository;
        private readonly IProfiler _profiler;

        private const string _getByIdCounter = "EntryRepository.Get.ById";
        private const string _getRelatedCounter = "EntryRepository.Get.Related";
        
        private const string _prepareCounter = "EntryRepository.Prepare";
        private const string _storeCounter = "EntryRepository.Store";

        public ProfilingEntryRepositoryDecorator(IEntryRepository entryRepository, IProfiler profiler)
        {
            _repository = entryRepository;
            _profiler = profiler;

            profiler.Register(_getByIdCounter, SamplingType.RawCount, "Milliseconds", "Get entry by id", "The time it takes for the Get method to execute");
            profiler.Register(_getRelatedCounter, SamplingType.RawCount, "Milliseconds", "Get related entries", "The time it takes for the GetRelated method to execute");
            
            profiler.Register(_prepareCounter, SamplingType.RawCount, "Milliseconds", "Prepare entry", "The time it takes for the Prepare method to execute");
            profiler.Register(_storeCounter, SamplingType.RawCount, "Milliseconds", "Store entry", "The time it takes for the Store method to execute"); 
        }

        public IEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            var start = Environment.TickCount;
            var entries = _repository.GetRelated(identifier, entriesWithRelation, entryRelations);
            _profiler.WriteSample(_getRelatedCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entries;
        }

        public IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations = EntryRelation.None)
        {
            var start = Environment.TickCount;
            var entries = _repository.Get(identifiers, entryRelations);
            _profiler.WriteSample(_getByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entries;
        }

        public Entry Get(Identifier identifier, EntryRelation entryRelations = EntryRelation.None)
        {
            var start = Environment.TickCount;
            var entry = _repository.Get(identifier, entryRelations);
            _profiler.WriteSample(_getByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entry;
        }

        public Entry Prepare(Guid spaceId)
        {
            var start = Environment.TickCount;
            var entry = _repository.Prepare(spaceId);
            _profiler.WriteSample(_prepareCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entry;
        }

        public Entry Prepare(Guid spaceId, Identifier identifier)
        {
            var start = Environment.TickCount;
            var entry = _repository.Prepare(spaceId, identifier);
            _profiler.WriteSample(_prepareCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return entry;
        }

        public Entry Store(Entry entry)
        {
            var start = Environment.TickCount;
            var storedEntry = _repository.Store(entry);
            _profiler.WriteSample(_storeCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storedEntry;
        }
        
        public Entry Store(IEditableEntry entry)
        {
            var start = Environment.TickCount;
            var storedEntry = _repository.Store(entry);
            _profiler.WriteSample(_storeCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storedEntry;
        }
    }
}