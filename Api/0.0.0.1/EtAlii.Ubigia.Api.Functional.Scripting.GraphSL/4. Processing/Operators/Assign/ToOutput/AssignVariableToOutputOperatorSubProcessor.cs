namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal class AssignVariableToOutputOperatorSubProcessor : IAssignVariableToOutputOperatorSubProcessor
    {
        private readonly IResultConverterSelector _resultConverterSelector;

        public AssignVariableToOutputOperatorSubProcessor(IResultConverterSelector resultConverterSelector)
        {
            _resultConverterSelector = resultConverterSelector;
        }

        public async Task Assign(OperatorParameters parameters)
        {
            parameters.RightInput.SubscribeAsync(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: async o =>
                {
                    var outputConverter = _resultConverterSelector.Select(o);
                    await outputConverter(o, parameters.Scope, parameters.Output);
                });

            await Task.CompletedTask;
        }
    }
}