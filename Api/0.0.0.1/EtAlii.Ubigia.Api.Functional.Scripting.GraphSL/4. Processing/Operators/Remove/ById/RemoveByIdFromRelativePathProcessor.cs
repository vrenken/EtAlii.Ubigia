namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class RemoveByIdFromRelativePathProcessor : IRemoveByIdFromRelativePathProcessor
    {
        private readonly IItemToIdentifierConverter _itemToIdentifierConverter;
        private readonly IProcessingContext _context;

        public RemoveByIdFromRelativePathProcessor(
            IProcessingContext context,
            IItemToIdentifierConverter itemToIdentifierConverter)
        {
            _itemToIdentifierConverter = itemToIdentifierConverter;
            _context = context;
        }
        public async Task Process(OperatorParameters parameters)
        {
            var rightResult = parameters.RightInput.ToEnumerable();
            var rightId = await _itemToIdentifierConverter.Convert(rightResult, parameters.Scope);
            if (rightId == Identifier.Empty)
            {
                throw new ScriptProcessingException("The RemoveByIdFromRelativePathProcessor requires a identifier to add");
            }

            parameters.LeftInput.SubscribeAsync(
                onError: parameters.Output.OnError,
                onCompleted: parameters.Output.OnCompleted,
                onNext: async o =>
                {
                    var leftId = await _itemToIdentifierConverter.Convert(o, parameters.Scope);
                    await Remove(leftId, rightId, parameters.Scope, parameters.Output);
                });
        }

        private async Task Remove(
            Identifier id,
            Identifier identifierToRemove,
            ExecutionScope scope,
            IObserver<object> output)
        {
            var newEntry = await _context.Logical.Nodes.Remove(id, identifierToRemove, scope);
            var result = new DynamicNode(newEntry);
            output.OnNext(result);
        }
    }
}
