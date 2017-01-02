namespace EtAlii.Ubigia.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Logging;

    internal class ProfilingStorageRepositoryDecorator : IStorageRepository
    {
        private readonly IStorageRepository _repository;
        private readonly IProfiler _profiler;

        private const string _getAllCounter = "StorageRepository.Get.All";
        private const string _getLocalCounter = "StorageRepository.Get.Local";
        private const string _getByNameCounter = "StorageRepository.Get.ByName";
        private const string _getByIdCounter = "StorageRepository.Get.ById";
        private const string _addCounter = "StorageRepository.Add";
        private const string _removeByIdCounter = "StorageRepository.Remove.ById";
        private const string _removeByInstanceCounter = "StorageRepository.Remove.ByInstance";
        private const string _updateCounter = "StorageRepository.Update";

        public ProfilingStorageRepositoryDecorator(IStorageRepository storageRepository, IProfiler profiler)
        {
            _repository = storageRepository;
            _profiler = profiler;

            profiler.Register(_getAllCounter, SamplingType.RawCount, "Milliseconds", "Get all storages", "The time it takes for the GetAll method to execute");
            profiler.Register(_getLocalCounter, SamplingType.RawCount, "Milliseconds", "Get local storage", "The time it takes for the GetLocal method to execute");
            profiler.Register(_getByNameCounter, SamplingType.RawCount, "Milliseconds", "Get storage by name", "The time it takes for the Get (by name) method to execute");
            profiler.Register(_getByIdCounter, SamplingType.RawCount, "Milliseconds", "Get storage by id", "The time it takes for the Get (by id) method to execute");

            profiler.Register(_addCounter, SamplingType.RawCount, "Milliseconds", "Add storage", "The time it takes for the Add method to execute");
            profiler.Register(_removeByInstanceCounter, SamplingType.RawCount, "Milliseconds", "Remove storage by instance", "The time it takes for the Remove (by instance) method to execute");
            profiler.Register(_removeByIdCounter, SamplingType.RawCount, "Milliseconds", "Remove storage by id", "The time it takes for the Remove (by id) method to execute");
            profiler.Register(_updateCounter, SamplingType.RawCount, "Milliseconds", "Update storage", "The time it takes for the Update method to execute");
        }

        public Storage GetLocal()
        {
            var start = Environment.TickCount;
            var storage = _repository.GetLocal();
            _profiler.WriteSample(_getLocalCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storage;
        }

        public Storage Get(string name)
        {
            var start = Environment.TickCount;
            var storage = _repository.Get(name);
            _profiler.WriteSample(_getByNameCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storage;
        }

        public IEnumerable<Storage> GetAll()
        {
            var start = Environment.TickCount;
            var storages = _repository.GetAll();
            _profiler.WriteSample(_getAllCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storages;
        }

        public Storage Get(Guid storageId)
        {
            var start = Environment.TickCount;
            var storage = _repository.Get(storageId);
            _profiler.WriteSample(_getByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storage;
        }

        public Storage Add(Storage storage)
        {
            var start = Environment.TickCount;
            storage = _repository.Add(storage);
            _profiler.WriteSample(_addCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storage;
        }

        public void Remove(Guid storageId)
        {
            var start = Environment.TickCount;
            _repository.Remove(storageId);
            _profiler.WriteSample(_removeByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public void Remove(Storage storage)
        {
            var start = Environment.TickCount;
            _repository.Remove(storage);
            _profiler.WriteSample(_removeByInstanceCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public Storage Update(Guid storageId, Storage storage)
        {
            var start = Environment.TickCount;
            storage = _repository.Update(storageId, storage);
            _profiler.WriteSample(_updateCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return storage;
        }
    }
}