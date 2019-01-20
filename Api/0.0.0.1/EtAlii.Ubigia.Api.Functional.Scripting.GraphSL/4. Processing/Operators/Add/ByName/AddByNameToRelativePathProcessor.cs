namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AddByNameToRelativePathProcessor : IAddByNameToRelativePathProcessor
    {
        private readonly IItemToPathSubjectConverter _itemToPathSubjectConverter;
        private readonly IItemToIdentifierConverter _itemToIdentifierConverter;
        private readonly IRecursiveAdder _recursiveAdder;

        public AddByNameToRelativePathProcessor(
            IItemToPathSubjectConverter itemToPathSubjectConverter,
            IItemToIdentifierConverter itemToIdentifierConverter,
            IRecursiveAdder recursiveAdder)
        {
            _itemToPathSubjectConverter = itemToPathSubjectConverter;
            _itemToIdentifierConverter = itemToIdentifierConverter;
            _recursiveAdder = recursiveAdder;
        }

        public async Task Process(OperatorParameters parameters)
        {
            var pathToAdd = await GetPathToAdd(parameters);
            if (pathToAdd == null)
            {
                throw new ScriptProcessingException("The AddByNameToRelativePathProcessor requires a path on the right side");
            }

            if (!pathToAdd.Parts.All(part => part is ConstantPathSubjectPart || part is ParentPathSubjectPart))
            {
                throw new ScriptProcessingException("The AddByNameToRelativePathProcessor requires a constant, hierarchical path");
            }

            if (pathToAdd.Parts.Any(part => part is ConstantPathSubjectPart && String.IsNullOrWhiteSpace(((ConstantPathSubjectPart)part).Name)))
            {
                throw new ScriptProcessingException("The AddByNameToRelativePathProcessor cannot handle empty parts");
            }

            parameters.LeftInput.SubscribeAsync
            (
                onError: parameters.Output.OnError,
                onCompleted: parameters.Output.OnCompleted,
                onNext: async o =>
                {
                    var leftId = await _itemToIdentifierConverter.Convert(o, parameters.Scope);
                    await Add(leftId, pathToAdd, parameters.Scope, parameters.Output);
                }
            );
            
            await Task.CompletedTask;
        }

        private async Task<PathSubject> GetPathToAdd(OperatorParameters parameters)
        {
            PathSubject pathToAdd = null;

            if (parameters.RightSubject is PathSubject subject)
            {
                pathToAdd = subject;
            }
            else
            {
                var rightResult = await parameters.RightInput.SingleOrDefaultAsync();

                _itemToPathSubjectConverter.TryConvert(rightResult, out pathToAdd);
            }

            return pathToAdd;
        }

        private async Task Add(Identifier id, PathSubject pathToAdd, ExecutionScope scope, IObserver<object> output)
        {
            var parentId = id;
            IEditableEntry newEntry = null;
            foreach (var partToAdd in pathToAdd.Parts.OfType<ConstantPathSubjectPart>())
            {
                var addResult = await _recursiveAdder.Add(parentId, partToAdd, newEntry, scope);
                parentId = addResult.ParentId;
                newEntry = addResult.NewEntry;
            }
            var result = new DynamicNode((IReadOnlyEntry)newEntry);
            output.OnNext(result);
        }
    }
}
