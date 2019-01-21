namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class AssignPathToPathOperatorSubProcessor : IAssignPathToPathOperatorSubProcessor
    {
        private readonly IProcessingContext _context;
        private readonly IToIdentifierConverter _toIdentifierConverter;

        public AssignPathToPathOperatorSubProcessor(
            IToIdentifierConverter toIdentifierConverter, 
            IProcessingContext context)
        {
            _toIdentifierConverter = toIdentifierConverter;
            _context = context;
        }

        public async Task Assign(OperatorParameters parameters)
        {
            var value = parameters.RightInput
                .ToEnumerable()
                .Single(); // We do not support multiple constants (yet)

            parameters.LeftInput
                .Select(o => _toIdentifierConverter.Convert(o))
                .SubscribeAsync(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: async (o) =>
                {
                    var result = await _context.Logical.Nodes.Assign(o, value, parameters.Scope);
                    parameters.Output.OnNext(result);
                });

            await Task.CompletedTask;
        }
    }
}