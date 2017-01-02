﻿namespace EtAlii.Servus.Infrastructure
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Logging;

    internal class ProfilingIdentifierRepositoryDecorator : IIdentifierRepository
    {
        private readonly IIdentifierRepository _repository;
        private readonly IProfiler _profiler;

        private const string _getTailCounter = "IdentifierRepository.GetTail";
        private const string _getCurrentHeadCounter = "IdentifierRepository.GetCurrentHead";
        private const string _getGetNextHeadCounter = "IdentifierRepository.GetNextHead";

        public ProfilingIdentifierRepositoryDecorator(IIdentifierRepository identifierRepository, IProfiler profiler)
        {
            this._repository = identifierRepository;
            _profiler = profiler;

            profiler.Register(_getTailCounter, SamplingType.RawCount, "Milliseconds", "Get tail identifier", "The time it takes for the GetTail method to execute");
            profiler.Register(_getCurrentHeadCounter, SamplingType.RawCount, "Milliseconds", "Get current head identifier", "The time it takes for the GetCurrentHead method to execute");
            profiler.Register(_getGetNextHeadCounter, SamplingType.RawCount, "Milliseconds", "Get next head identifier", "The time it takes for the GetNextHead method to execute"); 
        }


        public Identifier GetTail(Guid spaceId)
        {
            var start = Environment.TickCount;
            var result = _repository.GetTail(spaceId);
            _profiler.WriteSample(_getTailCounter, Environment.TickCount - start);
            return result;
        }

        public Identifier GetCurrentHead(Guid spaceId)
        {
            var start = Environment.TickCount;
            var result = _repository.GetCurrentHead(spaceId);
            _profiler.WriteSample(_getCurrentHeadCounter, Environment.TickCount - start);
            return result;
        }

        public Identifier GetNextHead(Guid spaceId, out Identifier previousHeadIdentifier)
        {
            var start = Environment.TickCount;
            var result = _repository.GetNextHead(spaceId, out previousHeadIdentifier);
            _profiler.WriteSample(_getGetNextHeadCounter, Environment.TickCount - start);
            return result;
        }
    }
}