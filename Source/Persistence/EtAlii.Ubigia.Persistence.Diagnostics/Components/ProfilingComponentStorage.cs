// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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

        /// <inheritdoc />
        public ContainerIdentifier GetNextContainer(ContainerIdentifier container)
        {
            var startTicks = Environment.TickCount;
            var result = _storage.GetNextContainer(container);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(GetNextContainerCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
            return result;
        }

        /// <inheritdoc />
        public async Task<T> Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent
        {
            var startTicks = Environment.TickCount;
            var result = await _storage.Retrieve<T>(container).ConfigureAwait(false);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(RetrieveCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
            return result;
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
            where T : CompositeComponent
        {
            var startTicks = Environment.TickCount;
            var items = _storage
                .RetrieveAll<T>(container)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(RetrieveAllCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
        }

        /// <inheritdoc />
        public void Store<T>(ContainerIdentifier container, T component)
            where T : class, IComponent
        {
            var startTicks = Environment.TickCount;
            _storage.Store(container, component);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(StoreCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
        }

        /// <inheritdoc />
        public async Task StoreAll<T>(ContainerIdentifier container, IEnumerable<T> components)
            where T : class, IComponent
        {
            var startTicks = Environment.TickCount;
            await _storage.StoreAll(container, components).ConfigureAwait(false);
            var endTicks = Environment.TickCount;
            _profiler.WriteSample(StoreCounter, TimeSpan.FromTicks(endTicks - startTicks).TotalMilliseconds);
        }
    }
}
