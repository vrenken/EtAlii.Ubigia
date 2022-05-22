// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;

    internal class ProfilingStorageRepositoryDecorator : IStorageRepository
    {
        private readonly IStorageRepository _repository;
        private readonly IProfiler _profiler;

        private const string GetAllCounter = "StorageRepository.Get.All";
        private const string GetLocalCounter = "StorageRepository.Get.Local";
        private const string GetByNameCounter = "StorageRepository.Get.ByName";
        private const string GetByIdCounter = "StorageRepository.Get.ById";
        private const string AddCounter = "StorageRepository.Add";
        private const string RemoveByIdCounter = "StorageRepository.Remove.ById";
        private const string RemoveByInstanceCounter = "StorageRepository.Remove.ByInstance";
        private const string UpdateCounter = "StorageRepository.Update";

        public ProfilingStorageRepositoryDecorator(IStorageRepository storageRepository, IProfiler profiler)
        {
            _repository = storageRepository;
            _profiler = profiler;

            profiler.Register(GetAllCounter, SamplingType.RawCount, "Milliseconds", "Get all storages", "The time it takes for the GetAll method to execute");
            profiler.Register(GetLocalCounter, SamplingType.RawCount, "Milliseconds", "Get local storage", "The time it takes for the GetLocal method to execute");
            profiler.Register(GetByNameCounter, SamplingType.RawCount, "Milliseconds", "Get storage by name", "The time it takes for the Get (by name) method to execute");
            profiler.Register(GetByIdCounter, SamplingType.RawCount, "Milliseconds", "Get storage by id", "The time it takes for the Get (by id) method to execute");

            profiler.Register(AddCounter, SamplingType.RawCount, "Milliseconds", "Add storage", "The time it takes for the Add method to execute");
            profiler.Register(RemoveByInstanceCounter, SamplingType.RawCount, "Milliseconds", "Remove storage by instance", "The time it takes for the Remove (by instance) method to execute");
            profiler.Register(RemoveByIdCounter, SamplingType.RawCount, "Milliseconds", "Remove storage by id", "The time it takes for the Remove (by id) method to execute");
            profiler.Register(UpdateCounter, SamplingType.RawCount, "Milliseconds", "Update storage", "The time it takes for the Update method to execute");
        }

        public async Task<Storage> GetLocal()
        {
            var start = Environment.TickCount;
            var storage = await _repository.GetLocal().ConfigureAwait(false);
            _profiler.WriteSample(GetLocalCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storage;
        }

        public async IAsyncEnumerable<Storage> GetAll()
        {
            var start = Environment.TickCount;
            var items = _repository
                .GetAll()
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }
            _profiler.WriteSample(GetAllCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task<Storage> Get(string name)
        {
            var start = Environment.TickCount;
            var storage = await _repository.Get(name).ConfigureAwait(false);
            _profiler.WriteSample(GetByNameCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storage;
        }

        public async Task<Storage> Get(Guid itemId)
        {
            var start = Environment.TickCount;
            var storage = await _repository.Get(itemId).ConfigureAwait(false);
            _profiler.WriteSample(GetByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storage;
        }

        public async Task<Storage> Add(Storage item)
        {
            var start = Environment.TickCount;
            item = await _repository.Add(item).ConfigureAwait(false);
            _profiler.WriteSample(AddCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return item;
        }

        public async Task Remove(Guid itemId)
        {
            var start = Environment.TickCount;
            await _repository.Remove(itemId).ConfigureAwait(false);
            _profiler.WriteSample(RemoveByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task Remove(Storage item)
        {
            var start = Environment.TickCount;
            await _repository.Remove(item).ConfigureAwait(false);
            _profiler.WriteSample(RemoveByInstanceCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task<Storage> Update(Guid itemId, Storage item)
        {
            var start = Environment.TickCount;
            item = await _repository.Update(itemId, item).ConfigureAwait(false);
            _profiler.WriteSample(UpdateCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return item;
        }
    }
}
