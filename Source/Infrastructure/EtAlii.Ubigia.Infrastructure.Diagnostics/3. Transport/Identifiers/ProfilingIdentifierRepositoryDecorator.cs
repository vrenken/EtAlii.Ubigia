namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;

    internal class ProfilingIdentifierRepositoryDecorator : IIdentifierRepository
    {
        private readonly IIdentifierRepository _repository;
        private readonly IProfiler _profiler;

        private const string _getTailCounter = "IdentifierRepository.GetTail";
        private const string _getCurrentHeadCounter = "IdentifierRepository.GetCurrentHead";
        private const string _getGetNextHeadCounter = "IdentifierRepository.GetNextHead";

        public ProfilingIdentifierRepositoryDecorator(IIdentifierRepository identifierRepository, IProfiler profiler)
        {
            _repository = identifierRepository;
            _profiler = profiler;

            profiler.Register(_getTailCounter, SamplingType.RawCount, "Milliseconds", "Get tail identifier", "The time it takes for the GetTail method to execute");
            profiler.Register(_getCurrentHeadCounter, SamplingType.RawCount, "Milliseconds", "Get current head identifier", "The time it takes for the GetCurrentHead method to execute");
            profiler.Register(_getGetNextHeadCounter, SamplingType.RawCount, "Milliseconds", "Get next head identifier", "The time it takes for the GetNextHead method to execute"); 
        }


        public async Task<Identifier> GetTail(Guid spaceId)
        {
            var start = Environment.TickCount;
            var result = await _repository.GetTail(spaceId).ConfigureAwait(false);
            _profiler.WriteSample(_getTailCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return result;
        }

        public async Task<Identifier> GetCurrentHead(Guid spaceId)
        {
            var start = Environment.TickCount;
            var result = await _repository.GetCurrentHead(spaceId).ConfigureAwait(false);
            _profiler.WriteSample(_getCurrentHeadCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return result;
        }

        public async Task<(Identifier NextHeadIdentifier, Identifier PreviousHeadIdentifier)> GetNextHead(Guid spaceId)
        {
            var start = Environment.TickCount;
            var head = await _repository.GetNextHead(spaceId).ConfigureAwait(false);
            _profiler.WriteSample(_getGetNextHeadCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return head;
        }
    }
}