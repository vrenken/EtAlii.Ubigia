namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Linq;

    internal class AssignEmptyToRootOperatorSubProcessor : IAssignEmptyToRootOperatorSubProcessor
    {
        private readonly IProcessingContext _context;

        public AssignEmptyToRootOperatorSubProcessor(
            IProcessingContext context)
        {
            _context = context;
        }

        public void Assign(OperatorParameters parameters)
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
                        var r = await _context.Logical.Roots.Get(root.Name);
                        await _context.Logical.Roots.Remove(r.Id);
                        parameters.Output.OnNext(root.Name);
                    }
                    catch (Exception e)
                    {
                        parameters.Output.OnError(e);
                    }
                });
        }
    }
}