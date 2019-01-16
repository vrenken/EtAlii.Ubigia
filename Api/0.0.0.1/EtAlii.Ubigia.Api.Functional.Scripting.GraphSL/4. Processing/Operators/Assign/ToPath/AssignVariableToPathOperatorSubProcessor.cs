namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class AssignVariableToPathOperatorSubProcessor : IAssignVariableToPathOperatorSubProcessor
    {
        private readonly IProcessingContext _context;
        private readonly IToIdentifierConverter _toIdentifierConverter;

        public AssignVariableToPathOperatorSubProcessor(
            IToIdentifierConverter toIdentifierConverter, 
            IProcessingContext context)
        {
            _toIdentifierConverter = toIdentifierConverter;
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
                    //var assigner = _toIdentifierAssignerSelector.TrySelect(value);
                    //if (assigner == null)
                    //{
                    //    throw new ScriptProcessingException("Object not supported for assignment operations: " + (value != null ? value.ToString() : "NULL"));
                    //}
                    var task = Task.Run(async () =>
                    {
                        var result = await _context.Logical.Nodes.Assign(o, value, parameters.Scope);
                        //var result = await assigner.Assign(value, o, parameters.Scope);
                        parameters.Output.OnNext(result);
                    });
                    task.Wait();
                });
        }
    }
}