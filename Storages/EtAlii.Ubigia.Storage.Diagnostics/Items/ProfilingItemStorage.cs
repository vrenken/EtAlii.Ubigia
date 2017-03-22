namespace EtAlii.Ubigia.Storage
{
    using System;
    using EtAlii.xTechnology.Logging;

    public class ProfilingItemStorage : IItemStorage
    {
        private readonly IItemStorage _storage;
        private readonly IProfiler _profiler;

        private const string _storeCounter = "ItemStorage.Store";
        private const string _retrieveCounter = "ItemStorage.Retrieve";
        private const string _removeCounter = "ItemStorage.Remove";
        private const string _getCounter = "ItemStorage.Get";
        private const string _hasCounter = "ItemStorage.Has";

        public ProfilingItemStorage(
            IItemStorage storage, 
            IProfiler profiler)
        {
            _storage = storage;
            _profiler = profiler;

            profiler.Register(_storeCounter, SamplingType.RawCount, "Milliseconds", "Store item", "The time it takes for the Store method to execute");
            profiler.Register(_retrieveCounter, SamplingType.RawCount, "Milliseconds", "Retrieve item", "The time it takes for the Retrieve method to execute");
            profiler.Register(_removeCounter, SamplingType.RawCount, "Milliseconds", "Remove item", "The time it takes for the Remove method to execute");
            profiler.Register(_getCounter, SamplingType.RawCount, "Milliseconds", "Get item", "The time it takes for the Get method to execute");
            profiler.Register(_hasCounter, SamplingType.RawCount, "Milliseconds", "Has item", "The time it takes for the Has method to execute");

        }

        public void Store<T>(T item, Guid id, ContainerIdentifier container) where T : class
        {
            var startTicks = Environment.TickCount;
            _storage.Store(item, id, container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(_storeCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
        }

        public T Retrieve<T>(Guid id, ContainerIdentifier container) where T : class
        {
            var startTicks = Environment.TickCount;
            var result = _storage.Retrieve<T>(id, container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(_retrieveCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
            return result;
        }

        public void Remove(Guid id, ContainerIdentifier container)
        {
            var startTicks = Environment.TickCount;
            _storage.Remove(id, container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(_removeCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
        }

        public Guid[] Get(ContainerIdentifier container)
        {
            var startTicks = Environment.TickCount;
            var result = _storage.Get(container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(_getCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
            return result;
        }

        public bool Has(Guid id, ContainerIdentifier container)
        {
            var startTicks = Environment.TickCount;
            var result = _storage.Has(id, container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(_hasCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
            return result;
        }
    }
}