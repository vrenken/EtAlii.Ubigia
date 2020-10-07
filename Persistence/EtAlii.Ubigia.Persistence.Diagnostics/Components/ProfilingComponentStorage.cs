﻿namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Diagnostics;

    public class ProfilingComponentStorage : IComponentStorage
    {
        private readonly IComponentStorage _storage;
        private readonly IProfiler _profiler;

        private const string GetNextContainerCounter = "ComponentStorage.GetNextContainer";
        private const string RetrieveCounter = "ComponentStorage.Retrieve";
        private const string RetrieveAllCounter = "ComponentStorage.RetrieveAll";
        private const string StoreCounter = "ComponentStorage.Store";
        private const string StoreAllCounter = "ComponentStorage.StoreAll";

        public ProfilingComponentStorage(
            IComponentStorage storage, 
            IProfiler profiler)
        {
            _storage = storage;
            _profiler = profiler;

            profiler.Register(GetNextContainerCounter, SamplingType.RawCount, "Milliseconds", "Get next container", "The time it takes for the GetNextContainer method to execute");
            profiler.Register(RetrieveCounter, SamplingType.RawCount, "Milliseconds", "Retrieve a non-composite container", "The time it takes for the Retrieve method to execute");
            profiler.Register(RetrieveAllCounter, SamplingType.RawCount, "Milliseconds", "Retrieve a composite container", "The time it takes for the RetrieveAll method to execute");
            profiler.Register(StoreCounter, SamplingType.RawCount, "Milliseconds", "Store a component", "The time it takes for the Store method to execute");
            profiler.Register(StoreAllCounter, SamplingType.RawCount, "Milliseconds", "Store components", "The time it takes for the StoreAll method to execute");
        }

        public ContainerIdentifier GetNextContainer(ContainerIdentifier container)
        {
            var startTicks = Environment.TickCount;
            var result = _storage.GetNextContainer(container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(GetNextContainerCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
            return result;
        }

        public T Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent
        {
            var startTicks = Environment.TickCount;
            var result = _storage.Retrieve<T>(container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(RetrieveCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
            return result;
        }

        public IEnumerable<T> RetrieveAll<T>(ContainerIdentifier container) 
            where T : CompositeComponent
        {
            var startTicks = Environment.TickCount;
            var result = _storage.RetrieveAll<T>(container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(RetrieveAllCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
            return result;
        }

        public void Store<T>(ContainerIdentifier container, T component) 
            where T : class, IComponent
        {
            var startTicks = Environment.TickCount;
            _storage.Store(container, component);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(StoreCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
        }

        public void StoreAll<T>(ContainerIdentifier container, IEnumerable<T> components) 
            where T : class, IComponent
        {
            var startTicks = Environment.TickCount;
            _storage.StoreAll(container, components);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(StoreCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
        }
    }
}