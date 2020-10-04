namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Logging;

    internal class LoggingStorageRepositoryDecorator : IStorageRepository
    {
        private readonly IStorageRepository _repository;
        private readonly ILogger _logger;

        public LoggingStorageRepositoryDecorator(
            IStorageRepository storageRepository, 
            //IProfiler profiler,
            ILogger logger)
        {
            _repository = storageRepository;
            _logger = logger;
        }

        public Storage GetLocal()
        {
            var message = "Getting local storage";
            _logger.Info(message);
            var start = Environment.TickCount;

            var storage = _repository.GetLocal();

            message = $"Got local storage (Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return storage;
        }

        public Storage Get(string name)
        {
            var message = $"Getting storage (Name: {name})";
            _logger.Info(message);
            var start = Environment.TickCount;

            message = $"Got storage (Name: {name} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            var storage = _repository.Get(name);
            return storage;
        }

        public IEnumerable<Storage> GetAll()
        {
            var message = "Getting all storages";
            _logger.Info(message);
            var start = Environment.TickCount;

            var storages = _repository.GetAll();

            message = $"Got all storages (Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return storages;
        }

        public Storage Get(Guid itemId)
        {
            var message = $"Getting storage (Id: {itemId})";
            _logger.Info(message);
            var start = Environment.TickCount;

            var storage = _repository.Get(itemId);

            message = $"Got storage (Id: {itemId} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return storage;
        }

        public async Task<Storage> Add(Storage item)
        {
            var message = $"Adding storage (Id: {item.Id})";
            _logger.Info(message);
            var start = Environment.TickCount;

            item = await _repository.Add(item);

            message = $"Added storage (Id: {item.Id} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return item;
        }

        public void Remove(Guid itemId)
        {
            var message = $"Removing storage (Id: {itemId})";
            _logger.Info(message);
            var start = Environment.TickCount;

            _repository.Remove(itemId);

            message = $"Removed storage (Id: {itemId} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public void Remove(Storage item)
        {
            var message = $"Removing storage (Id: {item.Id})";
            _logger.Info(message);
            var start = Environment.TickCount;

            _repository.Remove(item);

            message = $"Removed storage (Id: {item.Id} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public Storage Update(Guid itemId, Storage item)
        {
            var message = $"Updating storage (Id: {itemId})";
            _logger.Info(message);
            var start = Environment.TickCount;

            item = _repository.Update(itemId, item);

            message = $"Updated storage (Id: {itemId} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return item;
        }
    }
}