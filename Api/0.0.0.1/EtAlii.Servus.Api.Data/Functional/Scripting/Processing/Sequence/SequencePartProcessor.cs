namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    internal class SequencePartProcessor : ISequencePartProcessor
    {
        private readonly ISelector<SequencePart, ISequencePartProcessor> _selector;

        internal SequencePartProcessor(ISelector<SequencePart, ISequencePartProcessor> selector)
        {
            _selector = selector;
        }

        public object Process(ProcessParameters<SequencePart, SequencePart> parameters)
        {
            var processor = _selector.Select(parameters.Target);
            var result = processor.Process(parameters);
            return result;
        }
    }
}
