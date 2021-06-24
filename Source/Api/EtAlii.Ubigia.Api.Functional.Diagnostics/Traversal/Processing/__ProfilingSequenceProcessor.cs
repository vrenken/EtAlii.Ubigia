// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

//namespace EtAlii.Ubigia.Diagnostics.Profiling
//[
//    using System
//    using System.Threading.Tasks
//    using EtAlii.Ubigia.Api.Functional

//    internal class ProfilingSequenceProcessor : ISequenceProcessor
//    [
//        private readonly ISequenceProcessor _decoree
//        private readonly IProfiler _profiler

//        public ProfilingSequenceProcessor(
//            ISequenceProcessor decoree,
//            IProfiler profiler)
//        [
//            _decoree = decoree
//            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptSequenceProcessor)
//        ]
//        public async Task Process(Sequence sequence, IObserver<object> output)
//        [
//            dynamic profile = _profiler.Begin("Sequence: " + sequence.ToString())
//            profile.Sequence = sequence

//            await _decoree.Process(sequence, output)

//            _profiler.End(profile)
//        ]
//    ]
//]
