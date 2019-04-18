//namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
//{
//    using System.Threading.Tasks
//    using EtAlii.Ubigia.Api.Functional

//    internal class ProfilingPathSubjectForPathAssignmentOperationConverter : IPathSubjectForPathAssignmentOperationConverter
//    {
//        private readonly IPathSubjectForPathAssignmentOperationConverter _decoree
//        private readonly IProfiler _profiler

//        public ProfilingPathSubjectForPathAssignmentOperationConverter(
//            IPathSubjectForPathAssignmentOperationConverter decoree,
//            IProfiler profiler)
//        {
//            _decoree = decoree
//            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorPathSubjectConversion)
//        }

//        public async Task<object> Convert(PathSubject pathSubject, ProcessParameters<Subject, SequencePart> parameters, ExecutionScope scope)
//        {
//            dynamic profile = _profiler.Begin("Converting path subject for path assignment: " + pathSubject.ToString())
//            profile.PathSubject = pathSubject

//            var result = await _decoree.Convert(pathSubject, parameters, scope)

//            _profiler.End(profile)

//            return result
//        }
//    }
//}