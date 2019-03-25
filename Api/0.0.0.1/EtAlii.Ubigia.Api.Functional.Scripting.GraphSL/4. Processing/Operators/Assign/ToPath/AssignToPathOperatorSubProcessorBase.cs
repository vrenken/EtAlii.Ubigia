namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal abstract class AssignToPathOperatorSubProcessorBase 
    {
        private readonly IProcessingContext _context;
        private readonly IToIdentifierConverter _toIdentifierConverter;
        private readonly IPathSubjectToGraphPathConverter _pathSubjectToGraphPathConverter;

        public AssignToPathOperatorSubProcessorBase(
            IToIdentifierConverter toIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IProcessingContext context)
        {
            _toIdentifierConverter = toIdentifierConverter;
            _pathSubjectToGraphPathConverter = pathSubjectToGraphPathConverter;
            _context = context;
        }

        public void Assign(OperatorParameters parameters)
        {
            var value = parameters.RightInput
                .ToEnumerable()
                .Single(); // We do not support multiple constants (yet)

            parameters.LeftInput
                .Select(o => _toIdentifierConverter.Convert(o))
                .Subscribe(
                    onError: (e) => parameters.Output.OnError(e),
                    onCompleted: () => parameters.Output.OnCompleted(),
                    onNext: (o) =>
                    {
                        var task = Task.Run(async () =>
                        {
                            var leftPathSubject = (PathSubject)parameters.LeftSubject;
                            var graphPath = await _pathSubjectToGraphPathConverter.Convert(leftPathSubject, parameters.Scope);

                            var result = await _context.Logical.Nodes.Assign(graphPath, o, value, parameters.Scope);
                            parameters.Output.OnNext(result);
                        });
                        task.Wait();
                    });
        }
    }
}