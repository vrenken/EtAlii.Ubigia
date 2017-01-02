namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class AssignVariableToOutputOperatorSubProcessor : IAssignVariableToOutputOperatorSubProcessor
    {
        private readonly IResultConverterSelector _resultConverterSelector;

        public AssignVariableToOutputOperatorSubProcessor(IResultConverterSelector resultConverterSelector)
        {
            _resultConverterSelector = resultConverterSelector;
        }

        public void Assign(OperatorParameters parameters)
        {
            parameters.RightInput.Subscribe(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: o =>
                {
                    var outputConverter = _resultConverterSelector.Select(o);
                    outputConverter(o, parameters.Scope, parameters.Output);
                });
        }
    }
}