namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AssignStringConstantToRootOperatorSubProcessor : IAssignStringConstantToRootOperatorSubProcessor
    {
        private readonly IScriptProcessingContext _context;

        public AssignStringConstantToRootOperatorSubProcessor(
            IScriptProcessingContext context)
        {
            _context = context;
        }

        public Task Assign(OperatorParameters parameters)
        {
            // ReSharper disable once UnusedVariable
//            var definition = parameters.RightInput
//                .ToEnumerable()
//                .Cast<string>()
//                .Single() // We do not support multiple definitions

            parameters.LeftInput
                .Cast<RootSubject>()
                .SubscribeAsync(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: async (root) =>
                {
                    await _context.Logical.Roots.Add(root.Name).ConfigureAwait(false);
                    parameters.Output.OnNext(root.Name);
                });
            return Task.CompletedTask;
        }
    }
}