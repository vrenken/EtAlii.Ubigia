namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

    internal class AddByIdToRelativePathProcessor : IAddByIdToRelativePathProcessor
    {
        private readonly IItemToIdentifierConverter _itemToIdentifierConverter;
        private readonly IProcessingContext _context;

        public AddByIdToRelativePathProcessor(
            IProcessingContext context,
            IItemToIdentifierConverter itemToIdentifierConverter)
        {
            _itemToIdentifierConverter = itemToIdentifierConverter;
            _context = context;
        }


        public void Process(OperatorParameters parameters)
        {
            var idToAdd = Identifier.Empty;

            idToAdd = GetIdToAdd(parameters);
            if (idToAdd == Identifier.Empty)
            {
                throw new ScriptProcessingException("The AddByIdToRelativePathProcessor requires a identifier to add");
            }

            parameters.LeftInput.Subscribe(
                onError: parameters.Output.OnError,
                onCompleted: parameters.Output.OnCompleted,
                onNext: o =>
                {
                    var task2 = Task.Run(async () =>
                    {
                        var leftId = await _itemToIdentifierConverter.Convert(o, parameters.Scope);
                        await Add(leftId, idToAdd, parameters.Scope, parameters.Output);
                    });
                    task2.Wait();
                });
        }

        private Identifier GetIdToAdd(OperatorParameters parameters)
        {
            var idToAdd = Identifier.Empty;
            var task = Task.Run(async () =>
            {
                var rightResult = await parameters.RightInput.SingleAsync();
                idToAdd = await _itemToIdentifierConverter.Convert(rightResult, parameters.Scope);
            });
            task.Wait();

            return idToAdd;
        }

        private async Task Add(
            Identifier id,
            Identifier identifierToAdd,
            ExecutionScope scope,
            IObserver<object> output)
        {
            var newEntry = await _context.Logical.Nodes.Add(id, identifierToAdd, scope);
            var result = new DynamicNode((IReadOnlyEntry)newEntry);
            output.OnNext(result);
        }
    }
}
