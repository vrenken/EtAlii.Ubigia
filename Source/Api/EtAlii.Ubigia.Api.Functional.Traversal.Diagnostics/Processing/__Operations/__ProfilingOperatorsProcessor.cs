//namespace EtAlii.Ubigia.Diagnostics.Profiling
//[
//    using System
//    using System.Threading.Tasks
//    using EtAlii.Ubigia.Api.Functional

//    internal class ProfilingOperatorsProcessor : IOperatorsProcessor
//    [
//        private readonly IOperatorsProcessor _decoree
//        private readonly IProfiler _profiler

//        public ProfilingOperatorsProcessor(
//            IOperatorsProcessor decoree, 
//            IProfiler profiler)
//        [
//            _decoree = decoree
//            _profiler = profiler
//        ]
//        public async Task<object> Process(
//            ProcessParameters<SequencePart, SequencePart> parameters, 
//            ExecutionScope scope,
//            IObserver<object> output)
//        [
//            dynamic profile = _profiler.Begin("Operator: " + parameters.Target.ToString())
//            profile.Operator = parameters.Target

//            var result = await _decoree.Process(parameters, scope, output)

//            _profiler.End(profile)

//            return result
//        ]
//    ]
//]