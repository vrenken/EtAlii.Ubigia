//namespace EtAlii.Servus.Api.Diagnostics.Profiling
//{
//    using System;
//    using System.Threading.Tasks;
//    using EtAlii.Servus.Api.Functional;

//    internal class ProfilingCommentProcessor : ICommentProcessor
//    {
//        private readonly ICommentProcessor _decoree;
//        private readonly IProfiler _profiler;
//        public ProfilingCommentProcessor(
//            ICommentProcessor decoree,
//            IProfiler profiler)
//        {
//            _decoree = decoree;
//            _profiler = profiler;
//        }

//        public async Task<object> Process(
//            ProcessParameters<SequencePart, SequencePart> parameters, 
//            ExecutionScope scope,
//            IObserver<object> output)
//        {
//            dynamic profile = _profiler.Begin("Comment: " + parameters.Target.ToString());
//            profile.Operator = parameters.Target;

//            var result = await _decoree.Process(parameters, scope, output);

//            _profiler.End(profile);

//            return result;
//        }
//    }
//}