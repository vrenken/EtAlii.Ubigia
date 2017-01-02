namespace EtAlii.Servus.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Logging;

    internal class ProfilingAccountRepositoryDecorator : IAccountRepository
    {
        private readonly IAccountRepository _repository;
        private readonly IProfiler _profiler;

        private const string _getByNameNoPasswordCounter = "AccountRepository.Get.ByName.NoPassword";
        private const string _getByNamePasswordCounter = "AccountRepository.Get.ByName.Password";
        private const string _getAllCounter = "AccountRepository.Get.All";
        private const string _getByIdNoPasswordCounter = "AccountRepository.Get.ByName.NoPassword";
        private const string _addCounter = "AccountRepository.Add";
        private const string _removeByIdCounter = "AccountRepository.Remove.ByInstance";
        private const string _removeByInstanceCounter = "AccountRepository.Remove.ByItem";

        private const string _updateCounter = "AccountRepository.Update";

        public ProfilingAccountRepositoryDecorator(IAccountRepository accountRepository, IProfiler profiler)
        {
            this._repository = accountRepository;
            _profiler = profiler;

            profiler.Register(_getByNameNoPasswordCounter, SamplingType.RawCount, "Milliseconds", "Get entry by name without using a password", "The time it takes for the Get (by name) method to execute without a password given");
            profiler.Register(_getByNamePasswordCounter, SamplingType.RawCount, "Milliseconds", "Get entry by name using a password", "The time it takes for the Get (by name) method to execute with a password given");
            profiler.Register(_getAllCounter, SamplingType.RawCount, "Milliseconds", "Get all entries", "The time it takes for the GetAll method to execute");

            profiler.Register(_getByIdNoPasswordCounter, SamplingType.RawCount, "Milliseconds", "Get entry by id without using a password", "The time it takes for the Get (by id) method to execute without a password given");

            profiler.Register(_addCounter, SamplingType.RawCount, "Milliseconds", "Add account", "The time it takes for the Add method to execute");
            profiler.Register(_removeByIdCounter, SamplingType.RawCount, "Milliseconds", "Remove account by id", "The time it takes for the Remove (by id) method to execute");
            profiler.Register(_removeByInstanceCounter, SamplingType.RawCount, "Milliseconds", "Remove account by instance", "The time it takes for the Remove (by instance) method to execute");

            profiler.Register(_updateCounter, SamplingType.RawCount, "Milliseconds", "Update account", "The time it takes for the Update method to execute");
        }

        public Account Get(string accountName)
        {
            var start = Environment.TickCount;
            var account = _repository.Get(accountName);
            _profiler.WriteSample(_getByNameNoPasswordCounter, Environment.TickCount - start);
            return account;
        }

        public Account Get(string accountName, string password)
        {
            var start = Environment.TickCount;
            var account = _repository.Get(accountName, password);
            _profiler.WriteSample(_getByNamePasswordCounter, Environment.TickCount - start);
            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            var start = Environment.TickCount;
            var accounts = _repository.GetAll();
            _profiler.WriteSample(_getAllCounter, Environment.TickCount - start);
            return accounts;
        }

        public Account Get(Guid spaceId)
        {
            var start = Environment.TickCount;
            var account = _repository.Get(spaceId);
            _profiler.WriteSample(_getByIdNoPasswordCounter, Environment.TickCount - start);
            return account;
            
        }

        public Account Add(Account account)
        {
            var start = Environment.TickCount;
            account = _repository.Add(account);
            _profiler.WriteSample(_addCounter, Environment.TickCount - start);
            return account;
        }

        public void Remove(Guid accountId)
        {
            var start = Environment.TickCount;
            _repository.Remove(accountId);
            _profiler.WriteSample(_removeByIdCounter, Environment.TickCount - start);
        }

        public void Remove(Account account)
        {
            var start = Environment.TickCount;
            _repository.Remove(account);
            _profiler.WriteSample(_removeByInstanceCounter, Environment.TickCount - start);
        }

        public Account Update(Guid accountId, Account account)
        {
            var start = Environment.TickCount;
            account = _repository.Update(accountId, account);
            _profiler.WriteSample(_updateCounter, Environment.TickCount - start);
            return account;
        }
    }
}