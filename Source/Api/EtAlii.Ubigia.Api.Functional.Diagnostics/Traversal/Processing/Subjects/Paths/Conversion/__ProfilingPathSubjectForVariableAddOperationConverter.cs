// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

//namespace EtAlii.Ubigia.Diagnostics.Profiling
//[
//    using System.Threading.Tasks
//    using EtAlii.Ubigia.Api.Functional

//    internal class ProfilingPathSubjectForVariableAddOperationConverter : IPathSubjectForVariableAddOperationConverter
//    [
//        private readonly IPathSubjectForVariableAddOperationConverter _decoree
//        private readonly IProfiler _profiler

//        public ProfilingPathSubjectForVariableAddOperationConverter(
//            IPathSubjectForVariableAddOperationConverter decoree,
//            IProfiler profiler)
//        [
//            _decoree = decoree
//            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorPathSubjectConversion)
//        ]
//        public async Task<object> Convert(PathSubject pathSubject, ProcessParameters<Subject, SequencePart> parameters, ExecutionScope scope)
//        [
//            dynamic profile = _profiler.Begin("Converting path subject for variable add operation: " + pathSubject.ToString())
//            profile.PathSubject = pathSubject

//            var result = await _decoree.Convert(pathSubject, parameters, scope)

//            _profiler.End(profile)

//            return result
//        ]
//    ]
//]