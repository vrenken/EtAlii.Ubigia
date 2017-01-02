namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class SequencePartProcessor : ISequencePartProcessor
    {
        private readonly ISequencePartProcessorSelector _selector;

        internal SequencePartProcessor(ISequencePartProcessorSelector selector)
        {
            _selector = selector;
        }

        public async Task<object> Process(
            ProcessParameters<SequencePart, SequencePart> parameters, 
            ExecutionScope scope,
            IObserver<object> output)
        {
            var processor = _selector.Select(parameters.Target);
            var result = await processor.Process(parameters, scope, output);
            return result;
        }
    }
}
