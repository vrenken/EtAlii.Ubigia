namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class AssignRootDefinitionToRootOperatorSubProcessor : IAssignRootDefinitionToRootOperatorSubProcessor
    {
        private readonly IProcessingContext _context;

        public AssignRootDefinitionToRootOperatorSubProcessor(
            IProcessingContext context)
        {
            _context = context;
        }

        public async Task Assign(OperatorParameters parameters)
        {
            parameters.RightInput
                .ToEnumerable()
                .Cast<RootDefinitionSubject>()
                .Single(); // We do not support multiple definitions

            parameters.LeftInput
                .Cast<RootSubject>()
                .SubscribeAsync(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: async (root) =>
                {
                    var createdRoot = await _context.Logical.Roots.Add(root.Name);
                    parameters.Output.OnNext(createdRoot.Identifier);
                });

            await Task.CompletedTask;
        }
    }
}