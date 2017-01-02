namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using System.Linq;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Structure;

    internal class OperatorProcessor : ISequencePartProcessor
    {
        private readonly ISelector<Operator, IOperatorProcessor> _selector;

        internal OperatorProcessor(ISelector<Operator, IOperatorProcessor> selector)
        {
            _selector = selector;
        }

        public object Process(ProcessParameters<SequencePart, SequencePart> parameters)
        {
            var @operator = parameters.Target as Operator;
            var processor = _selector.Select(@operator);

            var operatorParameters = new ProcessParameters<Operator, SequencePart>(@operator)
            {
                FuturePart = parameters.FuturePart,
                LeftPart = parameters.LeftPart,
                RightPart = parameters.RightPart,
                RightResult = parameters.RightResult,
                LeftResult = parameters.LeftResult,
            };

            return processor.Process(operatorParameters);
        }
    }
}
