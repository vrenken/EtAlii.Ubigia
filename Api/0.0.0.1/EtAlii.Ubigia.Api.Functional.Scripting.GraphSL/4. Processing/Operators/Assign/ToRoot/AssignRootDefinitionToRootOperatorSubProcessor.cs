namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using System.Reactive.Linq;

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
            var result = parameters.RightInput
                .ToEnumerable()
                .Cast<RootDefinitionSubject>()
                .Count();
            if (result > 1)
            {
                throw new ScriptProcessingException("Unable to assign: Multiple roots specified.");
            }
            if (result == 0)
            {
                throw new ScriptProcessingException("Unable to assign: No root specified.");
            }
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