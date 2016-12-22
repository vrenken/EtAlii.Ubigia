namespace EtAlii.Servus.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.xTechnology.Logging;

    internal class ProfilingSpaceRepositoryDecorator : ISpaceRepository
    {
        private readonly ISpaceRepository _repository;
        private readonly IProfiler _profiler;

        private const string _getAllCounter = "SpaceRepository.Get.All";
        private const string _getAllByAccountCounter = "SpaceRepository.Get.AllByAccount";
        private const string _getByNameCounter = "SpaceRepository.Get.ByName";
        private const string _getByIdCounter = "SpaceRepository.Get.ById";
        private const string _addCounter = "SpaceRepository.Add";
        private const string _removeByIdCounter = "SpaceRepository.Remove.ById";
        private const string _removeByInstanceCounter = "SpaceRepository.Remove.ByInstance";
        private const string _updateCounter = "SpaceRepository.Update";

        public ProfilingSpaceRepositoryDecorator(ISpaceRepository spaceRepository, IProfiler profiler)
        {
            this._repository = spaceRepository;
            _profiler = profiler;

            profiler.Register(_getAllCounter, SamplingType.RawCount, "Milliseconds", "Get all spaces", "The time it takes for the GetAll method to execute");
            profiler.Register(_getAllByAccountCounter, SamplingType.RawCount, "Milliseconds", "Get all spaces by account id", "The time it takes for the GetAll (by account id) method to execute");
            profiler.Register(_getByNameCounter, SamplingType.RawCount, "Milliseconds", "Get space by name", "The time it takes for the Get (by name) method to execute");
            profiler.Register(_getByIdCounter, SamplingType.RawCount, "Milliseconds", "Get space by id", "The time it takes for the Get (by id) method to execute");

            profiler.Register(_addCounter, SamplingType.RawCount, "Milliseconds", "Add space", "The time it takes for the Add method to execute");
            profiler.Register(_removeByInstanceCounter, SamplingType.RawCount, "Milliseconds", "Remove space by instance", "The time it takes for the Remove (by instance) method to execute");
            profiler.Register(_removeByIdCounter, SamplingType.RawCount, "Milliseconds", "Remove space by id", "The time it takes for the Remove (by id) method to execute");
            profiler.Register(_updateCounter, SamplingType.RawCount, "Milliseconds", "Update space", "The time it takes for the Update method to execute");
        }

        public IEnumerable<Space> GetAll(Guid accountId)
        {
            var start = Environment.TickCount;
            var spaces = _repository.GetAll(accountId);
            _profiler.WriteSample(_getAllByAccountCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return spaces;        
        }

        public Space Get(Guid accountId, string spaceName)
        {
            var start = Environment.TickCount;
            var spaces = _repository.Get(accountId, spaceName);
            _profiler.WriteSample(_getByNameCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return spaces;        
        }

        public IEnumerable<Space> GetAll()
        {
            var start = Environment.TickCount;
            var spaces = _repository.GetAll();
            _profiler.WriteSample(_getAllCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return spaces;        
        }

        public Space Get(Guid spaceId)
        {
            var start = Environment.TickCount;
            var space = _repository.Get(spaceId);
            _profiler.WriteSample(_getByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return space;   
        }

        public Space Add(Space space, SpaceTemplate template)
        {
            var start = Environment.TickCount;
            space = _repository.Add(space, template);
            _profiler.WriteSample(_addCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return space;   
        }

        public void Remove(Guid spaceId)
        {
            var start = Environment.TickCount;
            _repository.Remove(spaceId);
            _profiler.WriteSample(_removeByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public void Remove(Space space)
        {
            var start = Environment.TickCount;
            _repository.Remove(space);
            _profiler.WriteSample(_removeByInstanceCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public Space Update(Guid spaceId, Space space)
        {
            var start = Environment.TickCount;
            space = _repository.Update(spaceId, space);
            _profiler.WriteSample(_updateCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return space;   
        }
    }
}