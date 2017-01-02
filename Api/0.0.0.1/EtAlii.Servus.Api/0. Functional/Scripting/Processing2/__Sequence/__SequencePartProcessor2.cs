//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Threading.Tasks;

//    internal class SequencePartProcessor2 : ISequencePartProcessor2
//    {
//        private readonly ISequencePartProcessorSelector2 _selector;

//        internal SequencePartProcessor2(ISequencePartProcessorSelector2 selector)
//        {
//            _selector = selector;
//        }

//        public async Task<object> Process(
//            ProcessParameters<SequencePart, SequencePart> parameters, 
//            ExecutionScope scope,
//            IObserver<object> output)
//        {
//            var processor = _selector.Select(parameters.Target);
//            var result = await processor.Process(parameters, scope, output);
//            return result;
//        }
//    }
//}
