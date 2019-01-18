namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class AssignCombinedToPathOperatorSubProcessor : IAssignCombinedToPathOperatorSubProcessor
    {
        private readonly IProcessingContext _context;
        private readonly IToIdentifierConverter _toIdentifierConverter;

        public AssignCombinedToPathOperatorSubProcessor(
            IToIdentifierConverter toIdentifierConverter, 
            IProcessingContext context)
        {
            _toIdentifierConverter = toIdentifierConverter;
            _context = context;
        }

        public async Task Assign(OperatorParameters parameters)
        {
            var value = parameters.RightInput
                .ToEnumerable()
                .Single(); // We do not support multiple constants (yet)

            parameters.LeftInput
                .Select(o => _toIdentifierConverter.Convert(o))
                .SubscribeAsync(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: async (o) =>
                {
                    //var assigner = _toIdentifierAssignerSelector.TrySelect(value);
                    //if (assigner == null)
                    //{
                    //    throw new ScriptProcessingException("Object not supported for assignment operations: " + (value != null ? value.ToString() : "NULL"));
                    //}

                    var result = await _context.Logical.Nodes.Assign(o, value, parameters.Scope);
                    //var result = await assigner.Assign(value, o, parameters.Scope);
                    parameters.Output.OnNext(result);
                });

            await Task.CompletedTask;
        }
    }
}