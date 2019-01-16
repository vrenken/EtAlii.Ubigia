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
        public void Process(OperatorParameters parameters)
        {
            var rightId = Identifier.Empty;

            var task = Task.Run(async () =>
            {
                var rightResult = parameters.RightInput.ToEnumerable();
                rightId = await _itemToIdentifierConverter.Convert(rightResult, parameters.Scope);
                if (rightId == Identifier.Empty)
                {
                    throw new ScriptProcessingException("The RemoveByIdFromRelativePathProcessor requires a identifier to add");
                }
            });
            task.Wait();

            parameters.LeftInput.Subscribe(
                onError: parameters.Output.OnError,
                onCompleted: parameters.Output.OnCompleted,
                onNext: o =>
                {
                    var task2 = Task.Run(async () =>
                    {
                        var leftId = await _itemToIdentifierConverter.Convert(o, parameters.Scope);
                        await Remove(leftId, rightId, parameters.Scope, parameters.Output);
                    });
                    task2.Wait();
                });

            //if (leftIds == null || !leftIds.Any())
            //{
            //    throw new ScriptProcessingException("The RemoveByIdFromRelativePathProcessor requires queryable ids from the previous path part");
            //}
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
