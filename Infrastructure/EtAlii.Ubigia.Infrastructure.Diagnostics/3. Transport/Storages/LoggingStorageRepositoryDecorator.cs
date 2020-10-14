namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Serilog;

    internal class LoggingStorageRepositoryDecorator : IStorageRepository
    {
        private readonly IStorageRepository _repository;
        private readonly ILogger _logger = Log.ForContext<IStorageRepository>();

        public LoggingStorageRepositoryDecorator(
            IStorageRepository storageRepository 
            //IProfiler profiler
            )
        {
            _repository = storageRepository;
        }

        public Storage GetLocal()
        {
            var message = "Getting local storage";
            _logger.Information(message);
            var start = Environment.TickCount;

            var storage = _repository.GetLocal();

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Got local storage (Duration: {duration}ms)";
            _logger.Information(message, duration);

            return storage;
        }

        public Storage Get(string name)
        {
            var message = "Getting storage (Name: {name})";
            _logger.Information(message, name);
            var start = Environment.TickCount;

            var storage = _repository.Get(name);
            
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Got storage (Name: {name} Duration: {duration}ms)";
            _logger.Information(message, name, duration);

            return storage;
        }

        public IEnumerable<Storage> GetAll()
        {
            var message = "Getting all storages";
            _logger.Information(message);
            var start = Environment.TickCount;

            var storages = _repository.GetAll();

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Got all storages (Duration: {duration}ms)";
            _logger.Information(message, duration);

            return storages;
        }

        public Storage Get(Guid itemId)
        {
            var message = "Getting storage (Id: {itemId})";
            _logger.Information(message, itemId);
            var start = Environment.TickCount;

            var storage = _repository.Get(itemId);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Got storage (Id: {itemId} Duration: {duration}ms)";
            _logger.Information(message, itemId, duration);

            return storage;
        }

        public async Task<Storage> Add(Storage item)
        {
            var message = "Adding storage (Id: {itemId})";
            _logger.Information(message, item.Id);
            var start = Environment.TickCount;

            item = await _repository.Add(item);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Added storage (Id: {item.Id} Duration: {duration}ms)";
            _logger.Information(message, item.Id, duration);

            return item;
        }

        public void Remove(Guid itemId)
        {
            var message = "Removing storage (Id: {itemId})";
            _logger.Information(message, itemId);
            var start = Environment.TickCount;

            _repository.Remove(itemId);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Removed storage (Id: {itemId} Duration: {duration}ms)";
            _logger.Information(message, itemId, duration);
        }

        public void Remove(Storage item)
        {
            var message = "Removing storage (Id: {itemId})";
            _logger.Information(message, item.Id);
            var start = Environment.TickCount;

            _repository.Remove(item);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Removed storage (Id: {itemId} Duration: {duration}ms)";
            _logger.Information(message, item.Id, duration);
        }

        public Storage Update(Guid itemId, Storage item)
        {
            var message = "Updating storage (Id: {itemId})";
            _logger.Information(message, itemId);
            var start = Environment.TickCount;

            item = _repository.Update(itemId, item);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Updated storage (Id: {itemId} Duration: {duration}ms)";
            _logger.Information(message, itemId, duration);

            return item;
        }
    }
}