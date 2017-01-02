namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class OperatorsProcessor : IOperatorsProcessor
    {
        private readonly IOperatorProcessorSelector _selector;

        internal OperatorsProcessor(IOperatorProcessorSelector selector)
        {
            _selector = selector;
        }

        public async Task<object> Process(
            ProcessParameters<SequencePart, SequencePart> parameters, 
            ExecutionScope scope,
            IObserver<object> output)
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

            return await processor.Process(operatorParameters, scope, output);
        }
    }
}
