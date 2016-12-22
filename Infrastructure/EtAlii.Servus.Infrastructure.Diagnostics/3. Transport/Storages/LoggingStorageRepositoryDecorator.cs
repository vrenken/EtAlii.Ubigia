namespace EtAlii.Servus.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.xTechnology.Logging;

    internal class LoggingStorageRepositoryDecorator : IStorageRepository
    {
        private readonly IStorageRepository _repository;
        private readonly ILogger _logger;

        public LoggingStorageRepositoryDecorator(
            IStorageRepository storageRepository, 
            IProfiler profiler,
            ILogger logger)
        {
            _repository = storageRepository;
            _logger = logger;
        }

        public Storage GetLocal()
        {
            var message = String.Format("Getting local storage");
            _logger.Info(message);
            var start = Environment.TickCount;

            var storage = _repository.GetLocal();

            message = $"Got local storage (Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return storage;
        }

        public Storage Get(string name)
        {
            var message = String.Format("Getting storage (Name: {0})", name);
            _logger.Info(message);
            var start = Environment.TickCount;

            message = $"Got storage (Name: {name} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            var storage = _repository.Get(name);
            return storage;
        }

        public IEnumerable<Storage> GetAll()
        {
            var message = String.Format("Getting all storages");
            _logger.Info(message);
            var start = Environment.TickCount;

            var storages = _repository.GetAll();

            message = $"Got all storages (Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return storages;
        }

        public Storage Get(Guid storageId)
        {
            var message = String.Format("Getting storage (Id: {0})", storageId);
            _logger.Info(message);
            var start = Environment.TickCount;

            var storage = _repository.Get(storageId);

            message = $"Got storage (Id: {storageId} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return storage;
        }

        public Storage Add(Storage storage)
        {
            var message = String.Format("Adding storage (Id: {0})", storage.Id);
            _logger.Info(message);
            var start = Environment.TickCount;

            storage = _repository.Add(storage);

            message = $"Added storage (Id: {storage.Id} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return storage;
        }

        public void Remove(Guid storageId)
        {
            var message = String.Format("Removing storage (Id: {0})", storageId);
            _logger.Info(message);
            var start = Environment.TickCount;

            _repository.Remove(storageId);

            message = $"Removed storage (Id: {storageId} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public void Remove(Storage storage)
        {
            var message = String.Format("Removing storage (Id: {0})", storage.Id);
            _logger.Info(message);
            var start = Environment.TickCount;

            _repository.Remove(storage);

            message = $"Removed storage (Id: {storage.Id} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public Storage Update(Guid storageId, Storage storage)
        {
            var message = String.Format("Updating storage (Id: {0})", storageId);
            _logger.Info(message);
            var start = Environment.TickCount;

            storage = _repository.Update(storageId, storage);

            message = $"Updated storage (Id: {storageId} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return storage;
        }
    }
}