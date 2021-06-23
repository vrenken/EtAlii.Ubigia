namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;

    internal class ProfilingAccountRepositoryDecorator : IAccountRepository
    {
        private readonly IAccountRepository _repository;
        private readonly IProfiler _profiler;

        #pragma warning disable S2068 // False positives. The entries below aren't passwords.
        private const string GetByNameNoPasswordCounter = "AccountRepository.Get.ByName.NoPassword";
        private const string GetByNamePasswordCounter = "AccountRepository.Get.ByName.Password";
        private const string GetByIdNoPasswordCounter = "AccountRepository.Get.ByName.NoPassword";
        #pragma warning restore S2068

        private const string GetAllCounter = "AccountRepository.Get.All";
        private const string AddCounter = "AccountRepository.Add";
        private const string RemoveByIdCounter = "AccountRepository.Remove.ByInstance";
        private const string RemoveByInstanceCounter = "AccountRepository.Remove.ByItem";

        private const string UpdateCounter = "AccountRepository.Update";

        public ProfilingAccountRepositoryDecorator(IAccountRepository accountRepository, IProfiler profiler)
        {
            _repository = accountRepository;
            _profiler = profiler;

            profiler.Register(GetByNameNoPasswordCounter, SamplingType.RawCount, "Milliseconds", "Get entry by name without using a password", "The time it takes for the Get (by name) method to execute without a password given");
            profiler.Register(GetByNamePasswordCounter, SamplingType.RawCount, "Milliseconds", "Get entry by name using a password", "The time it takes for the Get (by name) method to execute with a password given");
            profiler.Register(GetAllCounter, SamplingType.RawCount, "Milliseconds", "Get all entries", "The time it takes for the GetAll method to execute");

            profiler.Register(GetByIdNoPasswordCounter, SamplingType.RawCount, "Milliseconds", "Get entry by id without using a password", "The time it takes for the Get (by id) method to execute without a password given");

            profiler.Register(AddCounter, SamplingType.RawCount, "Milliseconds", "Add account", "The time it takes for the Add method to execute");
            profiler.Register(RemoveByIdCounter, SamplingType.RawCount, "Milliseconds", "Remove account by id", "The time it takes for the Remove (by id) method to execute");
            profiler.Register(RemoveByInstanceCounter, SamplingType.RawCount, "Milliseconds", "Remove account by instance", "The time it takes for the Remove (by instance) method to execute");

            profiler.Register(UpdateCounter, SamplingType.RawCount, "Milliseconds", "Update account", "The time it takes for the Update method to execute");
        }

        public Account Get(string accountName)
        {
            var start = Environment.TickCount;
            var account = _repository.Get(accountName);
            _profiler.WriteSample(GetByNameNoPasswordCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return account;
        }

        public Account Get(string accountName, string password)
        {
            var start = Environment.TickCount;
            var account = _repository.Get(accountName, password);
            _profiler.WriteSample(GetByNamePasswordCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return account;
        }

        public Account Get(Guid itemId)
        {
            var start = Environment.TickCount;
            var account = _repository.Get(itemId);
            _profiler.WriteSample(GetByIdNoPasswordCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return account;
        }

        public async IAsyncEnumerable<Account> GetAll()
        {
            var start = Environment.TickCount;
            var items = _repository.GetAll();
            await foreach (var item in items.ConfigureAwait(false))
            {
                yield return item;
            }
            _profiler.WriteSample(GetAllCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task<Account> Add(Account item, AccountTemplate template)
        {
            var start = Environment.TickCount;
            item = await _repository.Add(item, template).ConfigureAwait(false);
            _profiler.WriteSample(AddCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return item;
        }

        public void Remove(Guid itemId)
        {
            var start = Environment.TickCount;
            _repository.Remove(itemId);
            _profiler.WriteSample(RemoveByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public void Remove(Account item)
        {
            var start = Environment.TickCount;
            _repository.Remove(item);
            _profiler.WriteSample(RemoveByInstanceCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public Account Update(Guid itemId, Account item)
        {
            var start = Environment.TickCount;
            item = _repository.Update(itemId, item);
            _profiler.WriteSample(UpdateCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return item;
        }
    }
}
