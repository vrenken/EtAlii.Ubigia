namespace EtAlii.Ubigia.Api.Functional
{
    using System.Reactive.Linq;

    internal class AssignRootDefinitionToRootOperatorSubProcessor : IAssignRootDefinitionToRootOperatorSubProcessor
    {
        private readonly IProcessingContext _context;

        public AssignRootDefinitionToRootOperatorSubProcessor(
            IProcessingContext context)
        {
            _context = context;
        }

        public void Assign(OperatorParameters parameters)
        {
//            parameters.RightInput
//                .ToEnumerable()
//                .Cast<RootDefinitionSubject>()
//                .Single() // We do not support multiple definitions

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
        }
    }
}