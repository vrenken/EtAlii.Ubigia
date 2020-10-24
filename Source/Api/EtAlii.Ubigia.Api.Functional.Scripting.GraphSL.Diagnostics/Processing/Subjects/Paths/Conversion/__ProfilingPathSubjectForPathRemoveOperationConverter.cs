//namespace EtAlii.Ubigia.Diagnostics.Profiling
//[
//    using System.Threading.Tasks
//    using EtAlii.Ubigia.Api.Functional

//    internal class ProfilingPathSubjectForPathRemoveOperationConverter : IPathSubjectForPathRemoveOperationConverter
//    [
//        private readonly IPathSubjectForPathRemoveOperationConverter _decoree
//        private readonly IProfiler _profiler

//        public ProfilingPathSubjectForPathRemoveOperationConverter(
//            IPathSubjectForPathRemoveOperationConverter decoree,
//            IProfiler profiler)
//        [
//            _decoree = decoree
//            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorPathSubjectConversion)
//        ]
//        public async Task<object> Convert(PathSubject pathSubject, ProcessParameters<Subject, SequencePart> parameters, ExecutionScope scope)
//        [
//            dynamic profile = _profiler.Begin("Converting path subject for path remove operation: " + pathSubject.ToString())
//            profile.PathSubject = pathSubject

//            var result = await _decoree.Convert(pathSubject, parameters, scope)x

//            _profiler.End(profile)

//            return result
//        ]
//    ]
//]