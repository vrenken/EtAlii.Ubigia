// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;

    internal class ProfilingSpaceRepositoryDecorator : ISpaceRepository
    {
        private readonly ISpaceRepository _repository;
        private readonly IProfiler _profiler;

        private const string GetAllCounter = "SpaceRepository.Get.All";
        private const string GetAllByAccountCounter = "SpaceRepository.Get.AllByAccount";
        private const string GetByNameCounter = "SpaceRepository.Get.ByName";
        private const string GetByIdCounter = "SpaceRepository.Get.ById";
        private const string AddCounter = "SpaceRepository.Add";
        private const string RemoveByIdCounter = "SpaceRepository.Remove.ById";
        private const string RemoveByInstanceCounter = "SpaceRepository.Remove.ByInstance";
        private const string UpdateCounter = "SpaceRepository.Update";

        public ProfilingSpaceRepositoryDecorator(ISpaceRepository spaceRepository, IProfiler profiler)
        {
            _repository = spaceRepository;
            _profiler = profiler;

            profiler.Register(GetAllCounter, SamplingType.RawCount, "Milliseconds", "Get all spaces", "The time it takes for the GetAll method to execute");
            profiler.Register(GetAllByAccountCounter, SamplingType.RawCount, "Milliseconds", "Get all spaces by account id", "The time it takes for the GetAll (by account id) method to execute");
            profiler.Register(GetByNameCounter, SamplingType.RawCount, "Milliseconds", "Get space by name", "The time it takes for the Get (by name) method to execute");
            profiler.Register(GetByIdCounter, SamplingType.RawCount, "Milliseconds", "Get space by id", "The time it takes for the Get (by id) method to execute");

            profiler.Register(AddCounter, SamplingType.RawCount, "Milliseconds", "Add space", "The time it takes for the Add method to execute");
            profiler.Register(RemoveByInstanceCounter, SamplingType.RawCount, "Milliseconds", "Remove space by instance", "The time it takes for the Remove (by instance) method to execute");
            profiler.Register(RemoveByIdCounter, SamplingType.RawCount, "Milliseconds", "Remove space by id", "The time it takes for the Remove (by id) method to execute");
            profiler.Register(UpdateCounter, SamplingType.RawCount, "Milliseconds", "Update space", "The time it takes for the Update method to execute");
        }

        public async IAsyncEnumerable<Space> GetAll(Guid accountId)
        {
            var start = Environment.TickCount;
            var items = _repository
                .GetAll(accountId)
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }
            _profiler.WriteSample(GetAllByAccountCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async IAsyncEnumerable<Space> GetAll()
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

        public async Task<Space> Get(Guid accountId, string spaceName)
        {
            var start = Environment.TickCount;
            var spaces = await _repository.Get(accountId, spaceName).ConfigureAwait(false);
            _profiler.WriteSample(GetByNameCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return spaces;
        }

        public async Task<Space> Get(Guid itemId)
        {
            var start = Environment.TickCount;
            var space = await _repository.Get(itemId).ConfigureAwait(false);
            _profiler.WriteSample(GetByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return space;
        }

        public async Task<Space> Add(Space item, SpaceTemplate template)
        {
            var start = Environment.TickCount;
            item = await _repository.Add(item, template).ConfigureAwait(false);
            _profiler.WriteSample(AddCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return item;
        }

        public async Task Remove(Guid itemId)
        {
            var start = Environment.TickCount;
            await _repository.Remove(itemId).ConfigureAwait(false);
            _profiler.WriteSample(RemoveByIdCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task Remove(Space item)
        {
            var start = Environment.TickCount;
            await _repository.Remove(item).ConfigureAwait(false);
            _profiler.WriteSample(RemoveByInstanceCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task<Space> Update(Guid itemId, Space item)
        {
            var start = Environment.TickCount;
            item = await _repository.Update(itemId, item).ConfigureAwait(false);
            _profiler.WriteSample(UpdateCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return item;
        }
    }
}
