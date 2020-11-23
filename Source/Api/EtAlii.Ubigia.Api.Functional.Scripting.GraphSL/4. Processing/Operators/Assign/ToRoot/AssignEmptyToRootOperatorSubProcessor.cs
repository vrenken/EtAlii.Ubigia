namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AssignEmptyToRootOperatorSubProcessor : IAssignEmptyToRootOperatorSubProcessor
    {
        private readonly IScriptProcessingContext _context;

        public AssignEmptyToRootOperatorSubProcessor(
            IScriptProcessingContext context)
        {
            _context = context;
        }

        public Task Assign(OperatorParameters parameters)
        {
//            parameters.RightInput
//                .ToEnumerable()
//                .Cast<RootDefinitionSubject>()
//                .SingleOrDefault() // We do not support multiple definitions, should be null

            parameters.LeftInput
                .Cast<RootSubject>()
                .SubscribeAsync(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: async (root) =>
                {
                    try
                    {
                        var r = await _context.Logical.Roots.Get(root.Name).ConfigureAwait(false);
                        await _context.Logical.Roots.Remove(r.Id).ConfigureAwait(false);
                        parameters.Output.OnNext(root.Name);
                    }
                    catch (Exception e)
                    {
                        parameters.Output.OnError(e);
                    }
                });
            return Task.CompletedTask;
        }
    }
}