namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AssignCombinedToOutputOperatorSubProcessor : IAssignCombinedToOutputOperatorSubProcessor
    {
        private readonly IResultConverter _resultConverter;

        public AssignCombinedToOutputOperatorSubProcessor(IResultConverter resultConverter)
        {
            _resultConverter = resultConverter;
        }

        public Task Assign(OperatorParameters parameters)
        {
            parameters.RightInput.SubscribeAsync(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: async o =>
                {
                    await _resultConverter.Convert(o, parameters.Scope, parameters.Output).ConfigureAwait(false);
                });
            return Task.CompletedTask;
        }
    }
}
