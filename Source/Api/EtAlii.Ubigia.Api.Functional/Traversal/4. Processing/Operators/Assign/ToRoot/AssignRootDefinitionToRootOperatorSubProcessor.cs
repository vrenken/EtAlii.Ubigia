// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class AssignRootDefinitionToRootOperatorSubProcessor : IAssignRootDefinitionToRootOperatorSubProcessor
    {
        private readonly IScriptProcessingContext _context;

        public AssignRootDefinitionToRootOperatorSubProcessor(
            IScriptProcessingContext context)
        {
            _context = context;
        }

        public Task Assign(OperatorParameters parameters)
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
                    var createdRoot = await _context.Logical.Roots.Add(root.Name).ConfigureAwait(false);
                    parameters.Output.OnNext(createdRoot.Identifier);
                });
            return Task.CompletedTask;
        }
    }
}
