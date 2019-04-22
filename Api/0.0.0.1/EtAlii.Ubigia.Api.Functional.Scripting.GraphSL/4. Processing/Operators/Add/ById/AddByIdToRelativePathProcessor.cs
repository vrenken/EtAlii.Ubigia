namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

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


        public Task Process(OperatorParameters parameters)
        {
            var idToAdd = GetIdToAdd(parameters);
            if (idToAdd == Identifier.Empty)
            {
                throw new ScriptProcessingException("The AddByIdToRelativePathProcessor requires a identifier to add");
            }

            parameters.LeftInput.SubscribeAsync(
                onError: parameters.Output.OnError,
                onCompleted: parameters.Output.OnCompleted,
                onNext: async o =>
                {
                    var leftId = await _itemToIdentifierConverter.Convert(o, parameters.Scope);
                    await Add(leftId, idToAdd, parameters.Scope, parameters.Output);
                });
            return Task.CompletedTask;
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
            var result = new DynamicNode(newEntry);
            output.OnNext(result);
        }
    }
}
