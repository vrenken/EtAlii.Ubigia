namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AddConstantToExistingPathProcessor : IAddConstantToExistingPathProcessor
    {
        private readonly IItemToIdentifierConverter _itemToIdentifierConverter;
        private readonly IRecursiveAdder _recursiveAdder;

        public AddConstantToExistingPathProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IRecursiveAdder recursiveAdder)
        {
            _itemToIdentifierConverter = itemToIdentifierConverter;
            _recursiveAdder = recursiveAdder;
        }

        private async Task Add(Identifier id, StringConstantSubject stringConstant, ExecutionScope scope, IObserver<object> output)
        {
            var constantPathSubjectPart = new ConstantPathSubjectPart(stringConstant.Value);
            var parentId = id;
            var addResult = await _recursiveAdder.Add(parentId, constantPathSubjectPart, null, scope);
            var newEntry = addResult.NewEntry;
            var result = new DynamicNode((IReadOnlyEntry)newEntry);
            output.OnNext(result);
        }


        public Task Process(OperatorParameters parameters)
        {
            if (!(parameters.RightSubject is StringConstantSubject stringConstant))
            {
                throw new ScriptProcessingException($"The {this.GetType().Name} requires a string constant on the right side");
            }

            if (string.IsNullOrWhiteSpace(stringConstant.Value))
            {
                throw new ScriptProcessingException($"The {this.GetType().Name} requires a non-empty string constant on the right side");
            }

            parameters.LeftInput.SubscribeAsync(
                onError: parameters.Output.OnError,
                onCompleted: parameters.Output.OnCompleted,
                onNext: async o =>
                {
                    var leftId = _itemToIdentifierConverter.Convert(o);
                    await Add(leftId, stringConstant, parameters.Scope, parameters.Output);
                });
            return Task.CompletedTask;
        }
    }
}
