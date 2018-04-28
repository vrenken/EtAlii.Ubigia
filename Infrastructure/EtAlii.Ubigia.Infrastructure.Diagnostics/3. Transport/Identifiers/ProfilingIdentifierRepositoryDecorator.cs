namespace EtAlii.Ubigia.Infrastructure
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Logging;

    internal class ProfilingIdentifierRepositoryDecorator : IIdentifierRepository
    {
        private readonly IIdentifierRepository _repository;
        private readonly IProfiler _profiler;

        private const string GetTailCounter = "IdentifierRepository.GetTail";
        private const string GetCurrentHeadCounter = "IdentifierRepository.GetCurrentHead";
        private const string GetGetNextHeadCounter = "IdentifierRepository.GetNextHead";

        public ProfilingIdentifierRepositoryDecorator(IIdentifierRepository identifierRepository, IProfiler profiler)
        {
            _repository = identifierRepository;
            _profiler = profiler;

            profiler.Register(GetTailCounter, SamplingType.RawCount, "Milliseconds", "Get tail identifier", "The time it takes for the GetTail method to execute");
            profiler.Register(GetCurrentHeadCounter, SamplingType.RawCount, "Milliseconds", "Get current head identifier", "The time it takes for the GetCurrentHead method to execute");
            profiler.Register(GetGetNextHeadCounter, SamplingType.RawCount, "Milliseconds", "Get next head identifier", "The time it takes for the GetNextHead method to execute"); 
        }


        public Identifier GetTail(Guid spaceId)
        {
            var start = Environment.TickCount;
            var result = _repository.GetTail(spaceId);
            _profiler.WriteSample(GetTailCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return result;
        }

        public Identifier GetCurrentHead(Guid spaceId)
        {
            var start = Environment.TickCount;
            var result = _repository.GetCurrentHead(spaceId);
            _profiler.WriteSample(GetCurrentHeadCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return result;
        }

        public Identifier GetNextHead(Guid spaceId, out Identifier previousHeadIdentifier)
        {
            var start = Environment.TickCount;
            var result = _repository.GetNextHead(spaceId, out previousHeadIdentifier);
            _profiler.WriteSample(GetGetNextHeadCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return result;
        }
    }
}