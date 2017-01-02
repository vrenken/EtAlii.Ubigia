//namespace EtAlii.Servus.Api.Diagnostics.Profiling
//{
//    using System.Threading.Tasks;
//    using EtAlii.Servus.Api.Functional;

//    internal class ProfilingPathSubjectForFunctionAssignmentOperationConverter : IPathSubjectForFunctionAssignmentOperationConverter
//    {
//        private readonly IPathSubjectForFunctionAssignmentOperationConverter _decoree;
//        private readonly IProfiler _profiler;

//        public ProfilingPathSubjectForFunctionAssignmentOperationConverter(
//            IPathSubjectForFunctionAssignmentOperationConverter decoree,
//            IProfiler profiler)
//        {
//            _decoree = decoree;
//            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorPathSubjectConversion);
//        }

//        public async Task<object> Convert(PathSubject pathSubject, ProcessParameters<Subject, SequencePart> parameters, ExecutionScope scope)
//        {
//            dynamic profile = _profiler.Begin("Converting path subject for function assignment: " + pathSubject.ToString());
//            profile.PathSubject = pathSubject;

//            var result = await _decoree.Convert(pathSubject, parameters, scope);

//            _profiler.End(profile);

//            return result;
//        }
//    }
//}