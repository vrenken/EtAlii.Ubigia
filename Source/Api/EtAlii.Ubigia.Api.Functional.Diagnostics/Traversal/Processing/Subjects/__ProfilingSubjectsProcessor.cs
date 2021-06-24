// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

//namespace EtAlii.Ubigia.Diagnostics.Profiling
//[
//    using System
//    using System.Threading.Tasks
//    using EtAlii.Ubigia.Api.Functional

//    internal class ProfilingSubjectsProcessor : ISubjectsProcessor
//    [
//        private readonly ISubjectsProcessor _decoree
//        private readonly IProfiler _profiler

//        public ProfilingSubjectsProcessor(
//            ISubjectsProcessor decoree,
//            IProfiler profiler)
//        [
//            _decoree = decoree
//            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorSubject)
//        ]
//        public async Task<object> Process(
//            ProcessParameters<SequencePart, SequencePart> parameters,
//            ExecutionScope scope,
//            IObserver<object> output)
//        [
//            dynamic profile = _profiler.Begin("Subject: " + parameters.Target.ToString())
//            profile.Operator = parameters.Target

//            var result = await _decoree.Process(parameters, scope, output)

//            _profiler.End(profile)

//            return result
//        ]
//    ]
//]
