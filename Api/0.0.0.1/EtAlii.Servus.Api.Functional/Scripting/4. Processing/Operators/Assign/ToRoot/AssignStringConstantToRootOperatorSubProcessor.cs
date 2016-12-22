namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class AssignStringConstantToRootOperatorSubProcessor : IAssignStringConstantToRootOperatorSubProcessor
    {
        private readonly IProcessingContext _context;

        public AssignStringConstantToRootOperatorSubProcessor(
            IProcessingContext context)
        {
            _context = context;
        }

        public void Assign(OperatorParameters parameters)
        {
            var definition = parameters.RightInput
                .ToEnumerable()
                .Cast<string>()
                .Single(); // We do not support multiple definitions

            parameters.LeftInput
                .Cast<RootSubject>()
                .Subscribe(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: (root) =>
                {
                    var task = Task.Run(async () =>
                    {
                        await _context.Logical.Roots.Add(root.Name);
                        parameters.Output.OnNext(root.Name);
                    });
                    task.Wait();
                });
        }
    }
}