namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

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
            var definition = parameters.RightInput
                .ToEnumerable()
                .Cast<RootDefinitionSubject>()
                .SingleOrDefault(); // We do not support multiple definitions, should be null

            parameters.LeftInput
                .Cast<RootSubject>()
                .Subscribe(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: (root) =>
                {
                    var task = Task.Run(async () =>
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
                    task.Wait();
                });
        }
    }
}