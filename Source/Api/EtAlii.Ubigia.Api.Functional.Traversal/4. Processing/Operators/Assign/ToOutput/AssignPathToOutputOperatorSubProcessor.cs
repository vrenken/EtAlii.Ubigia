namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AssignPathToOutputOperatorSubProcessor : IAssignPathToOutputOperatorSubProcessor
    {
        private readonly IResultConverterSelector _resultConverterSelector;

        public AssignPathToOutputOperatorSubProcessor(IResultConverterSelector resultConverterSelector)
        {
            _resultConverterSelector = resultConverterSelector;
        }

        public Task Assign(OperatorParameters parameters)
        {
            parameters.RightInput.SubscribeAsync(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: async o =>
                {
                    var outputConverter = _resultConverterSelector.Select(o);
                    await outputConverter(o, parameters.Scope, parameters.Output).ConfigureAwait(false);
                });
            return Task.CompletedTask;
        }
    }
}
