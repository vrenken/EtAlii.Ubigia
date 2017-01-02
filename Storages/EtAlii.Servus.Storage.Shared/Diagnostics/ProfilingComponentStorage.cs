namespace EtAlii.Servus.Storage
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Logging;

    public class ProfilingComponentStorage : IComponentStorage
    {
        private readonly IComponentStorage _storage;
        private readonly IProfiler _profiler;

        private const string _getNextContainerCounter = "ComponentStorage.GetNextContainer";
        private const string _retrieveCounter = "ComponentStorage.Retrieve";
        private const string _retrieveAllCounter = "ComponentStorage.RetrieveAll";
        private const string _storeCounter = "ComponentStorage.Store";
        private const string _storeAllCounter = "ComponentStorage.StoreAll";

        public ProfilingComponentStorage(
            IComponentStorage storage, 
            IProfiler profiler)
        {
            this._storage = storage;
            _profiler = profiler;

            profiler.Register(_getNextContainerCounter, SamplingType.RawCount, "Milliseconds", "Get next container", "The time it takes for the GetNextContainer method to execute");
            profiler.Register(_retrieveCounter, SamplingType.RawCount, "Milliseconds", "Retrieve a non-composite container", "The time it takes for the Retrieve method to execute");
            profiler.Register(_retrieveAllCounter, SamplingType.RawCount, "Milliseconds", "Retrieve a composite container", "The time it takes for the RetrieveAll method to execute");
            profiler.Register(_storeCounter, SamplingType.RawCount, "Milliseconds", "Store a component", "The time it takes for the Store method to execute");
            profiler.Register(_storeAllCounter, SamplingType.RawCount, "Milliseconds", "Store components", "The time it takes for the StoreAll method to execute");
        }

        public ContainerIdentifier GetNextContainer(ContainerIdentifier container)
        {
            var startTicks = Environment.TickCount;
            var result = _storage.GetNextContainer(container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(_getNextContainerCounter, endTicks - startTicks);
            return result;
        }

        public T Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent
        {
            var startTicks = Environment.TickCount;
            var result = _storage.Retrieve<T>(container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(_retrieveCounter, endTicks - startTicks);
            return result;
        }

        public IEnumerable<T> RetrieveAll<T>(ContainerIdentifier container) 
            where T : CompositeComponent
        {
            var startTicks = Environment.TickCount;
            var result = _storage.RetrieveAll<T>(container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(_retrieveAllCounter, endTicks - startTicks);
            return result;
        }

        public void Store<T>(ContainerIdentifier container, T component) 
            where T : class, IComponent
        {
            var startTicks = Environment.TickCount;
            _storage.Store<T>(container, component);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(_storeCounter, endTicks - startTicks);
        }

        public void StoreAll<T>(ContainerIdentifier container, IEnumerable<T> components) 
            where T : class, IComponent
        {
            var startTicks = Environment.TickCount;
            _storage.StoreAll<T>(container, components);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(_storeCounter, endTicks - startTicks);
        }
    }
}